using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs.GearBox;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GearBoxTypeController : ControllerBase
    {
        public readonly Repository<GearBoxType> _repository;
        public readonly IMapper _mapper;

        public GearBoxTypeController(Repository<GearBoxType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GearBoxTypeDTO>> GetGearBoxTypes()
        {
            var producingCountries = await _repository.GetAllAsync();
            var gearBoxTypeDTOs = _mapper.Map<IEnumerable<GearBoxTypeDTO>>(producingCountries);

            return gearBoxTypeDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<GearBoxTypeDTO> GetById(int id)
        {
            var gearBoxType = await _repository.GetByIdAsync(id);
            var gearBoxTypeDTO = _mapper.Map<GearBoxTypeDTO>(gearBoxType);

            return gearBoxTypeDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGearBoxType([FromBody] GearBoxTypeDTO gearBoxTypeDTO)
        {
            if (gearBoxTypeDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var gearBoxType = _mapper.Map<GearBoxType>(gearBoxTypeDTO);
            await _repository.AddAsync(gearBoxType);

            // Return the created product
            return CreatedAtAction("GetById", new { id = gearBoxType.Id }, _mapper.Map<GearBoxTypeDTO>(gearBoxType));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateGearBoxType(int id, [FromBody] GearBoxTypeDTO gearBoxTypeDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (gearBoxTypeDTO == null)
            {
                return BadRequest("Invalid data");
            }

            _mapper.Map(gearBoxTypeDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<GearBoxTypeDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteGearBoxType(int id)
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
