namespace AquaPlayground.Backend.Web.Configurations
{
    using AutoMapper;

    using Common.Models.Dto.Order;
    using Common.Models.Dto.Promotion;
    using Common.Models.Dto.Service;
    using Common.Models.Dto.User;
    using Common.Models.Entity;

    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserGetDto>().ReverseMap();

            CreateMap<Service, ServicePostDto>().ReverseMap();
            CreateMap<Service, ServiceGetDto>().ReverseMap();
            CreateMap<Service, ServicePutDto>().ReverseMap();
            CreateMap<Service, ServiceSearchGetDto>().ReverseMap();

            CreateMap<Promotion, PromotionPostDto>().ReverseMap();
            CreateMap<Promotion, PromotionGetDto>().ReverseMap();
            CreateMap<Promotion, PromotionPutDto>().ReverseMap();

            CreateMap<Order, OrderPostDto>().ReverseMap();
            CreateMap<Order, OrderGetDto>().ReverseMap();
        }
    }
}
