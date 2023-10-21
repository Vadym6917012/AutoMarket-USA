using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelTypeController : Controller
    {
        public readonly Repository<FuelType> _repository;
        public readonly IMapper _mapper;

        public FuelTypeController(Repository<FuelType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<FuelTypeDTO>> GetFuelTypes()
        {
            var fuelTypes = await _repository.GetAllAsync();
            var fuelTypeDTOs = _mapper.Map<IEnumerable<FuelTypeDTO>>(fuelTypes);

            return fuelTypeDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<FuelTypeDTO> GetById(int id)
        {
            var fuelType = await _repository.GetByIdAsync(id);
            var fuelTypeDTO = _mapper.Map<FuelTypeDTO>(fuelType);

            return fuelTypeDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFuelType([FromBody] FuelTypeDTO fuelTypeDTO)
        {
            if (fuelTypeDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var fuelType = _mapper.Map<FuelType>(fuelTypeDTO);
            await _repository.AddAsync(fuelType);

            // Return the created product
            return CreatedAtAction("GetById", new { id = fuelType.Id }, _mapper.Map<FuelTypeDTO>(fuelType));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateFuelType(int id, [FromBody] FuelTypeDTO fuelTypeDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (fuelTypeDTO == null)
            {
                return BadRequest("Invalid data");
            }

            _mapper.Map(fuelTypeDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<FuelTypeDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteFuelType(int id)
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
