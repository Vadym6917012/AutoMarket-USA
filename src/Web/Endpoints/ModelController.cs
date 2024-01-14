﻿using Application.DTOs.Model;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelRepository _repository;
        private readonly IMapper _mapper;

        public ModelController(IModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get-models")]
        public async Task<IEnumerable<ModelDTO>> GetModels()
        {
            var models = await _repository.GetAllAsync();
            var modelDTOs = _mapper.Map<IEnumerable<ModelDTO>>(models);

            return modelDTOs.ToList();
        }

        [HttpGet("get-model/{id}")]
        public async Task<ModelDTO> GetById(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            var modelDTO = _mapper.Map<ModelDTO>(model);

            return modelDTO;
        }

        [HttpPost("create-model")]
        public async Task<IActionResult> CreateModel([FromBody] ModelDTO modelDTO)
        {
            if (modelDTO == null)
            {
                return BadRequest("Invalid model data");
            }

            var model = _mapper.Map<Model>(modelDTO);
            await _repository.AddAsync(model);

            // Return the created product
            return CreatedAtAction("GetById", new { id = model.Id }, _mapper.Map<ModelDTO>(model));
        }

        [HttpPost("create-with-generation")]
        public async Task<IActionResult> CreateWithGeneration([FromBody] ModelDTO modelDTO, string name, int yearFrom, int yearTo)
        {
            if (modelDTO == null)
            {
                return BadRequest("Invalid model data");
            }

            var model = _mapper.Map<Model>(modelDTO);
            await _repository.AddWithGeneration(model, name, yearFrom, yearTo);

            var createdModelDTO = _mapper.Map<ModelDTO>(model);

            // Return the created product
            return CreatedAtAction("GetById", new { id = createdModelDTO.Id }, createdModelDTO);
        }

        [HttpPut("update-model/{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] ModelDTO modelDTO)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (modelDTO == null)
            {
                return BadRequest("Invalid model data");
            }

            _mapper.Map(modelDTO, existingEntity);
            await _repository.UpdateAsync(existingEntity);

            return Ok(_mapper.Map<ModelDTO>(existingEntity));
        }

        [HttpDelete("delete-model/{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var existingEntity = _repository.GetById(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(existingEntity);

            return NoContent();
        }

        [HttpGet("get-model-by-make/{makeId:int}")]
        public IActionResult GetModelsByMake(int makeId)
        {
            var model = _repository.GetModelByMake(makeId);
            return Ok(model);
        }
    }
}