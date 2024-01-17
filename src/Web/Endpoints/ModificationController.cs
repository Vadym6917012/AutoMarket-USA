using Application.DTOs.Modification;
using Application.ModificationMediatoR.Commands;
using Application.ModificationMediatoR.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModificationController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly IMapper _mapper;

        public ModificationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get-modifications")]
        public async Task<IResult> GetModifications()
        {
            var modifications = await _mediator.Send(new GetAllModifications());
            var modificationDTOs = _mapper.Map<IEnumerable<ModificationDTO>>(modifications);

            return TypedResults.Ok(modificationDTOs.ToList());
        }

        [HttpGet("get-modification/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var modification = await _mediator.Send(new GetModificationById { Id = id });
            var modificationDTO = _mapper.Map<ModificationDTO>(modification);

            return TypedResults.Ok(modificationDTO);
        }

        [HttpPost("create-modification")]
        public async Task<IActionResult> CreateModification([FromBody] ModificationDTO modificationDTO)
        {
            if (modificationDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var modification = _mapper.Map<CreateModification>(modificationDTO);
            var addedModification = await _mediator.Send(modification);

            // Return the created product
            return CreatedAtAction("GetById", new { id = addedModification.Id }, _mapper.Map<ModificationDTO>(addedModification));
        }

        [HttpPut("update-modification/{id}")]
        public async Task<IResult> UpdateModification(int id, [FromBody] ModificationDTO modificationDTO)
        {
            if (id != modificationDTO.Id) return Results.BadRequest();

            if (modificationDTO == null)
            {
                return Results.BadRequest("Invalid data");
            }

            var command = _mapper.Map<UpdateModification>(modificationDTO);
            await _mediator.Send(command);

            return Results.NoContent();
        }

        [HttpDelete("delete-modification/{id}")]
        public async Task<IResult> DeleteModification(int id)
        {
            await _mediator.Send(new DeleteModification { Id = id });

            return Results.NoContent();
        }

        [HttpGet("get-modification-by-model/{modelId}")]
        public async Task<IResult> GetModificationsByModel(int modelId)
        {
            var modification = await _mediator.Send(new GetModificationsByModel { Id = modelId });
            var modificationDto = _mapper.Map<ModificationDTO>(modification);

            return TypedResults.Ok(modificationDto);
        }
    }
}
