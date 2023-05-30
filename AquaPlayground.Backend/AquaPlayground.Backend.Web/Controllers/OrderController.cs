using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.Order;
using AquaPlayground.Backend.Common.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AquaPlayground.Backend.Web.Controllers
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        private readonly UserManager<User> _userManager;

        public OrderController(IOrderService orderService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpPost("CreateOrder")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> CreateAsync(OrderPostDto orderPostDto)
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                await _orderService.CreateAsync(orderPostDto, user.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var orders = await _orderService.GetAllAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetListByUserId")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListByUserIdAsync(string id)
        {
            try
            {
                var orders = await _orderService.GetListByUserIdAsync(id);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetListByDate")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListByDatesAsync(DateTime? begin, DateTime? end, string? id)
        {
            try
            {
                var orders = await _orderService.GetListByDatesAsync(begin, end, id);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetMyOrders")]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetListByDatesAsync(DateTime? begin, DateTime? end)
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                var orders = await _orderService.GetListByDatesAsync(begin, end, user.Id);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("FindOrder")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            try
            {
                var orderDto = await _orderService.FindByIdAsync(id);

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("DeleteOrder")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _orderService.RemoveAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
