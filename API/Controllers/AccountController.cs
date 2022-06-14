using System.Diagnostics;
using System.Security.Claims;
using API.Authenticators;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticator _authenticator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IUserRepository userRepository, IMapper mapper,
        IRefreshTokenRepository refreshTokenRepository, IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto registerDto)
        {
            if (await _userRepository.UserExists(registerDto.Username)) return BadRequest("UserName taken!");

            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRolesAsync(user, new[] { "Operator" }); // By default, adds the user to the operator role

            var response = await _authenticator.Authenticate(user);

            return new UserDto
            {
                Username = user.UserName,
                AccessToken = response[0],
                RefreshToken = response[1]
            };
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("registerAdmin")]
        public async Task<ActionResult<UserDto>> RegisterAdmin(UserRegisterDto registerDto)
        {
            if (await _userRepository.UserExists(registerDto.Username)) return BadRequest("UserName taken!");

            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRolesAsync(user, new[] { "Admin", "Operator" }); // Assigning the user to the admin role

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            var response = await _authenticator.Authenticate(user);

            return new UserDto
            {
                Username = user.UserName,
                AccessToken = response[0],
                RefreshToken = response[1]
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserRegisterDto loginDto)
        {
            var user = await _userRepository.GetUserSinglePrDefaultAsync(loginDto);

            _mapper.Map(loginDto, user);

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            var response = await _authenticator.Authenticate(user);

            return new UserDto
            {
                Username = user.UserName,
                AccessToken = response[0],
                RefreshToken = response[1]
            };
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh(RefreshRequestDto refreshToken)
        {
            var valid = _authenticator.ValidateRefreshToken(refreshToken.Token);
            
            if(!valid) return BadRequest("Token not valid!");

            RefreshTokenDto refreshTokenDTO = _refreshTokenRepository.GetByToken(refreshToken.Token);

            if (refreshTokenDTO == null) return NotFound("Token not found!");

            AppUser user = await _userRepository.GetUserByIdAsync(refreshTokenDTO.UserId);

            if (user == null) return NotFound("User not found!");

            var response = await _authenticator.Authenticate(user);

            _refreshTokenRepository.DeleteByToken(refreshToken.Token);
            _refreshTokenRepository.SaveTokenChanges();
            
            return Ok(new Dictionary<string, string> {
                {"accessToken", response[0]},
                {"refreshToken", response[1]}
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public string Logout()
        {
            // Uses the user id inside the access token to delete the refresh token
            // So he cannot refresh the access token
            string id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            int userID = int.Parse(id);

            RefreshToken refreshToken = _refreshTokenRepository.GetByUserID(userID);

            _refreshTokenRepository.Delete(refreshToken);

            _refreshTokenRepository.SaveTokenChanges();

            return ("Logged out!");

        }
    }
}