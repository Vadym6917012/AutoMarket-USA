using AutoMapper;
using AutoMarket.Server.Core.Models;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs.Make;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly MakeRepository _repository;
        private readonly IMapper _mapper;

        public MakeController(MakeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get-makes")]
        public async Task<IEnumerable<MakeDTO>> GetMakes()
        {
            var makes = await _repository.GetAllAsync();
            var makeDTOs = _mapper.Map<IEnumerable<MakeDTO>>(makes);

            return makeDTOs.ToList();
        }

        [HttpGet("get-make/{id}")]
        public async Task<MakeDTO> GetById(int id)
        {
            var make = await _repository.GetByIdAsync(id);
            var makeDTO = _mapper.Map<MakeDTO>(make);

            return makeDTO;
        }

        [HttpPost("create-make")]
        public async Task<IActionResult> CreateMake([FromBody] MakeDTO makeDTO)
        {
            if (makeDTO == null)
            {
                return BadRequest("Invalid make data");
            }

            var make = _mapper.Map<Make>(makeDTO);
            await _repository.AddAsync(make);

            // Return the created product
            return CreatedAtAction("GetById", new { id = make.Id }, _mapper.Map<MakeDTO>(make));
        }

        [HttpPut("update-make")]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] MakeDTO makeDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (makeDTO == null)
            {
                return BadRequest("Invalid make data");
            }

            _mapper.Map(makeDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<MakeDTO>(existingEntity));
        }

        [HttpDelete("delete-make/{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(existingEntity);

            return NoContent();
        }

        [HttpGet("get-make-by-country/{producingCountryId}")]
        public IActionResult GetMakeByProducingCountry(int producingCountryId)
        {
            var make = _repository.GetMakeByCountry(producingCountryId);
            return Ok(make);
        }

        [HttpGet("get-make-by-model/{modelId}")]
        public async Task<ActionResult> GetMakeByModel(int modelId)
        {
            var make = _repository.GetMakeByModel(modelId);

            if (make == null)
            {
                return BadRequest("Не вдалося знайти марку за даною моделлю");
            }

            var makeDTO = _mapper.Map<MakeDTO>(make);

            return Ok(makeDTO);
        }
    }
}
