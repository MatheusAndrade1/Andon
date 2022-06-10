using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserRepository _userRepository;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(UserRegisterDto registerDto)
    {
        if (await _userRepository.UserExists(registerDto.Username)) return BadRequest("UserName taken!");

        var user = _mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    [HttpPost("registerAdmin")]
    public async Task<ActionResult<UserDto>> RegisterAdmin(UserRegisterDto registerDto)
    {
        if (await _userRepository.UserExists(registerDto.Username)) return BadRequest("UserName taken!");

        var user = _mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);
        
        var roleResult = await _userManager.AddToRoleAsync(user, "Admin"); // Assigning the user to the admin role

        if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(UserRegisterDto loginDto)
    {
        var user = await _userRepository.GetUserSinglePrDefaultAsync(loginDto);

        _mapper.Map(loginDto, user);

        if (user == null) return Unauthorized("Invalid username!");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if(!result.Succeeded) return Unauthorized();

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }
}
}