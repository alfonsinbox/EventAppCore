using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventAppCore.Models;
using EventAppCore.Models.View;
using EventAppCore.Repositories;
using EventAppCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;

namespace EventAppCore.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly LocationRepository _locationRepository;
        private readonly UserRepository _userRepository;
        private readonly AccessTokenService _accessTokenService;

        public LocationController(LocationRepository locationRepository, UserRepository userRepository,
            AccessTokenService accessTokenService)
        {
            _locationRepository = locationRepository;
            _userRepository = userRepository;
            _accessTokenService = accessTokenService;
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateLocation locationModel)
        {
            return Json(
                await _locationRepository.Put(locationModel,
                    _userRepository.GetById(_accessTokenService.GetIdFromToken(this.User))));
        }

        //[Authorize]
        [HttpGet("[action]")]
        public IActionResult Get(string id, double latitude, double longitude)
        {
            return Json(Mapper.Map<ViewLocation>(_locationRepository.GetById(id)
                .WithDistanceFrom(latitude, longitude)));

        }

        // page = the amount of "chunks" it should skip in database before reading the data
        [HttpGet("[action]")]
        public IActionResult Search(double latitude, double longitude, int page = 0, string query = "")
        {
            var allLocations = Mapper.Map<List<ViewLocation>>(_locationRepository.Search(query)
                .OrderBy(l => l.GetDistanceFrom(latitude, longitude))
                .Skip(page*8)
                .Take(8)
                .ToList());

            allLocations.ForEach(l => l.SetDistanceFrom(latitude, longitude));

            return Json(allLocations);
        }
    }
}
