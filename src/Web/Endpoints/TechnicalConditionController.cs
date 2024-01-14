using Application.DTOs.TechnicalCondition;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalConditionController : ControllerBase
    {
        private readonly IRepository<TechnicalCondition> _repository;
        private readonly IMapper _mapper;

        public TechnicalConditionController(IRepository<TechnicalCondition> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TechnicalConditionDTO>> GetTechnicalConditions()
        {
            var technicalConditions = await _repository.GetAllAsync();
            var technicalConditionDTOs = _mapper.Map<IEnumerable<TechnicalConditionDTO>>(technicalConditions);

            return technicalConditionDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<TechnicalConditionDTO> GetById(int id)
        {
            var technicalCondition = await _repository.GetByIdAsync(id);
            var technicalConditionDTO = _mapper.Map<TechnicalConditionDTO>(technicalCondition);

            return technicalConditionDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTechnicalCondition([FromBody] TechnicalConditionDTO technicalConditionDTO)
        {
            if (technicalConditionDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var technicalCondition = _mapper.Map<TechnicalCondition>(technicalConditionDTO);
            await _repository.AddAsync(technicalCondition);

            // Return the created product
            return CreatedAtAction("GetById", new { id = technicalCondition.Id }, _mapper.Map<TechnicalConditionDTO>(technicalCondition));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTechnicalCondition(int id, [FromBody] TechnicalConditionDTO technicalConditionDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (technicalConditionDTO == null)
            {
                return BadRequest("Invalid data");
            }

            _mapper.Map(technicalConditionDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<TechnicalConditionDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteTechnicalCondition(int id)
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
