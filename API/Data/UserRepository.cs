using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public void AddUser(AppUser user)
        {
            _context.Users.Add(user);
        }

        public async Task<UserRegisterDto> GetUserAsync(int id)
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .ProjectTo<UserRegisterDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<AppUser> GetUserSinglePrDefaultAsync(UserRegisterDto registerDto)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == registerDto.Username);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<AppUser>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}