using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.Promotion;
using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        private readonly IMapper _mapper;

        public PromotionService(IPromotionRepository promotionRepository, IMapper mapper)
        {
            _promotionRepository = promotionRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PromotionPostDto promotion)
        {
            if (Validation(promotion))
            {
                var result = _mapper.Map<Promotion>(promotion);

                await _promotionRepository.CreateAsync(result);
            }
            else
            {
                throw new ValidationException("Validation declined");
            }
        }
        public async Task<List<PromotionGetDto>> GetAllAsync()
        {
            var promotions = await _promotionRepository.GetAllAsync();

            var result = _mapper.Map<List<PromotionGetDto>>(promotions);

            return result;
        }

        public async Task<List<PromotionGetDto>> GetListByNameAsync(string name)
        {
            var promotions = await _promotionRepository.GetListByNameAsync(name);

            var result = _mapper.Map<List<PromotionGetDto>>(promotions);

            return result;
        }

        public async Task<PromotionGetDto> FindByIdAsync(Guid id)
        {
            var promotion = await _promotionRepository.FindByIdAsync(id);

            var result = _mapper.Map<PromotionGetDto>(promotion);

            return result;
        }

        public async Task RemoveAsync(Guid id)
        {
            var promotion = await _promotionRepository.FindByIdAsync(id);

            if (promotion is null)
            {
                throw new ArgumentNullException("Promotinon not found");
            }

            await _promotionRepository.RemoveAsync(promotion);
        }

        public async Task UpdateAsync(PromotionPutDto dto)
        {
            var promotion = await _promotionRepository.FindByIdAsync(dto.Id);

            if (promotion is null)
            {
                throw new ArgumentException("Promotion not found");
            }

            if (!Validation(dto))
            {
                throw new ValidationException("Validation declined");
            }

            promotion.Name = dto.Name ?? promotion.Name;
            promotion.Description = dto.Description ?? promotion.Description;
            promotion.BeginDate = dto.BeginDate ?? promotion.BeginDate;
            promotion.EndDate = dto.EndDate ?? promotion.EndDate;
            promotion.DiscountPercent = dto.DiscountPercent ?? promotion.DiscountPercent;

            await _promotionRepository.UpdateAsync(promotion);
        }

        public static bool Validation(IPromotionDto dto)
        {
            if (dto.BeginDate > dto.EndDate || dto.BeginDate < DateTime.Now || dto.DiscountPercent > 100)
            {
                return false;
            }

            return true;
        }
    }
}
