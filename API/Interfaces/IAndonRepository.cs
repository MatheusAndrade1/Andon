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
        Task<List<AndonGetDto>> GetAndonsAsync();
        Task<AndonDto> GetAndonAsync(int id);
        Task<bool> AndonExists(string type);
        Task<Andon> GetAndonByEntityIdAsync(string entityId);
        AndonGetDto FormatAndon(AndonDto andonDto);
    }
}