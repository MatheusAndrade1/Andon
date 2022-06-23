using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
            return Ok(await _andonRepository.GetAndonsAsync());
        }

        [HttpGet("node/{entityId}")]
        public async Task<ActionResult<List<AndonDto>>> GetAndonsByEntityId(string entityId)
        {
            return Ok(await _andonRepository.GetAndonsByEntityIdAsync(entityId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AndonDto>> GetAndon(int id)
        {
            var andons = await _andonRepository.GetAndonByIdAsync(id);

            if (andons == null) return NotFound();

            return _mapper.Map<AndonDto>(andons);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Andon>> Register(AndonRegisterDto registerDto)
        {
            if (await _andonRepository.AndonExists(registerDto.type)) return BadRequest("Andon already exists!");

            var andon = new Andon
            {
                type = registerDto.type,
                warnCount = registerDto.warnCount,
                alarmCount = registerDto.alarmCount,
                entityId = registerDto.entityId
            };

            _andonRepository.Add(andon);
            await _andonRepository.SaveAllAsync();

            return andon;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAndon(int id, AndonRegisterDto andonUpdateDto)
        {
            var andon = await _andonRepository.GetAndonByIdAsync(id);

            if (andon == null) return NotFound();

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