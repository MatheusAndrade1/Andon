using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface INodeListRepository
    {
        Task<NodeListDto> GetNodeAsync(int id);
        Task<NodeList> GetNodeByIdAsync(int id);
        Task<bool> SaveAllAsync();
        void AddNodeList(NodeList node);
        void UpdateNodeList(NodeList node);
        void RemoveNodeList(NodeList node);
        Task<IEnumerable<NodeListDto>> GetNodeListsAsync();
    }
}