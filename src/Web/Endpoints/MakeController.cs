using Application.DTOs.Make;
using Application.MakeMediatoR.Commands;
using Application.MakeMediatoR.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MakeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get-makes")]
        public async Task<IResult> GetMakes()
        {
            var makes = await _mediator.Send(new GetAllMakes());
            var makeDTOs = _mapper.Map<IEnumerable<MakeDTO>>(makes);

            return TypedResults.Ok(makeDTOs.ToList());
        }

        [HttpGet("get-make/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var make = await _mediator.Send(new GetMakeById { Id = id });
            var makeDTO = _mapper.Map<MakeDTO>(make);

            return TypedResults.Ok(makeDTO);
        }

        [HttpPost("create-make")]
        public async Task<IActionResult> CreateMake([FromBody] MakeDTO makeDTO)
        {
            if (makeDTO == null)
            {
                return BadRequest("Invalid make data");
            }

            var make = _mapper.Map<CreateMake>(makeDTO);
            var addedEntity = await _mediator.Send(make);

            // Return the created product
            return CreatedAtAction("GetById", new { id = addedEntity.Id }, _mapper.Map<MakeDTO>(addedEntity));
        }

        [HttpPut("update-make")]
        public async Task<IResult> UpdateMake(int id, [FromBody] MakeDTO makeDTO)
        {
            if (id != makeDTO.Id) return Results.BadRequest();

            if (makeDTO == null)
            {
                return Results.BadRequest("Invalid make data");
            }

            var command = _mapper.Map<UpdateMake>(makeDTO);
            var updEntity = await _mediator.Send(command);

            return Results.Ok(_mapper.Map<MakeDTO>(updEntity));
        }

        [HttpDelete("delete-make/{id}")]
        public async Task<IResult> DeleteMake(int id)
        {
            await _mediator.Send(new DeleteMake { Id = id});

            return Results.NoContent();
        }

        [HttpGet("get-make-by-country/{producingCountryId}")]
        public async Task<IResult> GetMakeByProducingCountry(int producingCountryId)
        {
            var make = await _mediator.Send(new GetMakeByProducingCountry { Id = producingCountryId });
            return TypedResults.Ok(make.ToList());
        }
    }
}
