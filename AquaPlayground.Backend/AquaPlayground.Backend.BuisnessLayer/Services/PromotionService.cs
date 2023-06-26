namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Common.Models.Dto.Promotion;
    using Common.Models.Entity;

    using DataLayer.Repositories.Interfaces;

    using Intefaces;

    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository promotionRepository;

        private readonly IMapper mapper;

        public PromotionService(IPromotionRepository promotionRepository, IMapper mapper)
        {
            this.promotionRepository = promotionRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(PromotionPostDto promotion)
        {
            if (Validation(promotion))
            {
                var result = mapper.Map<Promotion>(promotion);

                await promotionRepository.CreateAsync(result);
            }
            else
            {
                throw new ValidationException("Validation declined");
            }
        }
        public async Task<List<PromotionGetDto>> GetAllAsync()
        {
            var promotions = await promotionRepository.GetAllAsync();

            var result = mapper.Map<List<PromotionGetDto>>(promotions);

            return result;
        }

        public async Task<List<PromotionGetDto>> GetListByNameAsync(string name)
        {
            var promotions = await promotionRepository.GetListByNameAsync(name);

            var result = mapper.Map<List<PromotionGetDto>>(promotions);

            return result;
        }

        public async Task<PromotionGetDto> FindByIdAsync(Guid id)
        {
            var promotion = await promotionRepository.FindByIdAsync(id);

            var result = mapper.Map<PromotionGetDto>(promotion);

            return result;
        }

        public async Task RemoveAsync(Guid id)
        {
            var promotion = await promotionRepository.FindByIdAsync(id);

            if (promotion is null)
            {
                throw new ArgumentNullException("Promotinon not found");
            }

            await promotionRepository.RemoveAsync(promotion);
        }

        public async Task UpdateAsync(PromotionPutDto dto)
        {
            var promotion = await promotionRepository.FindByIdAsync(dto.Id);

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

            await promotionRepository.UpdateAsync(promotion);
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
