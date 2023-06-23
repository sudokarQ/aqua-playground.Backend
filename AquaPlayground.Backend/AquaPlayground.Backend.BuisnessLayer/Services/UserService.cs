namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    using AutoMapper;

    using Common.Models.Dto.User;
    using Common.Models.Entity;

    using DataLayer.Repositories.Interfaces;

    using Intefaces;

    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        private readonly IMapper mapper;

        private readonly UserManager<User> userManager;

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<List<UserGetDto>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();

            var result = mapper.Map<List<UserGetDto>>(users);

            return result;
        }

        public async Task<UserGetDto> FindByIdAsync(string id)
        {
            var user = await userRepository.FindByIdAsync(id);

            var result = mapper.Map<UserGetDto>(user);

            return result;
        }

        public async Task<List<UserGetDto>> FindByLoginAsync(string login)
        {
            var users = await userRepository.FindByLoginAsync(login);

            var result = mapper.Map<List<UserGetDto>>(users);

            return result;
        }

        public async Task<List<UserGetDto>> FindByNameAsync(string name)
        {
            var users = await userRepository.FindByNameAsync(name);

            var result = mapper.Map<List<UserGetDto>>(users);

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto dto, User user)
        {
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                var newPasswordHash = userManager.PasswordHasher.HashPassword(user, dto.Password);

                user.PasswordHash = newPasswordHash ?? user.PasswordHash;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                user.Name = dto.Name;
            }

            if (!string.IsNullOrWhiteSpace(dto.Surname))
            {
                user.Surname = dto.Surname;
            }

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                user.PhoneNumber = dto.PhoneNumber;
            }

            var result = await userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            var result = await userManager.DeleteAsync(user);

            return result;
        }
    }
}
