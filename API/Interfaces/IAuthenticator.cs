using API.Entities;

namespace API.Interfaces
{
    public interface IAuthenticator
    {
         Task<List<string>> Authenticate(AppUser user);
         bool ValidateRefreshToken(string refreshToken);
    }
}