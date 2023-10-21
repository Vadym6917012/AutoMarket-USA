﻿using AutoMapper;
using AutoMarket.Server.Core;
using AutoMarket.Server.Infrastructure;
using AutoMarket.Server.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerationController : ControllerBase
    {
        public readonly Repository<Generation> _repository;
        public readonly IMapper _mapper;

        public GenerationController(Repository<Generation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GenerationDTO>> GetGenerations()
        {
            var generations = await _repository.GetAllAsync();
            var generationDTOs = _mapper.Map<IEnumerable<GenerationDTO>>(generations);

            return generationDTOs.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<GenerationDTO> GetById(int id)
        {
            var generation = await _repository.GetByIdAsync(id);
            var generationDTO = _mapper.Map<GenerationDTO>(generation);

            return generationDTO;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGeneration([FromBody] GenerationDTO generationDTO)
        {
            if (generationDTO == null)
            {
                return BadRequest("Invalid generation data");
            }

            var generation = _mapper.Map<Generation>(generationDTO);
            await _repository.AddAsync(generation);

            // Return the created product
            return CreatedAtAction("GetById", new { id = generation.Id }, _mapper.Map<GenerationDTO>(generation));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateGeneration(int id, [FromBody] GenerationDTO generationDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (generationDTO == null)
            {
                return BadRequest("Invalid body type data");
            }

            _mapper.Map(generationDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<GenerationDTO>(existingEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteGeneration(int id)
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
