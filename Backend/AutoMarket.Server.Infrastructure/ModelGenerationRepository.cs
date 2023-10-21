using AutoMarket.Server.Core;
using System.Linq.Expressions;

namespace AutoMarket.Server.Infrastructure
{
    public class ModelGenerationRepository : IRepository<ModelGeneration>
    {
        private readonly DataContext _ctx;
        public ModelGenerationRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }

        public async Task AddAsync(ModelGeneration entity)
        {
            await _ctx.Set<ModelGeneration>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public Task DeleteAsync(ModelGeneration entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ModelGeneration>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ModelGeneration GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ModelGeneration> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ModelGeneration> GetFirstAsync(Expression<Func<ModelGeneration, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ModelGeneration entity)
        {
            throw new NotImplementedException();
        }
    }
}
