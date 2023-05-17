using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.Promotion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaPlayground.Backend.Web.Controllers
{
    [ApiController]
    public class PromotionController : Controller
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet("GetAllPromotions")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var promotions = await _promotionService.GetAllAsync();

                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetPromotionsByName")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListByName(string name)
        {
            try
            {
                var promotions = await _promotionService.GetListByNameAsync(name);

                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("CreatePromotion")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(PromotionPostDto promotionPostDto)
        {
            try
            {
                await _promotionService.CreateAsync(promotionPostDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("DeletePromotion")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                await _promotionService.RemoveAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("UpdatePromotion")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(PromotionPutDto dto)
        {
            try
            {
                await _promotionService.UpdateAsync(dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("FindPromotion")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            try
            {
                var promotionDto = await _promotionService.FindByIdAsync(id);

                return Ok(promotionDto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
