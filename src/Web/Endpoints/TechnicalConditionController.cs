using Application.DTOs.TechnicalCondition;
using Application.TechnicalConditionMediatoR.Commands;
using Application.TechnicalConditionMediatoR.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalConditionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TechnicalConditionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get-technicalconditions")]
        public async Task<IResult> GetTechnicalConditions()
        {
            var query = await _mediator.Send(new GetAllTechnicalConditions());
            var technicalConditionDTOs = _mapper.Map<IEnumerable<TechnicalConditionDTO>>(query);

            return TypedResults.Ok(technicalConditionDTOs.ToList());
        }

        [HttpGet("get-technicalcondition/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var query = await _mediator.Send(new GetTechnicalConditionById { Id = id });
            var technicalConditionDTO = _mapper.Map<TechnicalConditionDTO>(query);

            return TypedResults.Ok(technicalConditionDTO);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTechnicalCondition([FromBody] TechnicalConditionDTO technicalConditionDTO)
        {
            if (technicalConditionDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var command = _mapper.Map<CreateTechnicalCondition>(technicalConditionDTO);
            var technicalCondition = await _mediator.Send(command);

            // Return the created product
            return CreatedAtAction("GetById", new { id = technicalCondition.Id }, _mapper.Map<TechnicalConditionDTO>(technicalCondition));
        }

        [HttpPut("update-technicalcondition/{id}")]
        public async Task<IResult> UpdateTechnicalCondition(int id, [FromBody] TechnicalConditionDTO technicalConditionDTO)
        {
            if (id != technicalConditionDTO.Id) return Results.BadRequest();

            if (technicalConditionDTO == null)
            {
                return Results.BadRequest("Invalid data");
            }

            var command = _mapper.Map<CreateTechnicalCondition>(technicalConditionDTO);
            await _mediator.Send(command);

            return Results.NoContent();
        }

        [HttpDelete("delete-technicalcondition/{id}")]
        public async Task<IActionResult> DeleteTechnicalCondition(int id)
        {
            await _mediator.Send(new DeleteTechnicalCondition { Id = id });

            return NoContent();
        }
    }
}
