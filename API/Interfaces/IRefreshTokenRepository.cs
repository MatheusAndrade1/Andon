using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IRefreshTokenRepository
    {
         void Create(RefreshToken refreshToken);
         RefreshTokenDto GetByToken(string token);
         void SaveTokenChanges();
         void DeleteByToken(string token);
         RefreshToken GetByUserID(int id);
         void Delete(RefreshToken refreshToken);
    }
}