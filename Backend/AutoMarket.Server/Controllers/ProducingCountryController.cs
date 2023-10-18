using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/producingcountry")]
    [ApiController]
    public class ProducingCountryController : Controller
    {
        private readonly Repository<ProducingCountry> _repository;
        private readonly IMapper _mapper;

        public ProducingCountryController(Repository<ProducingCountry> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProducingCountryDTO>> GetProducingCountrys()
        {
            var producingCountries = await _repository.GetAllAsync();
            var producingCountryDTOs = _mapper.Map<IEnumerable<ProducingCountryDTO>>(producingCountries);

            return producingCountryDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ProducingCountryDTO> GetById(int id)
        {
            var producingCountry = await _repository.GetByIdAsync(id);
            var producingCountryDTO = _mapper.Map<ProducingCountryDTO>(producingCountry);

            return producingCountryDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducingCountry([FromBody] ProducingCountryDTO producingCountryDTO)
        {
            if (producingCountryDTO == null)
            {
                return BadRequest("Invalid body type data");
            }

            var producingCountry = _mapper.Map<ProducingCountry>(producingCountryDTO);
            await _repository.AddAsync(producingCountry);

            // Return the created product
            return CreatedAtAction("GetById", new { id = producingCountry.Id }, _mapper.Map<ProducingCountryDTO>(producingCountry));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProducingCountry(int id, [FromBody] ProducingCountryDTO producingCountryDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (producingCountryDTO == null)
            {
                return BadRequest("Invalid body type data");
            }

            _mapper.Map(producingCountryDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<ProducingCountryDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProducingCountry(int id)
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
