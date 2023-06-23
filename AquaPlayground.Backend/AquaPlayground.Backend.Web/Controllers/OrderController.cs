namespace AquaPlayground.Backend.Web.Controllers
{
    using BuisnessLayer.Intefaces;

    using Common.Models.Dto.Order;
    using Common.Models.Entity;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        private readonly UserManager<User> userManager;

        public OrderController(IOrderService orderService, UserManager<User> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> CreateAsync(OrderPostDto orderPostDto)
        {
            var user = await userManager.GetUserAsync(User);

            try
            {
                await orderService.CreateAsync(orderPostDto, user.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var orders = await orderService.GetAllAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("userId")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListByUserIdAsync([FromQuery] string userId)
        {
            try
            {
                var orders = await orderService.GetListByUserIdAsync(userId);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("Dates")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListByDatesAsync(DateTime? begin, DateTime? end, string? userId)
        {
            try
            {
                var orders = await orderService.GetListByDatesAsync(begin, end, userId);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("MyOrders")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetListByDatesAsync(DateTime? begin, DateTime? end)
        {
            var user = await userManager.GetUserAsync(User);

            try
            {
                var orders = await orderService.GetListByDatesAsync(begin, end, user.Id);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            try
            {
                var orderDto = await orderService.FindByIdAsync(id);

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await orderService.RemoveAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
