using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventAppCore.Models;
using EventAppCore.Models.View;
using EventAppCore.Repositories;
using EventAppCore.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventAppCore.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly AccessTokenService _accessTokenService;

        public UserController(UserRepository userRepository, AccessTokenService accessTokenService)
        {
            _userRepository = userRepository;
            _accessTokenService = accessTokenService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] SignUpUser userModel)
        {
            var addedUser = await _userRepository.Put(userModel);
            return Json(Mapper.Map<ViewUser>(addedUser));
        }

        [HttpPost("[action]")]
        public IActionResult Get()
        {
            return Json(Mapper.Map<ViewUser>(_userRepository.GetById(_accessTokenService.GetIdFromToken(this.User))));
        }

        [HttpGet("[action]")]
        public IActionResult Get(string id)
        {
            var user = _userRepository.GetById(id);
            //Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented));
            return Json(Mapper.Map<ViewUser>(user));
        }

        [HttpGet("[action]")]
        public IActionResult Search(string query, int page = 0)
        {
            var pageSize = 8;

            if (query == null) return BadRequest();
            
            var users = _userRepository.Search(query)
                .Skip(page * pageSize)
                .Take(pageSize)
                //.ProjectTo<SignUpUser>()
                .ToList();

            return Json(Mapper.Map<List<ViewUser>>(users));
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {   
            return Json(Mapper.Map<List<ViewUser>>(_userRepository.GetAll().ToList()));
        }
    }
}