using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<UserRegisterDto> GetUserAsync(int id);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<bool> SaveAllAsync();
        void AddUser(AppUser user);
        Task<AppUser> GetUserSinglePrDefaultAsync(UserRegisterDto registerDto);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<bool> UserExists(string username);
    }
}