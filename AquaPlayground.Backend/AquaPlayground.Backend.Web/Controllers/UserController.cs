namespace AquaPlayground.Backend.Web.Controllers
{
    using BuisnessLayer.Intefaces;

    using Common.Models.Dto.User;
    using Common.Models.Entity;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        private readonly UserManager<User> userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await userService.GetAllAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("profile")]
        [Authorize]
        [Produces("application/json")]
        public async Task<IActionResult> GetMyProfile()
        {
            var user = await userManager.GetUserAsync(User);

            try
            {
                var result = await userService.FindByIdAsync(user.Id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("name")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListByName([FromQuery] string name)
        {
            try
            {
                var users = await userService.FindByNameAsync(name);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("login")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListByLogin([FromQuery] string login)
        {
            try
            {
                var users = await userService.FindByLoginAsync(login);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FindByIdAsync(string id)
        {
            try
            {
                var user = await userService.FindByIdAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            var result = await userService.UpdateUserAsync(dto, user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await userManager.GetUserAsync(User);

            var result = await userService.DeleteUserAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Deleted");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user is null)
            {
                return NotFound("Incorrect Id");
            }

            var result = await userService.DeleteUserAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Deleted");
        }
    }
}
