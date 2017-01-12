using System;
using System.Threading.Tasks;
using AutoMapper;
using EventAppCore.Models.View;
using EventAppCore.Repositories;
using EventAppCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventAppCore.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly RefreshTokenRepository _refreshTokenRepository;
        private readonly UserRepository _userRepository;
        private readonly AccessTokenService _accessTokenServices;

        public AuthController(RefreshTokenRepository refreshTokenRepository, UserRepository userRepository,
            AccessTokenService accessTokenServices)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _accessTokenServices = accessTokenServices;
        }

        // Equivalent to 'Sign in'
        // TODO Maybe read data FromBody
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRefreshToken(string username, string password, string device)
        {
            var user = _userRepository.GetUserByCredentials(username, password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Json(Mapper.Map<ViewRefreshToken>(await _refreshTokenRepository.CreateTokenForUser(user, device)));
        }

        // Token that gives access to resources
        [HttpGet("[action]")]
        public IActionResult GetAccessToken(string refreshToken)
        {

            return Json(_accessTokenServices.GetToken(_refreshTokenRepository.GetUserFromToken(refreshToken)));
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult ProtectedRes()
        {
            return Json(new {A = "B", Ka = "lleAnka"});
        }
    }
}