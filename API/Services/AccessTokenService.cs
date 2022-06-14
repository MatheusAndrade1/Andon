using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        public AccessTokenService(IConfiguration config, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _config = config;
            _userManager = userManager;
            _tokenService = tokenService;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public async Task<String> AccessToken(AppUser user)
        {
            var claims = new List<Claim> //Creating a new claim with the user username
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role))); // Selecting the role from a list of roles

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); //Creates the credentials

            return _tokenService.CreateToken(user,
                                            "JwtAccessExpirationMinutes", 
                                            creds, 
                                            claims);
        }

        //     public async Task<ActionResult> DeleteToken(int id)
        //     {

        //     }
        // }
    }
}