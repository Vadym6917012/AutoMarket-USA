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

        [HttpGet]
        [Route("get-make-by-country/{producingCountryId:int}")]
        public IActionResult GetMakeByProducingCountry(int producingCountryId)
        {
            var make = _repository.GetMakeByCountry(producingCountryId);
            return Ok(make);
        }

        [HttpGet]
        [Route("get-make-by-model/{modelId:int}")]
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
