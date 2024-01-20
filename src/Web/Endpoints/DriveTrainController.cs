using Application.DriveTrains.Commands;
using Application.DriveTrains.Queries;
using Application.DTOs.DriveTrain;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriveTrainController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DriveTrainController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("get-drivetrains")]
        public async Task<IResult> GetDriveTrains()
        {
            var driveTrains = await _mediator.Send(new GetAllDriveTrains());
            var driveTrainDTOs = _mapper.Map<IEnumerable<DriveTrainDTO>>(driveTrains);

            return TypedResults.Ok(driveTrainDTOs.ToList());
        }

        [HttpGet("get-drivetrain/{id}")]
        public async Task<IResult> GetById(int id)
        {

            var driveTrain = await _mediator.Send(new GetDriveTrainById { Id = id });
            var driveTrainDTO = _mapper.Map<DriveTrainDTO>(driveTrain);

            return TypedResults.Ok(driveTrainDTO);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDriveTrain([FromBody] DriveTrainDTO driveTrainDTO)
        {
            if (driveTrainDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var createCommand = _mapper.Map<CreateDriveTrain>(driveTrainDTO);
            var driveTrain = await _mediator.Send(createCommand);

            // Return the created product
            return CreatedAtAction("GetById", new { id = driveTrain.Id }, _mapper.Map<DriveTrainDTO>(driveTrain));
        }

        [HttpPut("update-drivetrain/{id}")]
        public async Task<IResult> UpdateDriveTrain(int id, [FromBody] DriveTrainDTO driveTrainDTO)
        {
            if (id != driveTrainDTO.Id) return Results.BadRequest();

            var command = _mapper.Map<UpdateDriveTrain>(driveTrainDTO);

            await _mediator.Send(command);

            return Results.NoContent();
        }

        [HttpDelete("delete-drivetrain/{id}")]
        public async Task<IResult> DeleteDriveTrain(int id)
        {
            await _mediator.Send(new DeleteDriveTrain { Id = id });

            return Results.NoContent();
        }
    }
}
