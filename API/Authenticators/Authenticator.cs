using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _config;
        public Authenticator(IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService,
        IRefreshTokenRepository refreshTokenRepository, IConfiguration config)
        {
            _config = config;
            _refreshTokenRepository = refreshTokenRepository;
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }

    public async Task<List<string>> Authenticate(AppUser user)
    {
        var accessToken = await _accessTokenService.AccessToken(user);
        var refreshToken = _refreshTokenService.RefreshToken(user);

        // Saving the refresh into the database
        RefreshToken refreshTokenDTO = new RefreshToken()
        {
            Token = refreshToken,
            UserId = user.Id
        };

        _refreshTokenRepository.Create(refreshTokenDTO);
        _refreshTokenRepository.SaveTokenChanges();

        return new List<string>(){
                accessToken,
                refreshToken
            };
    }

    public bool ValidateRefreshToken(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;
        TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])),
                    ValidateIssuer = false, //API server
                    ValidateAudience = false, //front end client
                    ClockSkew = TimeSpan.Zero, // The token expires exactly at the time set by JwtExpirationMinutes + TimeNow
                };
        try
        {
            tokenHandler.ValidateToken(refreshToken, validationParameters, out validatedToken);
            return true;
        }
        catch (System.Exception)
        {
            
            return false;
        }
        
    }

    }
}