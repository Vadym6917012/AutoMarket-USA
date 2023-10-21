using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyTypeController : ControllerBase
    {
        private readonly Repository<BodyType> _repository;
        private readonly IMapper _mapper;

        public BodyTypeController(Repository<BodyType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BodyTypeDTO>> GetBodyTypes()
        {
            var bodyTypes = await _repository.GetAllAsync();
            var bodyTypeDTOs = _mapper.Map<IEnumerable<BodyTypeDTO>>(bodyTypes);

            return bodyTypeDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<BodyTypeDTO> GetById(int id)
        {
            var bodyType = await _repository.GetByIdAsync(id);
            var bodyTypeDTO = _mapper.Map<BodyTypeDTO>(bodyType);

            return bodyTypeDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBodyType([FromBody] BodyTypeDTO bodyTypeDTO)
        {
            if (bodyTypeDTO == null)
            {
                return BadRequest("Invalid body type data");
            }

            var bodyType = _mapper.Map<BodyType>(bodyTypeDTO);
            await _repository.AddAsync(bodyType);

            // Return the created product
            return CreatedAtAction("GetById", new { id = bodyType.Id }, _mapper.Map<BodyTypeDTO>(bodyType));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBodyType(int id, [FromBody] BodyTypeDTO bodyTypeDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (bodyTypeDTO == null)
            {
                return BadRequest("Invalid body type data");
            }

            _mapper.Map(bodyTypeDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<BodyTypeDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBodyType(int id)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(existingEntity);

            return NoContent();
        }
    }
}
