using AutoMapper;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/image")]
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
        public async Task<IEnumerable<ImagesDTO>> GetImages()
        {
            var images = await _repository.GetAllAsync();
            var imagesDTOs = _mapper.Map<IEnumerable<ImagesDTO>>(images);

            return imagesDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ImagesDTO> GetById(int id)
        {
            var image = await _repository.GetByIdAsync(id);
            var imageDTO = _mapper.Map<ImagesDTO>(image);

            return imageDTO;
        }
    }
}
