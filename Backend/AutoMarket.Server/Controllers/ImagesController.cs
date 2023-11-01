using AutoMapper;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs.Images;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImagesRepository _repository;
        private readonly IMapper _mapper;

        public ImagesController(ImagesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ImagesDTO>> GetImages()
        {
            var images = await _repository.GetAllAsync();
            var imagesDTOs = _mapper.Map<IEnumerable<ImagesDTO>>(images);

            return imagesDTOs.ToList();
        }

        [HttpGet]
        [Route("{carId:int}")]
        public async Task<IEnumerable<ImagesDTO>> GetByCarId(int carId)
        {
            var image = await _repository.GetByCarIdAsync(carId);
            var imageDTO = _mapper.Map<IEnumerable<ImagesDTO>>(image);

            return imageDTO.ToList();
        }

        [HttpGet]
        [Route("get-by-car-name")]
        public ActionResult GetPhotoByName(string name)
        {
            var fileStream = _repository.GetPhotoByName(name);

            if (fileStream != null)
            {
                return File(fileStream, "image/jpeg");
            } else
            { 
                return NotFound(); 
            }
        }
    }
}
