﻿using System.Linq;
using System.Threading.Tasks;
using ConferenceSystem.Application.Auth;
using ConferenceSystem.Mappers;
using ConferenceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceSystem.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationManager _authenticationManager;

        public UserController(
            IUserService userService,
            IAuthenticationManager authenticationManager)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _authenticationManager.GetCurrentUser();
            var userModel = UserMapper.Map(user);

            return Ok(userModel);
        }

        [HttpGet]
        [Route("items")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAsync();
            var userModels = users.Select(UserMapper.Map);

            return Ok(userModels);
        }
    }
}
