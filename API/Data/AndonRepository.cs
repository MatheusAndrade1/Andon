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
            // return await _context.Andon.FindAsync(id);
            return await _context.Andon
                .Where(x => x.id == id)
                .ProjectTo<AndonDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Add(AppAndon andon) // Testing
        {
            _context.Andon.Add(andon);
        }

         public async Task<IEnumerable<AndonDto>> GetAndonsAsync()
        {
            return await _context.Andon
                .ProjectTo<AndonDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void Update(AppAndon andon)
        {
            _context.Entry(andon).State = EntityState.Modified;
        }

        public async Task<AppAndon> GetAndonByIdAsync(int id)
        {
            return await _context.Andon.FindAsync(id);
        }

        public async Task<bool> AndonExists(string type)
        {
            return await _context.Andon.AnyAsync(x => x.type == type);
        }

        public void Remove(AppAndon andon)
        {
            _context.Remove(andon);
        }
    }
}