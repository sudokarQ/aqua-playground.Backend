namespace AquaPlayground.Backend.Web.Controllers
{
    using BuisnessLayer.Intefaces;

    using Common.Models.Dto.Service;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }


        [HttpGet()]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var services = await serviceService.GetAllAsync();

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
                var services = await serviceService.GetListByNameAsync(name);

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
                await serviceService.CreateAsync(servicePostDto);

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
                await serviceService.RemoveAsync(id);

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
                await serviceService.UpdateAsync(dto);

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
                var serviceDto = await serviceService.FindByIdAsync(id);

                return Ok(serviceDto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
