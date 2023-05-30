using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AquaPlayground.Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        private readonly IServiceService _serviceService;

        private readonly UserManager<User> _userManager;

        public CartController(ICartService cartService, UserManager<User> userManager, IServiceService serviceService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _serviceService = serviceService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                var orders = await _cartService.GetClientCart(user.Id);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("Order")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> OrderFromCart(string? adress)
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                await _cartService.OrderFromCart(user.Id, adress);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPut("AddService")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> AddToCart(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                await _cartService.AddServiceToCart(id, user.Id);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("RemoveService")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                await _cartService.RemoveServiceFromCart(id, user.Id);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
