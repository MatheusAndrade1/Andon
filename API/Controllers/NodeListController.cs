using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NodeListController : BaseApiController
    {
        private readonly INodeListRepository _nodeListRepository;
        private readonly IMapper _mapper;
        public NodeListController(INodeListRepository nodeListRepository, IMapper mapper)
        {
            _mapper = mapper;
            _nodeListRepository = nodeListRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NodeListDto>>> GetNodeLists()
        {
            return Ok(await _nodeListRepository.GetNodeListsAsync());

        }

        [HttpGet("{entityId}")]
        public async Task<ActionResult<NodeListGetDto>> GetNodeList(string entityId)
        {
            var NodeList = await _nodeListRepository.GetNodeListByEntityIdAsync(entityId);

            if (NodeList == null) return NotFound();

            var NodeListDto = new NodeListDto
            {
                entityId = NodeList.entityId,
                name = NodeList.name,
                hierarchyDefinitionId = NodeList.hierarchyDefinitionId,
                hierarchyId = NodeList.hierarchyId,
                parentEntityId = NodeList.parentEntityId,
                path = NodeList.path
            };

            return _nodeListRepository.FormatNodeList(NodeListDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<NodeList>> Register(NodeListDto registerDto)
        {
            if (await _nodeListRepository.NodeListExists(registerDto.entityId)) return BadRequest("NodeList already exists!");

            var NodeList = new NodeList
            {
                entityId = registerDto.entityId,
                name = registerDto.name,
                hierarchyDefinitionId = registerDto.hierarchyDefinitionId,
                hierarchyId = registerDto.hierarchyId,
                parentEntityId = registerDto.parentEntityId,
                path = registerDto.path
            };

            _nodeListRepository.Add(NodeList);
            await _nodeListRepository.SaveAllAsync();

            return NodeList;
        }

        [HttpPut("{entityId}")]
        public async Task<ActionResult> UpdateNodeList(string entityId, NodeListDto NodeListDto)
        {
            var NodeList = await _nodeListRepository.GetNodeListByEntityIdAsync(entityId);

            if(NodeList == null) return NotFound();

            _mapper.Map(NodeListDto, NodeList);

            _nodeListRepository.Update(NodeList);

            if(await _nodeListRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update NodeList!");
        }

        [HttpDelete("{entityId}")]
        public async Task<ActionResult> DeleteNodeList(string entityId)
        {
            var NodeList = await _nodeListRepository.GetNodeListByEntityIdAsync(entityId);

            if(NodeList == null) return NotFound();

            _nodeListRepository.RemoveNodeList(NodeList);

            if(await _nodeListRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete NodeList!");
        }
    }
}