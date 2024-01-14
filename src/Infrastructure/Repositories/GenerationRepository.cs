using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenerationRepository : IGenerationRepository
    {
        private readonly DataContext _ctx;
        public GenerationRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }
        public async Task AddAsync(Generation entity)
        {
            await _ctx.Set<Generation>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task AddToModelAsync(int modelId, Generation entity)
        {
            var existModel = await _ctx.Set<Model>().FirstOrDefaultAsync(x => x.Id == modelId);

            if (existModel != null)
            {
                var existGeneration = await _ctx.Set<Generation>().FirstOrDefaultAsync(g => g.Name == entity.Name && g.YearFrom == entity.YearFrom && g.YearTo == entity.YearTo);

                if (existGeneration == null)
                {
                    await _ctx.Set<Generation>().AddAsync(entity);

                    await _ctx.SaveChangesAsync();

                    _ctx.Set<ModelGeneration>().Add(new ModelGeneration { ModelId = modelId, GenerationId = entity.Id });
                }
                else
                {
                    _ctx.Set<ModelGeneration>().Add(new ModelGeneration { ModelId = modelId, GenerationId = entity.Id });
                }
            }

            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Generation entity)
        {
            _ctx.Set<Generation>().Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Generation entity)
        {
            _ctx.Set<Generation>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Generation> GetByIdAsync(int id)
        {
            return await _ctx.Set<Generation>().FindAsync(id);
        }

        public Generation GetById(int id)
        {
            return _ctx.Set<Generation>().Find(id);
        }

        public IEnumerable<Generation> GetGenerationsByModel(int modelId)
        {
            return _ctx.Set<ModelGeneration>()
                .Where(mg => mg.ModelId == modelId)
                .Select(mg => mg.Generation)
                .ToList();
        }

        public async Task<Generation> GetFirstAsync(Expression<Func<Generation, bool>> expression)
        {
            return await _ctx.Set<Generation>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Generation>> GetAllAsync()
        {
            return await _ctx.Set<Generation>().Include(mg => mg.ModelGenerations).ThenInclude(m => m.Model).ToListAsync();
        }
    }
}
