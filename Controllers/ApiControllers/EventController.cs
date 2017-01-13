using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventAppCore.Models;
using EventAppCore.Models.View;
using EventAppCore.Repositories;
using EventAppCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventAppCore.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly EventRepository _eventRepository;
        private readonly AccessTokenService _accessTokenService;
        private readonly UserRepository _userRepository;
        private readonly LocationRepository _locationRepository;

        public EventController(EventRepository eventRepository, AccessTokenService accessTokenService,
            UserRepository userRepository, LocationRepository locationRepository)
        {
            _eventRepository = eventRepository;
            _accessTokenService = accessTokenService;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        [HttpPost("[action]")]
        [Authorize]
        public IActionResult Create([FromBody] CreateEvent eventModel)
        {
            var requestingUser = _userRepository.GetById(_accessTokenService.GetIdFromToken(this.User));
            var desiredLocation = _locationRepository.GetById(eventModel.LocationId);
            var addedEvent = _eventRepository.Put(eventModel, requestingUser, desiredLocation);
            return Json(addedEvent);
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult Get()
        {
            return Json(Mapper.Map<List<ViewEvent>>(_eventRepository.GetAll()
                .Where(e => e.CreatedBy == _userRepository.GetById(_accessTokenService.GetIdFromToken(this.User)))));
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult GetUpcoming()
        {
            // TODO Check what role the user has
            return Json(Mapper.Map<List<ViewEvent>>(_eventRepository.GetAll()
                .Where(e => e.EndTime > DateTimeOffset.UtcNow &&
                            e.UserEvents.Any(ue => ue.User ==
                                                   _userRepository.GetById(
                                                       _accessTokenService.GetIdFromToken(this.User))))));
        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult GetMy()
        {
            return Json(Mapper.Map<List<ViewEvent>>(
                _eventRepository.GetAll()
                    .Where(e => e.CreatedBy ==
                                _userRepository.GetById(_accessTokenService.GetIdFromToken(this.User)))));
        }
    }
}