using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AndonRepository : IAndonRepository
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly INodeListRepository _nodeListRepository;

        public AndonRepository(DataContext context, IMapper mapper, INodeListRepository nodeListRepository)
        {
            _nodeListRepository = nodeListRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<AndonDto> GetAndonAsync(int id)
        {
            return await _context.Andon
                .Where(x => x.id == id)
                .ProjectTo<AndonDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Add(Andon andon) // Testing
        {
            _context.Andon.Add(andon);
        }

        public async Task<IEnumerable<AndonDto>> GetAndonsAsync()
        {
            return await _context.Andon
                .ProjectTo<AndonDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<AndonGroupedDto>> GetAndonsByEntityIdAsync(string entityId)
        {
            var paramEntityId = new SqlParameter("entityId", entityId);

            // calls a function that returns all childs from the given entityId
            var sqlQuery = "SELECT * FROM GetChildFunc(@entityId)";

            return await _context.Andon
                .FromSqlRaw(sqlQuery, paramEntityId)
                .ProjectTo<AndonGroupedDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void Update(Andon andon)
        {
            _context.Entry(andon).State = EntityState.Modified;
        }

        public async Task<Andon> GetAndonByIdAsync(int id)
        {
            return await _context.Andon.FindAsync(id);
        }

        public async Task<bool> AndonExists(string type)
        {
            return await _context.Andon.AnyAsync(x => x.type == type);
        }

        public void RemoveAndon(Andon andon)
        {
            _context.Andon.Remove(andon);
        }
    }
}