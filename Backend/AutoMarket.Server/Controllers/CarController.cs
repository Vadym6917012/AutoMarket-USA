using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Core.Models;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using AutoMarket.Server.Shared.DTOs.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarRepository _repository;
        private readonly ImagesRepository _imagesRepository;
        private readonly IMapper _mapper;

        public CarController(CarRepository repository, ImagesRepository imagesRepository, IMapper mapper)
        {
            _repository = repository;
            _imagesRepository = imagesRepository;
            _mapper = mapper;
        }

        [HttpGet("get-cars")]
        public async Task<IEnumerable<CarDTO>> GetCars()
        {
            var cars = await _repository.GetAllAsync();
            var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(cars);

            return carDTOs.ToList();
        }

        [HttpGet("get-car/{id}")]
        public async Task<CarDTO> GetById(int id)
        {
            var car = await _repository.GetByIdAsync(id);
            var carDTO = _mapper.Map<CarDTO>(car);

            return carDTO;
        }

        [HttpGet("get-car-by-userid/{id}")]
        public async Task<IEnumerable<CarDTO>> GetCarByUserId(string id)
        {
            var car = await _repository.GetCarByUserId(id);
            var carDTO = _mapper.Map<IEnumerable<CarDTO>>(car);

            return carDTO.ToList();
        }

        [HttpGet("car-home-filter")]
        public async Task<IEnumerable<CarDTO>> CarHomeFilter([FromQuery] CarHomeFilter filter)
        {
            var car = await _repository.HomeFilter(filter);
            var carDTO = _mapper.Map<IEnumerable<CarDTO>>(car);

            return carDTO.ToList();
        }

        [HttpGet("car-filter")]
        public async Task<IEnumerable<CarDTO>> CarFilter([FromQuery] CarFilter filter)
        {
            var car = await _repository.CarFilter(filter);
            var carDTO = _mapper.Map<IEnumerable<CarDTO>>(car);

            return carDTO.ToList();
        }

        [HttpGet("get-cars-by-count/{count}")]
        public async Task<IEnumerable<CarDTO>> GetByCount(int count)
        {
            var cars = await _repository.GetByCount(count);
            var carDto = _mapper.Map<IEnumerable<CarDTO>>(cars);

            return carDto.ToList();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateDTO carDTO)
        {
            if (carDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var car = _mapper.Map<Car>(carDTO);
            await _repository.AddAsync(car);

            return CreatedAtAction("GetById", new { id = car.Id }, _mapper.Map<CarCreateDTO>(car));
        }

        [HttpPost("create-with-images")]
        public async Task<IActionResult> CreateCarWithImages([FromForm] CarCreateDTO carDTO, List<IFormFile> images)
        {
            if (carDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var car = _mapper.Map<Car>(carDTO);

            var checkYear = _repository.CheckYear(car);

            if (checkYear == null)
            {
                ModelState.AddModelError("errors", "Рік повинен співпадати з роком створення покоління");
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(car);

            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    if (image != null && image.Length > 0)
                    {
                        var filePathToImage = _imagesRepository.AddImagesToDirectory(image);
                        var filePathToDisplay = filePathToImage.Substring(filePathToImage.LastIndexOf("\\User Photos\\") + 1);
                        filePathToDisplay.Replace(@"\\", "/");

                        if (!string.IsNullOrEmpty(filePathToImage))
                        {
                            var imageAdded = new Images { ImagePathToDisplay = "https://localhost:7119/" + filePathToDisplay, ImagePath = filePathToImage, CarId = car.Id, Car = car };
                            await _imagesRepository.AddAsync(imageAdded);
                        }
                    }
                }
            }

            return Ok(new JsonResult(new { title = "Оголошення додано успішно", message = "Ваше оголошення додано успішно", id = car.Id }));
        }

        [HttpPut("update-car")]
        public async Task<IActionResult> UpdateCar([FromBody] CarCreateDTO carDTO)
        {
            if (carDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var existingEntity = _repository.GetById(carDTO.Id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(carDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(new JsonResult(new { title = "Оголошення оновлено успішно", message = "Ваше оголошення було оновлено успішно", id = existingEntity.Id }));
        }

        [HttpDelete("delete-car/{carId}")]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            var existingEntity = _repository.GetById(carId);

            if (existingEntity == null)
            {
                return NotFound(new JsonResult(new { title = "Такого оголошення не існує", message = "Ваше оголошення не знайдене тому не було видалено" }));
            }

            await _repository.DeleteAsync(existingEntity);

            return Ok(new JsonResult(new { title = "Оголошення видалено успішно", message = "Ваше оголошення було видалено успішно" }));
        }
    }
}
