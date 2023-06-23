namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    using Common.Models.Dto.User;

    public interface IAuthService
    {
        Task<bool> ValidateUser(UserLoginDto loginUserDto);

        Task<string> CreateToken();

        Task<bool> IsAdmin(UserLoginDto loginUserDto);
    }
}
