using API.Entities;

namespace API.Interfaces
{
    public interface IAccessTokenService
    {
        Task<String> AccessToken(AppUser user);
    }
}