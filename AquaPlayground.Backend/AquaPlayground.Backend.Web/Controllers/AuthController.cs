namespace AquaPlayground.Backend.Web.Controllers
{
    using AutoMapper;

    using BuisnessLayer.Intefaces;

    using Common.Models.Dto.User;
    using Common.Models.Entity;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        private readonly IMapper mapper;

        private readonly IAuthService authManager;

        public AuthController(UserManager<User> userManager, IMapper mapper, IAuthService authManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.authManager = authManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = mapper.Map<User>(dto);
                user.UserName = dto.Email;

                var result = await userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await userManager.AddToRolesAsync(user, dto.Roles);

                return Accepted();
            }
            catch (Exception)
            {
                return Problem("Something went wrong", statusCode: 500);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "Post");


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await authManager.ValidateUser(dto))
                {
                    return Unauthorized(dto);
                }

                return Accepted(new { Token = await authManager.CreateToken(), IsAdmin = await authManager.IsAdmin(dto) });
            }
            catch (Exception)
            {
                return Problem("Something went wrong with login", statusCode: 500);
            }
        }
    }
}
