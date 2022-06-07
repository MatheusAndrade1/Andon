using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAndonRepository
    {
        void Update(AppAndon andon);
        void Add(AppAndon andon) ;
        void Remove(AppAndon andon) ;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AndonDto>> GetAndonsAsync();
        Task<AndonDto> GetAndonAsync(int id);
        Task<AppAndon> GetAndonByIdAsync(int id);
        Task<bool> AndonExists(string type);
    }
}