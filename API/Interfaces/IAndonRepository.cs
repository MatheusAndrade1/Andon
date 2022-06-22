using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAndonRepository
    {
        void Update(Andon andon);
        void Add(Andon andon) ;
        void RemoveAndon(Andon andon) ;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AndonDto>> GetAndonsAsync();
        Task<AndonDto> GetAndonAsync(int id);
        Task<Andon> GetAndonByIdAsync(int id);
        Task<bool> AndonExists(string type);
        Task<List<Andon>> GetAndonsByEntityIdAsync(string entityId);
    }
}