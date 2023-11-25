using AutoMapper;
using AutoMarket.Server.Core.Models;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs.Generation;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerationController : ControllerBase
    {
        public readonly GenerationRepository _repository;
        public readonly IMapper _mapper;

        public GenerationController(GenerationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GenerationDTO>> GetGenerations()
        {
            var generations = await _repository.GetAllAsync();
            var generationDTOs = _mapper.Map<IEnumerable<GenerationDTO>>(generations);

            return generationDTOs.ToList();
        }

        [HttpGet]
        [Route("get-generation-by-model/{modelId:int}")]
        public IActionResult GetGenerationsByModel(int modelId)
        {
            var generations = _repository.GetGenerationsByModel(modelId);
            return Ok(generations);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<GenerationDTO> GetById(int id)
        {
            var generation = await _repository.GetByIdAsync(id);
            var generationDTO = _mapper.Map<GenerationDTO>(generation);

            return generationDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGeneration([FromBody] GenerationDTO generationDTO)
        {
            if (generationDTO == null)
            {
                return BadRequest("Invalid generation data");
            }

            var generation = _mapper.Map<Generation>(generationDTO);
            await _repository.AddAsync(generation);

            return CreatedAtAction("GetById", new { id = generation.Id }, _mapper.Map<GenerationDTO>(generation));
        }

        [HttpPost("add-generation-to-model")]
        public async Task<IActionResult> AddGenerationToModel([FromQuery] int modelId, [FromBody] GenerationDTO generationDTO)
        {
            if (generationDTO == null)
            {
                return BadRequest("Invalid generation data");
            }

            var generation = _mapper.Map<Generation>(generationDTO);
            await _repository.AddToModelAsync(modelId, generation);

            return Ok(new JsonResult(new { title = "Покоління додано успішно", message = "Покоління додано успішно", id = generation.Id }));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateGeneration(int id, [FromBody] GenerationDTO generationDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (generationDTO == null)
            {
                return BadRequest("Invalid body type data");
            }

            _mapper.Map(generationDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<GenerationDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteGeneration(int id)
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
