namespace AquaPlayground.Backend.Web.Controllers
{
    using BuisnessLayer.Intefaces;

    using Common.Models.Entity;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        private readonly UserManager<User> userManager;

        public CartController(ICartService cartService, UserManager<User> userManager)
        {
            this.cartService = cartService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            var user = await userManager.GetUserAsync(User);

            try
            {
                var orders = await cartService.GetClientCart(user.Id);

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
            var user = await userManager.GetUserAsync(User);

            try
            {
                await cartService.OrderFromCart(user.Id, adress);

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
            var user = await userManager.GetUserAsync(User);

            try
            {
                await cartService.AddServiceToCart(id, user.Id);

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
            var user = await userManager.GetUserAsync(User);

            try
            {
                await cartService.RemoveServiceFromCart(id, user.Id);

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
