namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    using Common.Models.Dto.User;
    using Common.Models.Entity;

    using Microsoft.AspNetCore.Identity;

    public interface IUserService
    {
        Task<List<UserGetDto>> GetAllAsync();

        Task<UserGetDto> FindByIdAsync(string id);

        Task<List<UserGetDto>> FindByLoginAsync(string login);

        Task<List<UserGetDto>> FindByNameAsync(string name);

        Task<IdentityResult> UpdateUserAsync(UserUpdateDto dto, User user);

        Task<IdentityResult> DeleteUserAsync(User user);
    }
}
