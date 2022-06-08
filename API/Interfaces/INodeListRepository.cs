using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface INodeListRepository
    {
        Task<NodeListDto> GetNodeAsync(int id);
        Task<AppNodeList> GetNodeByIdAsync(int id);
        Task<bool> SaveAllAsync();
        void AddNodeList(AppNodeList node);
        void UpdateNodeList(AppNodeList node);
        void RemoveNodeList(AppNodeList node);
        Task<IEnumerable<NodeListDto>> GetNodeListsAsync();
    }
}