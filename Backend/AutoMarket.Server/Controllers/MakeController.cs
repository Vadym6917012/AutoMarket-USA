using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/make")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly Repository<Make> _repository;
        private readonly IMapper _mapper;

        public MakeController(Repository<Make> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeDTO>> GetMakes()
        {
            var makes = await _repository.GetAllAsync();
            var makeDTOs = _mapper.Map<IEnumerable<MakeDTO>>(makes);

            return makeDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<MakeDTO> GetById(int id)
        {
            var make = await _repository.GetByIdAsync(id);
            var makeDTO = _mapper.Map<MakeDTO>(make);

            return makeDTO;
        }

        [HttpPost]
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

        [HttpPut]
        [Route("{id:int}")]
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

        [HttpDelete]
        [Route("{id:int}")]
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
    }
}
