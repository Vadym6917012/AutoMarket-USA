using Application.DTOs.FuelType;
using Application.FuelTypeMediatoR.Commands;
using Application.FuelTypeMediatoR.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public FuelTypeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("get-fueltypes")]
        public async Task<IResult> GetFuelTypes()
        {
            var fuelTypes = await _mediator.Send(new GetAllFuelTypes());
            var fuelTypeDTOs = _mapper.Map<IEnumerable<FuelTypeDTO>>(fuelTypes);

            return TypedResults.Ok(fuelTypeDTOs.ToList());
        }

        [HttpGet("get-fueltype/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var fuelType = await _mediator.Send(new GetFuelTypeById { Id = id });
            var fuelTypeDTO = _mapper.Map<FuelTypeDTO>(fuelType);

            return TypedResults.Ok(fuelTypeDTO);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFuelType([FromBody] FuelTypeDTO fuelTypeDTO)
        {
            if (fuelTypeDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var fuelType = _mapper.Map<CreateFuelType>(fuelTypeDTO);
            var addedFuelType = await _mediator.Send(fuelType);

            // Return the created product
            return CreatedAtAction("GetById", new { id = addedFuelType.Id }, _mapper.Map<FuelTypeDTO>(addedFuelType));
        }

        [HttpPut("update-fueltype")]
        public async Task<IResult> UpdateFuelType(int id, [FromBody] FuelTypeDTO fuelTypeDTO)
        {
            if (id != fuelTypeDTO.Id) return Results.BadRequest();

            if (fuelTypeDTO == null)
            {
                return Results.BadRequest("Invalid data");
            }

            var command = _mapper.Map<UpdateFuelType>(fuelTypeDTO);
            await _mediator.Send(command);

            return Results.NoContent();
        }

        [HttpDelete("delete-fueltype/{id}")]
        public async Task<IResult> DeleteFuelType(int id)
        {
            await _mediator.Send(new DeleteFuelType { Id = id });

            return Results.NoContent();
        }
    }
}
