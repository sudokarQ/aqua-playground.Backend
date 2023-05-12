using AquaPlayground.Backend.Common.Models.Dto.User;
using AquaPlayground.Backend.Common.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
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
