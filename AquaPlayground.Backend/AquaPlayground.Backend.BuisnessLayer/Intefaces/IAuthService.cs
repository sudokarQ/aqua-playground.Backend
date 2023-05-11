using AquaPlayground.Backend.Common.Models.Dto.User;

namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUser(UserLoginDto loginUserDto);
        Task<string> CreateToken();
    }
}
