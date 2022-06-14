using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RefreshTokenRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // private readonly List<RefreshToken> _refreshToken = new List<RefreshToken>();
        public void Create(RefreshToken refreshToken)
        {
            _context.RefreshToken.Add(refreshToken);
        }

        public void DeleteByToken(string token)
        {
            RefreshToken refreshToken = _context.RefreshToken.Where(x => x.Token == token).FirstOrDefault();
            _context.RefreshToken.Remove(refreshToken);
        }

        public void Delete(RefreshToken refreshToken)
        {
            _context.RefreshToken.Remove(refreshToken);
        }
        
        public void SaveTokenChanges()
        {
            _context.SaveChanges();
        }
        

        public RefreshTokenDto GetByToken(string token)
        {
            return _context.RefreshToken
                .Where(x => x.Token == token)
                .ProjectTo<RefreshTokenDto>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public RefreshToken GetByUserID(int id)
        {
            return _context.RefreshToken
                .Where(x => x.UserId == id)
                .FirstOrDefault();
        }
    }
}