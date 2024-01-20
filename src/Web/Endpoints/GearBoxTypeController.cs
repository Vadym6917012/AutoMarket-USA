using Application.DTOs.GearBox;
using Application.GearBoxes.Commands;
using Application.GearBoxes.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class GearBoxTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GearBoxTypeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("get-gearboxtypes")]
        public async Task<IResult> GetGearBoxTypes()
        {
            var gearBoxTypes = await _mediator.Send(new GetAllGearBoxes());
            var gearBoxTypeDTOs = _mapper.Map<IEnumerable<GearBoxTypeDTO>>(gearBoxTypes);

            return TypedResults.Ok(gearBoxTypeDTOs.ToList());
        }

        [HttpGet("get-gearboxtype/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var gearBoxType = await _mediator.Send(new GetGearBoxById { Id = id });
            var gearBoxTypeDTO = _mapper.Map<GearBoxTypeDTO>(gearBoxType);

            return TypedResults.Ok(gearBoxTypeDTO);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGearBoxType([FromBody] GearBoxTypeDTO gearBoxTypeDTO)
        {
            if (gearBoxTypeDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var gearBoxType = _mapper.Map<CreateGearBox>(gearBoxTypeDTO);
            var addedGearBoxType = await _mediator.Send(gearBoxType);

            // Return the created product
            return CreatedAtAction("GetById", new { id = addedGearBoxType.Id }, _mapper.Map<GearBoxTypeDTO>(addedGearBoxType));
        }

        [HttpPut("update-gearboxtype/{id}")]
        public async Task<IResult> UpdateGearBoxType(int id, [FromBody] GearBoxTypeDTO gearBoxTypeDTO)
        {
            if (id != gearBoxTypeDTO.Id) return Results.BadRequest();

            if (gearBoxTypeDTO == null)
            {
                return Results.BadRequest("Invalid data");
            }

            var command = _mapper.Map<UpdateGearBox>(gearBoxTypeDTO);
            await _mediator.Send(command);

            return Results.NoContent();
        }

        [HttpDelete("delete-gearboxtype/{id}")]
        public async Task<IResult> DeleteGearBoxType(int id)
        {
            await _mediator.Send(new DeleteGearBox { Id = id });

            return Results.NoContent();
        }
    }
}