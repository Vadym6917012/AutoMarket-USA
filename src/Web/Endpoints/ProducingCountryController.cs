using Application.DTOs.ProducingCountry;
using Application.ProducingCountries.Commands;
using Application.ProducingCountries.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducingCountryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProducingCountryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get-countries")]
        public async Task<IResult> GetProducingCountrys()
        {
            var producingCountries = await _mediator.Send(new GetAllProducingCountries());
            var producingCountryDTOs = _mapper.Map<IEnumerable<ProducingCountryDTO>>(producingCountries);

            return TypedResults.Ok(producingCountryDTOs.ToList());
        }

        [HttpGet("get-country/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var producingCountry = await _mediator.Send(new GetProducingCountryById { Id = id });
            var producingCountryDTO = _mapper.Map<ProducingCountryDTO>(producingCountry);

            return TypedResults.Ok(producingCountryDTO);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProducingCountry([FromBody] ProducingCountryDTO producingCountryDTO)
        {
            if (producingCountryDTO == null)
            {
                return BadRequest("Invalid body type data");
            }

            var command = _mapper.Map<CreateProducingCountry>(producingCountryDTO);
            var addedEntity = await _mediator.Send(command);

            // Return the created product
            return CreatedAtAction("GetById", new { id = addedEntity.Id }, _mapper.Map<ProducingCountryDTO>(addedEntity));
        }

        [HttpPut("update-country/{id}")]
        public async Task<IResult> UpdateProducingCountry(int id, [FromBody] ProducingCountryDTO producingCountryDTO)
        {
            if (id != producingCountryDTO.Id) return Results.BadRequest();

            if (producingCountryDTO == null)
            {
                return Results.BadRequest("Invalid body type data");
            }

            var command = _mapper.Map<UpdateProducingCountry>(producingCountryDTO);
            await _mediator.Send(command);

            return Results.NoContent();
        }

        [HttpDelete("delete-country/{id}")]
        public async Task<IActionResult> DeleteProducingCountry(int id)
        {
            await _mediator.Send(new DeleteProducingCountry { Id = id });

            return NoContent();
        }
    }
}
