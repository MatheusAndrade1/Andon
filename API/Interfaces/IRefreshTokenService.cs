using API.Entities;

namespace API.Interfaces
{
    public interface IRefreshTokenService
    {
        string RefreshToken(AppUser user);
    }
}