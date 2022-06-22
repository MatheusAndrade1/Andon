using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface INodeListRepository
    {
        Task<NodeListDto> GetNodeListAsync(int id);
        Task<bool> SaveAllAsync();
        void Add(NodeList NodeList);
         Task<List<NodeListGetDto>> GetNodeListsAsync();
         void Update(NodeList NodeList);
         Task<NodeList> GetNodeListByEntityIdAsync(string entityId);
         Task<bool> NodeListExists(string type);
         void RemoveNodeList(NodeList NodeList);
         NodeListGetDto FormatNodeList(NodeListDto NodeListDto);
         bool IsChild(string type);
         Task<List<NodeListGetDto>> GetNodeTreeListAsync(string entityId);
    }
}