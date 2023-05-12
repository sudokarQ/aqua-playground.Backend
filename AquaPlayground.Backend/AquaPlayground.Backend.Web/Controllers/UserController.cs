using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.User;
using AquaPlayground.Backend.Common.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AquaPlayground.Backend.Web.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("GetAllUsers")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetUsersByName")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListByName(string name)
        {
            try
            {
                var users = await _userService.FindByNameAsync(name);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetUsersByLogin")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListByLogin(string login)
        {
            try
            {
                var users = await _userService.FindByLoginAsync(login);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("FindUser")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FindByIdAsync(string id)
        {
            try
            {
                var user = await _userService.FindByIdAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto dto)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userService.UpdateUserAsync(dto, user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Updated");
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userService.DeleteUserAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Deleted");
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                return NotFound("Incorrect Id");
            }

            var result = await _userService.DeleteUserAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Deleted");
        }
    }
}
