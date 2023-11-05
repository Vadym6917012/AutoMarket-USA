using AutoMapper;
using AutoMarket.Server.Core.Models;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs.DriveTrain;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriveTrainController : ControllerBase
    {
        private readonly Repository<DriveTrain> _repository;
        private readonly IMapper _mapper;

        public DriveTrainController(Repository<DriveTrain> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DriveTrainDTO>> GetDriveTrains()
        {
            var driveTrains = await _repository.GetAllAsync();
            var driveTrainDTOs = _mapper.Map<IEnumerable<DriveTrainDTO>>(driveTrains);

            return driveTrainDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<DriveTrainDTO> GetById(int id)
        {
            var driveTrain = await _repository.GetByIdAsync(id);
            var driveTrainDTO = _mapper.Map<DriveTrainDTO>(driveTrain);

            return driveTrainDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriveTrain([FromBody] DriveTrainDTO driveTrainDTO)
        {
            if (driveTrainDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var driveTrain = _mapper.Map<DriveTrain>(driveTrainDTO);
            await _repository.AddAsync(driveTrain);

            // Return the created product
            return CreatedAtAction("GetById", new { id = driveTrain.Id }, _mapper.Map<DriveTrainDTO>(driveTrain));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateDriveTrain(int id, [FromBody] DriveTrainDTO driveTrainDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (driveTrainDTO == null)
            {
                return BadRequest("Invalid data");
            }

            _mapper.Map(driveTrainDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<DriveTrainDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteDriveTrain(int id)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(existingEntity);

            return NoContent();
        }
    }
}
