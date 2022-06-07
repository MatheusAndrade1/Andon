using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AndonController : BaseApiController
    {
        private readonly IAndonRepository _andonRepository;
        private readonly IMapper _mapper;
        public AndonController(IAndonRepository andonRepository, IMapper mapper)
        {
            _mapper = mapper;
            _andonRepository = andonRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AndonDto>>> GetAndons()
        {
            //var andons = await _andonRepository.GetAndonsAsync();
            //var andonsToReturn = _mapper.Map<IEnumerable<AndonDto>>(andons);
            //return Ok(andonsToReturn);
            return Ok(await _andonRepository.GetAndonsAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AndonDto>> GetAndon(int id)
        {
            var andons = await _andonRepository.GetAndonByIdAsync(id);
            return _mapper.Map<AndonDto>(andons);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppAndon>> Register(AndonRegisterDto registerDto)
        {
            if (await _andonRepository.AndonExists(registerDto.type)) return BadRequest("Andon already exists!");

            var andon = new AppAndon
            {
                type = registerDto.type,
                warnCount = registerDto.warnCount,
                alarmCount = registerDto.alarmCount
            };

            //_context.Andon.Add(andon); // Here we get the date
            //await _context.SaveChangesAsync(); // Here we actually apply the changes to the database
            _andonRepository.Add(andon);
            await _andonRepository.SaveAllAsync();

            return andon;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAndon(int id, AndonUpdateDto andonUpdateDto)
        {
            var andon = await _andonRepository.GetAndonByIdAsync(id);

            _mapper.Map(andonUpdateDto, andon);

            _andonRepository.Update(andon);

            if(await _andonRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update andon!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAndon(int id)
        {
            var andon = await _andonRepository.GetAndonByIdAsync(id);

            if(andon == null) return NotFound();

            _andonRepository.RemoveAndon(andon);

            if(await _andonRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete andon!");
        }
    }
}