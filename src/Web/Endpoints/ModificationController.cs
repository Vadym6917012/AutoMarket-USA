using Application.DTOs.Modification;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModificationController : ControllerBase
    {
        public readonly IModificationRepository _repository;
        public readonly IMapper _mapper;

        public ModificationController(IModificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get-modifications")]
        public async Task<IEnumerable<ModificationDTO>> GetModifications()
        {
            var modifications = await _repository.GetAllAsync();
            var modificationDTOs = _mapper.Map<IEnumerable<ModificationDTO>>(modifications);

            return modificationDTOs.ToList();
        }

        [HttpGet("get-modification/{id}")]
        public async Task<ModificationDTO> GetById(int id)
        {
            var modification = await _repository.GetByIdAsync(id);
            var modificationDTO = _mapper.Map<ModificationDTO>(modification);

            return modificationDTO;
        }

        [HttpPost("create-modification")]
        public async Task<IActionResult> CreateModification([FromBody] ModificationDTO modificationDTO)
        {
            if (modificationDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var modification = _mapper.Map<Modification>(modificationDTO);
            await _repository.AddAsync(modification);

            // Return the created product
            return CreatedAtAction("GetById", new { id = modification.Id }, _mapper.Map<ModificationDTO>(modification));
        }

        [HttpPut("update-modification/{id}")]
        public async Task<IActionResult> UpdateModification(int id, [FromBody] ModificationDTO modificationDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (modificationDTO == null)
            {
                return BadRequest("Invalid data");
            }

            _mapper.Map(modificationDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<ModificationDTO>(existingEntity));
        }

        [HttpDelete("delete-modification/{id}")]
        public async Task<IActionResult> DeleteModification(int id)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(existingEntity);

            return NoContent();
        }

        [HttpGet("get-modification-by-model/{modelId}")]
        public IActionResult GetModificationsByModel(int modelId)
        {
            var modification = _repository.GetModificationByModel(modelId);
            return Ok(modification);
        }
    }
}
