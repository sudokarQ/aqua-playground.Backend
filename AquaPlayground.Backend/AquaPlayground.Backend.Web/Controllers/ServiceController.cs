using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaPlayground.Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }


        [HttpGet()]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var services = await _serviceService.GetAllAsync();

                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                Response.Headers.Add("Access-Control-Allow-Methods", "GET");


                return Ok(services);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("name")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListByName([FromQuery] string name)
        {
            try
            {
                var services = await _serviceService.GetListByNameAsync(name);

                return Ok(services);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(ServicePostDto servicePostDto)
        {
            try
            {
                await _serviceService.CreateAsync(servicePostDto);

                return Ok();
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
                await _serviceService.RemoveAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(ServicePutDto dto)
        {
            try
            {
                await _serviceService.UpdateAsync(dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            try
            {
                var serviceDto = await _serviceService.FindByIdAsync(id);

                return Ok(serviceDto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
