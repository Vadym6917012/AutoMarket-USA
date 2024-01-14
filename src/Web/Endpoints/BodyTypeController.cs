using Application.BodyTypeMediatoR.Commands;
using Application.BodyTypeMediatoR.Queries;
using Application.DTOs.BodyType;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BodyTypeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("get-bodytypes")]
        public async Task<IResult> GetBodyTypes()
        {
            var bodyTypes = await _mediator.Send(new GetAllBodyTypes());
            var bodyTypeDTOs = _mapper.Map<IEnumerable<BodyTypeDTO>>(bodyTypes);

            return TypedResults.Ok(bodyTypeDTOs.ToList());
        }

        [HttpGet("get-bodytype/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var getBodyType = new GetBodyTypeById { Id = id };
            
            var bodyType = await _mediator.Send(getBodyType);
            var bodyTypeDTO = _mapper.Map<BodyTypeDTO>(bodyType);

            return TypedResults.Ok(bodyTypeDTO);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBodyType([FromBody] BodyTypeDTO bodyTypeDTO)
        {
            if (bodyTypeDTO == null)
            {
                return BadRequest("Invalid body type data");
            }

            var createCommand = _mapper.Map<CreateBodyType>(bodyTypeDTO);

            await _mediator.Send(createCommand);

            // Return the created product
            return CreatedAtAction("GetById", new { id = bodyTypeDTO.Id }, _mapper.Map<BodyTypeDTO>(createCommand));
        }

        [HttpPut("update-bodytype")]
        public async Task<IResult> UpdateBodyType(int id, [FromBody] BodyTypeDTO bodyTypeDTO)
        {
            if (id != bodyTypeDTO.Id) return Results.BadRequest();

            var command = _mapper.Map<UpdateBodyType>(bodyTypeDTO);

            await _mediator.Send(command);

            return Results.NoContent();
        }

        [HttpDelete("delete-bodytype/{id}")]
        public async Task<IResult> DeleteBodyType(int id)
        {
            await _mediator.Send(new DeleteBodyType { Id = id });

            return Results.NoContent();
        }
    }
}
