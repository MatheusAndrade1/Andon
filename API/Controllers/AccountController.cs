using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountController(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto registerDto)
        {
            //if (await _userRepository.UserExists(registerDto.type)) return BadRequest("UserName taken!");
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _userRepository.AddUser(user);
            await _userRepository.SaveAllAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserRegisterDto registerDto)
        {
            var user = await _userRepository.GetUserSinglePrDefaultAsync(registerDto);

            _mapper.Map(registerDto, user);

            if (user == null) return Unauthorized("Invalid username!");

            //Comparing the password
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Wrong username/password!");
            }

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}