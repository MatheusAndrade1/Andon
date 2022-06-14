using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
        }

        //
        public string CreateToken(AppUser user, string time, SigningCredentials creds = null, List<Claim> claims = null)
        {
            var tokenDescriptor = new SecurityTokenDescriptor // Describing the token
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(float.Parse(_config[time])),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        //     public async Task<ActionResult> DeleteToken(int id)
        //     {

        //     }
        // }
    }
}