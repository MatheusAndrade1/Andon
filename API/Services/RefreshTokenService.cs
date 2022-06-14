using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        public RefreshTokenService(IConfiguration config, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string RefreshToken(AppUser user)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            return _tokenService.CreateToken(user, "JwtRefreshExpirationMinutes", creds);
        }
    }
}