using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class NodeListRepository : INodeListRepository
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public NodeListRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<NodeListDto> GetNodeListAsync(int id)
        {
            return await _context.NodeList
                .Where(x => x.id == id)
                .ProjectTo<NodeListDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Add(NodeList NodeList) // Testing
        {
            _context.NodeList.Add(NodeList);
        }

         public async Task<List<NodeListGetDto>> GetNodeListsAsync()
        {
            var result = await _context.NodeList
                .ProjectTo<NodeListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var items = new List<NodeListGetDto>();

            // Gets the value of each NodeList and adds to the list
            foreach (var item in result)
            {
                items.Add(FormatNodeList(item)); 
            }
            return items;
        }

        public void Update(NodeList NodeList)
        {
            _context.Entry(NodeList).State = EntityState.Modified;
        }

        public async Task<NodeList> GetNodeListByEntityIdAsync(string entityId)
        {
            return await _context.NodeList
                .Where(x => x.entityId == entityId)
                // .ProjectTo<NodeListDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> NodeListExists(string type)
        {
            return await _context.NodeList.AnyAsync(x => x.entityId == type);
        }

        public void RemoveNodeList(NodeList NodeList)
        {
            _context.NodeList.Remove(NodeList);
        }

        public NodeListGetDto FormatNodeList(NodeListDto NodeListDto)
        {
            var NodeLists = new NodeListGetDto
            {
                entityId = NodeListDto.entityId,
                name = NodeListDto.name,
                type = NodeListDto.type,
                paths = new Dictionary<string,string>[]
                {
                    new Dictionary<string, string> 
                    {
                        {"hierarchyDefinitionId", NodeListDto.hierarchyDefinitionId},
                        {"hierarchyId", NodeListDto.hierarchyId},
                        {"parentEntityId", NodeListDto.parentEntityId},
                        {"path", NodeListDto.path},
                    }
                }
            };

            return NodeLists;
        }
    }
}