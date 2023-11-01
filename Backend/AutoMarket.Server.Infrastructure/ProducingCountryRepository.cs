using AutoMarket.Server.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoMarket.Server.Infrastructure
{
    public class ProducingCountryRepository : IRepository<ProducingCountry>
    {
        private readonly DataContext _ctx;
        public ProducingCountryRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }
        public async Task AddAsync(ProducingCountry entity)
        {
            await _ctx.Set<ProducingCountry>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProducingCountry entity)
        {
            _ctx.Set<ProducingCountry>().Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProducingCountry entity)
        {
            _ctx.Set<ProducingCountry>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ProducingCountry> GetByIdAsync(int id)
        {
            return await _ctx.Set<ProducingCountry>().FindAsync(id);
        }

        public ProducingCountry GetById(int id)
        {
            return _ctx.Set<ProducingCountry>().Find(id);
        }

        public async Task<ProducingCountry> GetFirstAsync(Expression<Func<ProducingCountry, bool>> expression)
        {
            return await _ctx.Set<ProducingCountry>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<ProducingCountry>> GetAllAsync()
        {
            return await _ctx.Set<ProducingCountry>().ToListAsync();
        }
    }
}
