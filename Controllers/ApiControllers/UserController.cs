using System;
using System.Linq;
using System.Threading.Tasks;
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
            //Console.WriteLine(serUser);
            //var user = JsonConvert.DeserializeObject<User>(serUser);
            //Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented));
            var addedUser = await _userRepository.Put(userModel);
            return Json(addedUser);
        }

        [HttpPost("[action]")]
        public IActionResult Get()
        {
            return Json(_userRepository.GetById(_accessTokenService.GetIdFromToken(this.User)));
        }

        [HttpGet("[action]")]
        public IActionResult Get(string id)
        {
            var user = _userRepository.GetById(id);
            Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented));
            return Json(user);
        }

        [HttpGet("[action]")]
        public IActionResult Search(string query, int page = 0)
        {
            if (query == null) return BadRequest();

            var users = _userRepository.Search(query)
                .Skip(page*8)
                .Take(8)
                .ProjectTo<SignUpUser>()
                .ToList();
            //Console.WriteLine(JsonConvert.SerializeObject(users, Formatting.Indented));
            return Json(users);
        }
    }
}