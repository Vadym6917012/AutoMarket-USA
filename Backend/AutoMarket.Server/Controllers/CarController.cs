using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarRepository _repository;
        private readonly IMapper _mapper;

        public CarController(CarRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CarDTO>> GetCars()
        {
            var cars = await _repository.GetAllAsync();
            var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(cars);

            return carDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<CarDTO> GetById(int id)
        {
            var car = await _repository.GetByIdAsync(id);
            var carDTO = _mapper.Map<CarDTO>(car);

            return carDTO;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCar([FromBody] CarDTO carDTO)
        {
            if (carDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var car = _mapper.Map<Car>(carDTO);
            await _repository.AddAsync(car);

            return CreatedAtAction("GetById", new { id = car.Id }, _mapper.Map<CarDTO>(car));
        }

        [HttpPost("create-with-images")]
        public async Task<IActionResult> CreateCarWithImages([FromForm] CarDTO carDTO, List<IFormFile> images)
        {
            if (carDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var car = _mapper.Map<Car>(carDTO);
            await _repository.AddAsyncWithImages(car, images);

            return CreatedAtAction("GetById", new { id = car.Id }, _mapper.Map<CarDTO>(car));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarDTO carDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (carDTO == null)
            {
                return BadRequest("Invalid data");
            }

            _mapper.Map(carDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<CarDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCar(int id)
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
