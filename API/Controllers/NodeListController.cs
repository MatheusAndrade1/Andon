using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class NodeListController : BaseApiController
    {
        private readonly INodeListRepository _nodeListRepository;
        private readonly IMapper _mapper;
        public NodeListController(INodeListRepository nodeListRepository, IMapper mapper)
        {
            _mapper = mapper;
            _nodeListRepository = nodeListRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NodeListDto>> GetNode(int id)
        {
            var node = await _nodeListRepository.GetNodeByIdAsync(id);
            return _mapper.Map<NodeListDto>(node);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NodeListDto>>> GetNodeLists()
        {
            return Ok(await _nodeListRepository.GetNodeListsAsync());
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppNodeList>> Register(NodeListRegisterDto registerDto)
        {
            var node = new AppNodeList
            {
                path = registerDto.path,
                name = registerDto.name,
                nodeType = registerDto.nodeType
            };
            _nodeListRepository.AddNodeList(node);
            await _nodeListRepository.SaveAllAsync();

            return node;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNodeList(int id, NodeListRegisterDto nodeListUpdateDto)
        {
            var node = await _nodeListRepository.GetNodeByIdAsync(id);

            _mapper.Map(nodeListUpdateDto, node);

            _nodeListRepository.UpdateNodeList(node);

            if(await _nodeListRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update node list!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNodeList(int id)
        {
            var node = await _nodeListRepository.GetNodeByIdAsync(id);

            if(node == null) return NotFound();

            _nodeListRepository.RemoveNodeList(node);

            if(await _nodeListRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete node list!");
        }
    }
}