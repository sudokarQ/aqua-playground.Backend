using AquaPlayground.Backend.Common.Models.Dto.Service;
using AquaPlayground.Backend.Common.Models.Dto.User;
using AquaPlayground.Backend.Common.Models.Entity;
using AutoMapper;

namespace AquaPlayground.Backend.Web.Configurations
{
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
        }
    }
}
