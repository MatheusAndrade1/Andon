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
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public NodeListRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<NodeListDto> GetNodeAsync(int id)
        {
            return await _context.NodeList.
                Where(x => x.id == id)
                .ProjectTo<NodeListDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<NodeList> GetNodeByIdAsync(int id)
        {
            return await _context.NodeList.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void AddNodeList(NodeList node) // Testing
        {
            _context.NodeList.Add(node);
        }

        public async Task<IEnumerable<NodeListDto>> GetNodeListsAsync()
        {
            return await _context.NodeList
                .ProjectTo<NodeListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void UpdateNodeList(NodeList node)
        {
            _context.Entry(node).State = EntityState.Modified;
        }

        public void RemoveNodeList(NodeList node)
        {
            _context.NodeList.Remove(node);
        }
    }
}