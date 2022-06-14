using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AndonRepository : IAndonRepository
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public AndonRepository(DataContext context, IMapper mapper)
        {
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

         public async Task<List<AndonGetDto>> GetAndonsAsync()
        {
            var result = await _context.Andon
                .ProjectTo<AndonDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var items = new List<AndonGetDto>();

            // Gets the value of each andon and adds to the list
            foreach (var item in result)
            {
                var andons = new AndonGetDto
                {
                    entityId = item.entityId,
                    name = item.name,
                    paths = new Dictionary<string,string>
                    {
                        {"hierarchyDefinitionId", item.hierarchyDefinitionId},
                        {"hierarchyId", item.hierarchyId},
                        {"parentEntityId", item.parentEntityId},
                        {"path", item.path},
                    }
                };
                
                items.Add(andons);
            }

            return items;
        }

        public void Update(Andon andon)
        {
            _context.Entry(andon).State = EntityState.Modified;
        }

        public async Task<Andon> GetAndonByEntityIdAsync(string entityId)
        {
            return await _context.Andon
                .Where(x => x.entityId == entityId)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> AndonExists(string type)
        {
            return await _context.Andon.AnyAsync(x => x.entityId == type);
        }

        public void RemoveAndon(Andon andon)
        {
            _context.Andon.Remove(andon);
        }
    }
}