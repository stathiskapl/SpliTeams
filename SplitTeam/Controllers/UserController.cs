using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SplitTeam.Model;
using SplitTeam.ModelDtos;
using SplitTeam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitTeam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _log;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> log, IUserService userService)
        {
            _log = log;
            _userService = userService;
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserToLoginDto user)
        {
            try
            {
                var userToRegister = await _userService.AddUser(user);
                if (userToRegister == null)
                {
                    _log.LogError($"User with username {user.Username} exists");
                    return StatusCode(502, "User already exists");
                }
                else
                {
                    _log.LogInformation($"Returning new user with id {userToRegister.Id}");
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                await _userService.UpdateUser(user);
                _log.LogInformation($"Returning updated user with id {user.Id}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] User user)
        {
            try
            {
                await _userService.DeleteUser(user);
                _log.LogInformation($"User with id {user} deleted");
                return Ok(user.Id);

            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                _log.LogInformation($"Returning get user with id {id}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAll();
                _log.LogInformation($"Returning all users: {users.Count}");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserToLoginDto user)
        {
            try
            {
                var userToLogin = await _userService.Login(user.Username, user.Password);
                if (userToLogin == null)
                {
                    return BadRequest("Username or Password is wrong");
                }

                _log.LogInformation($"Returning token {userToLogin.Token} for user with id {userToLogin.Id}");
                return Ok(userToLogin);
            }
            catch (Exception ex)
            {
                _log.LogError($"Something went wrong: {ex}");
                return StatusCode(500, ex);
            }
        }

    }
}
