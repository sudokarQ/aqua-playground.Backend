using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.User;
using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<UserGetDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            
            var result = _mapper.Map<List<UserGetDto>>(users);

            return result;
        }

        public async Task<UserGetDto> FindByIdAsync(string id)
        {
            var user = await _userRepository.FindByIdAsync(id);

            var result = _mapper.Map<UserGetDto>(user);

            return result;
        }

        public async Task<List<UserGetDto>> FindByLoginAsync(string login)
        {
            var users = await _userRepository.FindByLoginAsync(login);

            var result = _mapper.Map<List<UserGetDto>>(users);

            return result;
        }

        public async Task<List<UserGetDto>> FindByNameAsync(string name)
        {
            var users = await _userRepository.FindByNameAsync(name);

            var result = _mapper.Map<List<UserGetDto>>(users);

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto dto, User user)
        {
            if (await _userRepository.AnyAsync(u => u.Email == dto.Email))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email is already taken" });
            }

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);

                user.PasswordHash = newPasswordHash ?? user.PasswordHash;
            }

            if (!string.IsNullOrEmpty(dto.Name) && !string.IsNullOrWhiteSpace(dto.Name))
            {
                user.Name = dto.Name;
            }

            if (!string.IsNullOrEmpty(dto.Surname) && !string.IsNullOrWhiteSpace(dto.Surname))
            {
                user.Surname = dto.Surname;
            }

            user.Email = dto.Email ?? user.Email;
            user.UserName = user.Email;

            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result;
        }
    }
}
