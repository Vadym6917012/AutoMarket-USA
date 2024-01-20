using Application.DTOs.Model;
using Application.Models.Commands;
using Application.Models.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ModelController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get-models")]
        public async Task<IResult> GetModels()
        {
            var models = await _mediator.Send(new GetAllModels());
            var modelDTOs = _mapper.Map<IEnumerable<ModelDTO>>(models);

            return TypedResults.Ok(modelDTOs.ToList());
        }

        [HttpGet("get-model/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var model = await _mediator.Send(new GetModelById { Id = id });
            var modelDTO = _mapper.Map<ModelDTO>(model);

            return TypedResults.Ok(modelDTO);
        }

        [HttpPost("create-model")]
        public async Task<IActionResult> CreateModel([FromBody] ModelDTO modelDTO)
        {
            if (modelDTO == null)
            {
                return BadRequest("Invalid model data");
            }

            var model = _mapper.Map<CreateModel>(modelDTO);
            var addedModel = await _mediator.Send(model);

            // Return the created product
            return CreatedAtAction("GetById", new { id = addedModel.Id }, _mapper.Map<ModelDTO>(addedModel));
        }

        [HttpPut("update-model/{id}")]
        public async Task<IResult> UpdateModel(int id, [FromBody] ModelDTO modelDTO)
        {
            if (id != modelDTO.Id) return Results.BadRequest();

            if (modelDTO == null)
            {
                return Results.BadRequest("Invalid model data");
            }

            var command = _mapper.Map<UpdateModel>(modelDTO);
            var updEntity = await _mediator.Send(command);

            return TypedResults.Ok(_mapper.Map<ModelDTO>(updEntity));
        }

        [HttpDelete("delete-model/{id}")]
        public async Task<IResult> DeleteModel(int id)
        {
            await _mediator.Send(new DeleteModel { Id = id });

            return Results.NoContent();
        }

        [HttpGet("get-model-by-make/{makeId:int}")]
        public async Task<IResult> GetModelsByMake(int makeId)
        {
            var model = _mediator.Send(new GetModelsByMake { Id = makeId });

            return TypedResults.Ok(model);
        }
    }
}
