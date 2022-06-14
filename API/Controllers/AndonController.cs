using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
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

        [HttpGet("{entityId}")]
        public async Task<ActionResult<AndonGetDto>> GetAndon(string entityId)
        {
            var andon = await _andonRepository.GetAndonByEntityIdAsync(entityId);

            if (andon.parentEntityId != null)
            {
                var andonChild = await _andonRepository.GetAndonByEntityIdAsync(andon.parentEntityId);
            }
            // return _mapper.Map<AndonDto>(andon);

            var result = new AndonGetDto
            {
                entityId = andon.entityId,
                name = andon.name,
                paths = new Dictionary<string,string>
                {
                    {"hierarchyDefinitionId", andon.hierarchyDefinitionId},
                    {"hierarchyId", andon.hierarchyId},
                    {"parentEntityId", andon.parentEntityId},
                    {"path", andon.path},
                }
            };

            return result;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Andon>> Register(AndonDto registerDto)
        {
            if (await _andonRepository.AndonExists(registerDto.entityId)) return BadRequest("Andon already exists!");

            var andon = new Andon
            {
                entityId = registerDto.entityId,
                name = registerDto.name,
                hierarchyDefinitionId = registerDto.hierarchyDefinitionId,
                hierarchyId = registerDto.hierarchyId,
                parentEntityId = registerDto.parentEntityId,
                path = registerDto.path
            };

            //_context.Andon.Add(andon); // Here we get the date
            //await _context.SaveChangesAsync(); // Here we actually apply the changes to the database
            _andonRepository.Add(andon);
            await _andonRepository.SaveAllAsync();

            return andon;
        }

        [HttpPut("{entityId}")]
        public async Task<ActionResult> UpdateAndon(string entityId, AndonUpdateDto andonUpdateDto)
        {
            var andon = await _andonRepository.GetAndonByEntityIdAsync(entityId);

            _mapper.Map(andonUpdateDto, andon);

            _andonRepository.Update(andon);

            if(await _andonRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update andon!");
        }

        [HttpDelete("{entityId}")]
        public async Task<ActionResult> DeleteAndon(string entityId)
        {
            var andon = await _andonRepository.GetAndonByEntityIdAsync(entityId);

            if(andon == null) return NotFound();

            _andonRepository.RemoveAndon(andon);

            if(await _andonRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete andon!");
        }
    }
}