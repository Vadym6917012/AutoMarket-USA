using Application.CarMediatoR.Commands;
using Application.CarMediatoR.Queries;
using Application.DTOs.Car;
using Application.ImagesMediatoR.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public CarController(IMapper mapper, IMediator mediator, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("get-cars")]
        public async Task<IResult> GetCars()
        {
            var cars = await _mediator.Send(new GetAllCars());
            var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(cars);

            return TypedResults.Ok(carDTOs.ToList());
        }

        [HttpGet("get-unverified-cars")]
        public async Task<IResult> GetUnverifiedCars()
        {
            var cars = await _mediator.Send(new GetUnverifiedCars());
            var carDtos = _mapper.Map<IEnumerable<CarDTO>>(cars);

            return TypedResults.Ok(carDtos.ToList());
        }

        [HttpGet("get-car/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var car = await _mediator.Send(new GetCarById { Id = id });
            var carDTO = _mapper.Map<CarDTO>(car);

            return TypedResults.Ok(carDTO);
        }

        [HttpGet("get-car-for-update/{id}")]
        public async Task<IResult> GetByIdForUpdate(int id)
        {
            var car = await _mediator.Send(new GetCarById { Id = id });
            var carDTO = _mapper.Map<CarCreateDTO>(car);

            return TypedResults.Ok(carDTO);
        }

        [HttpGet("get-car-by-userid/{id}")]
        public async Task<IResult> GetCarByUserId(string id)
        {
            var car = await _mediator.Send(new GetCarByUserId { UserId = id });
            var carDTO = _mapper.Map<IEnumerable<CarDTO>>(car);

            return TypedResults.Ok(carDTO.ToList());
        }

        [HttpGet("car-home-filter")]
        public async Task<IResult> CarHomeFilter([FromQuery] CarHomeFilter filter)
        {
            var query = _mapper.Map<CarAdvanceFilter>(filter);
            var car = await _mediator.Send(query);
            var carDTO = _mapper.Map<IEnumerable<CarDTO>>(car);

            return TypedResults.Ok(carDTO.ToList());
        }

        [HttpGet("car-filter")]
        public async Task<IResult> CarFilter([FromQuery] CarFilter filter)
        {
            var query = _mapper.Map<CarAdvanceFilter>(filter);
            var car = await _mediator.Send(query);
            var carDTO = _mapper.Map<IEnumerable<CarDTO>>(car);

            return TypedResults.Ok(carDTO.ToList());
        }

        [HttpGet("get-recent-cars/{count}")]
        public async Task<IResult> GetRecentCars(int count)
        {
            var cars = await _mediator.Send(new GetRecentCars { Count = count });
            var carDto = _mapper.Map<IEnumerable<CarDTO>>(cars);

            return TypedResults.Ok(carDto.ToList());
        }

        [HttpPost("create-with-images")]
        public async Task<IResult> CreateCarWithImages([FromForm] CarCreateDTO carDTO, List<IFormFile> images)
        {
            if (carDTO == null)
            {
                return Results.BadRequest("Invalid data");
            }

            var createCommand = _mapper.Map<CreateCar>(carDTO);

            var car = await _mediator.Send(createCommand);

            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    if (image != null && image.Length > 0)
                    {
                        var filePathToImage = AddImagesToDirectory(image);
                        var filePathToDisplay = filePathToImage.Substring(filePathToImage.LastIndexOf("\\User Photos\\") + 1);
                        filePathToDisplay.Replace(@"\\", "/");

                        if (!string.IsNullOrEmpty(filePathToImage))
                        {
                            var imageAdded = new CreateImage { ImagePathToDisplay = "https://localhost:7119/" + filePathToDisplay, ImagePath = filePathToImage, CarId = car.Id, Car = car };
                            await _mediator.Send(imageAdded);
                        }
                    }
                }
            }
            else
            {
                var defaultImagePath = AddImagesToDirectory(null);
                var defaultImagePathToDisplay = defaultImagePath.Substring(defaultImagePath.LastIndexOf("\\User Photos\\") + 1);
                defaultImagePathToDisplay.Replace(@"\\", "/");

                if (!string.IsNullOrEmpty(defaultImagePath))
                {
                    var defaultImage = new CreateImage { ImagePathToDisplay = "https://localhost:7119/" + defaultImagePathToDisplay, ImagePath = defaultImagePath, CarId = car.Id, Car = car };
                    await _mediator.Send(defaultImage);
                }
            }

            return Results.Ok(new JsonResult(new { title = "Оголошення додано успішно", message = "Ваше оголошення додано успішно", id = car.Id }));
        }

        [HttpPut("update-car")]
        public async Task<IResult> UpdateCar(int id, [FromBody] CarCreateDTO carDTO)
        {
            if (id != carDTO.Id) return Results.BadRequest("Invalid data");

            var command = _mapper.Map<UpdateCar>(carDTO);

            var car = await _mediator.Send(command);

            return Results.Ok(new JsonResult(new { title = "Оголошення оновлено успішно", message = "Ваше оголошення було оновлено успішно", id = car.Id }));
        }

        [HttpDelete("delete-car/{carId}")]
        public async Task<IResult> DeleteCar(int carId)
        {
            await _mediator.Send(new DeleteCar { Id = carId });

            return Results.Ok(new JsonResult(new { title = "Оголошення видалено успішно", message = "Ваше оголошення було видалено успішно" }));
        }

        #region private Helpers

        private string AddImagesToDirectory(IFormFile? images)
        {
            if (images == null || images.Length == 0)
            {
                var uniqueFileName = "NoImage.png";
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "User Photos");
                var filePathCombained = Path.Combine(uploadsFolder, uniqueFileName);

                return filePathCombained;
            }
            else if (images != null && images.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + images.FileName;
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "User Photos");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var filePathCombained = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePathCombained, FileMode.Create))
                {
                    images.CopyTo(fileStream);
                }

                return filePathCombained;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
