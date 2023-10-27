using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using AutoMarket.Server.Shared.DTOs.Car;
using Microsoft.AspNetCore.Mvc;

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

            return CreatedAtAction("GetById", new { id = car.Id }, _mapper.Map<CarCreateDTO>(car));
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
