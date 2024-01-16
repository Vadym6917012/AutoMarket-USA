using Application.DTOs.Generation;
using Application.GenerationMediatoR.Commands;
using Application.GenerationMediatoR.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GenerationController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("get-generations")]
        public async Task<IResult> GetGenerations()
        {
            var generations = await _mediator.Send(new GetAllGenerations());
            var generationDTOs = _mapper.Map<IEnumerable<GenerationDTO>>(generations);

            return TypedResults.Ok(generationDTOs.ToList());
        }

        [HttpGet("get-generation-by-model/{modelId}")]
        public async Task<IResult> GetGenerationsByModel(int modelId)
        {
            var generations = await _mediator.Send(new GetGenerationsByModel { Id = modelId });
            var generationDTOs = _mapper.Map<IEnumerable<GenerationDTO>>(generations);

            return TypedResults.Ok(generationDTOs.ToList());
        }

        [HttpGet("get-generation/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var generation = await _mediator.Send(new GetGenerationById { Id = id });
            var generationDTO = _mapper.Map<GenerationDTO>(generation);

            return TypedResults.Ok(generationDTO);
        }

        [HttpPost("add-generation-to-model")]
        public async Task<IResult> AddGenerationToModel([FromQuery] int modelId, [FromBody] GenerationDTO generationDTO)
        {
            if (generationDTO == null)
            {
                return Results.BadRequest("Invalid generation data");
            }

            var command = new CreateGeneration
            {
                ModelId = modelId,
                Name = generationDTO.Name,
                YearFrom = generationDTO.YearFrom,
                YearTo = generationDTO.YearTo,
            };

            await _mediator.Send(command);

            return Results.Ok(new JsonResult(new { title = "Покоління додано успішно", message = $"Покоління {generationDTO.Name}  додано успішно", id = generationDTO.Id }));
        }

        [HttpPut("update-generation/{id}")]
        public async Task<IResult> UpdateGeneration(int id, [FromBody] GenerationDTO generationDTO)
        {
            if (id != generationDTO.Id) return Results.BadRequest();

            if (generationDTO == null)
            {
                return TypedResults.BadRequest("Invalid body type data");
            }

            var command = _mapper.Map<UpdateGeneration>(generationDTO);
            await _mediator.Send(command);

            return TypedResults.NoContent();
        }

        [HttpDelete("delete-generation/{id}")]
        public async Task<IResult> DeleteGeneration(int id)
        {
            await _mediator.Send(new DeleteGeneration { Id = id });

            return TypedResults.NoContent();
        }
    }
}