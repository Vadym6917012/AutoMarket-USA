﻿using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly DataContext _ctx;
        public ModelRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }
        public async Task<Model> AddAsync(Model entity)
        {
            var addedEntity = await _ctx.Set<Model>().AddAsync(entity);
            await _ctx.SaveChangesAsync();

            return addedEntity.Entity;
        }

        public async Task AddWithGeneration(Model entity, string generationName, int yearFrom, int yearTo)
        {
            var existEntity = GetById(entity.Id);

            if (existEntity == null)
            {
                await _ctx.Set<Model>().AddAsync(entity);
            }
            else
            {
                return;
            }

            var existingGeneration = _ctx.Set<Generation>().FirstOrDefault(g => g.Name == generationName);

            if (existingGeneration == null)
            {
                var newGeneration = new Generation { Name = generationName, YearFrom = yearFrom, YearTo = yearTo };
                _ctx.Set<Generation>().Add(newGeneration);
                _ctx.Entry(newGeneration).State = EntityState.Detached;
                _ctx.SaveChanges();
                _ctx.Set<ModelGeneration>().Add(new ModelGeneration { ModelId = entity.Id, Model = entity, GenerationId = newGeneration.Id, Generation = newGeneration });
                _ctx.SaveChanges();
            }
            else
            {
                _ctx.Set<ModelGeneration>().Add(new ModelGeneration { ModelId = entity.Id, Model = entity, GenerationId = existingGeneration.Id, Generation = existingGeneration });
                _ctx.SaveChanges();
            }
        }

        public async Task AddGenerationToModel(Model entity, string generationName, int yearFrom, int yearTo)
        {
            await _ctx.Set<Model>().AddAsync(entity);

            var existingGeneration = _ctx.Set<Generation>().FirstOrDefault(g => g.Name == generationName && g.YearFrom == yearFrom && g.YearTo == yearTo);

            if (existingGeneration == null)
            {
                var newGeneration = new Generation { Name = generationName, YearFrom = yearFrom, YearTo = yearTo };
                _ctx.Set<Generation>().Add(newGeneration);
                _ctx.Entry(newGeneration).State = EntityState.Detached;
                _ctx.SaveChanges();
                _ctx.Set<ModelGeneration>().Add(new ModelGeneration { ModelId = entity.Id, Model = entity, GenerationId = newGeneration.Id, Generation = newGeneration });
                _ctx.SaveChanges();
            }
            else
            {
                _ctx.Set<ModelGeneration>().Add(new ModelGeneration { ModelId = entity.Id, Model = entity, GenerationId = existingGeneration.Id, Generation = existingGeneration });
                _ctx.SaveChanges();
            }
        }

        public async Task<Model> UpdateAsync(Model entity)
        {
            _ctx.Set<Model>().Update(entity);
            await _ctx.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Model entity)
        {
            _ctx.Set<Model>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Model> GetByIdAsync(int id)
        {
            return await _ctx.Set<Model>().FindAsync(id);
        }

        public Model GetById(int id)
        {
            return _ctx.Set<Model>().Find(id);
        }

        public async Task<Model> GetFirstAsync(Expression<Func<Model, bool>> expression)
        {
            return await _ctx.Set<Model>().Include(m => m.ModelGenerations).ThenInclude(mg => mg.Generation).FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _ctx.Set<Model>().Include(m => m.ModelGenerations).ThenInclude(mg => mg.Generation).ToListAsync();
        }

        public async Task<IEnumerable<Model>> GetModelByMake(int makeId)
        {
            return _ctx.Set<Model>()
                .Where(m => m.MakeId == makeId)
                .ToList();
        }
    }
}
