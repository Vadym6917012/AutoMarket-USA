using AutoMarket.Server.Core;
using AutoMarket.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoMarket.Server.Infrastructure
{
    public class MakeRepository : IRepository<Make>
    {
        private readonly DataContext _ctx;
        public MakeRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }
        public async Task AddAsync(Make entity)
        {
            await _ctx.Set<Make>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Make entity)
        {
            _ctx.Set<Make>().Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Make entity)
        {
            _ctx.Set<Make>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Make> GetByIdAsync(int id)
        {
            return await _ctx.Set<Make>().FindAsync(id);
        }

        public Make GetById(int id)
        {
            return _ctx.Set<Make>().Find(id);
        }

        public async Task<Make> GetFirstAsync(Expression<Func<Make, bool>> expression)
        {
            return await _ctx.Set<Make>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Make>> GetAllAsync()
        {
            return await _ctx.Set<Make>().Include(m => m.Models).ToListAsync();
        }

        public Make GetMakeByModel(int modelId)
        {
            var make = _ctx.Set<Make>().Include(m => m.Models).Where(m => m.Models.Any(i => i.Id == modelId)).FirstOrDefault();

            return make;
        }

        public IEnumerable<Make> GetMakeByCountry(int id)
        {
            var makes = _ctx.Set<Make>()
                .Where(m => m.ProducingCountryId == id)
                .ToList();

            if (makes.Count == 0)
            {
                return _ctx.Set<Make>().ToList();
            }
            else
            {
                return makes;
            }
        }
    }
}
