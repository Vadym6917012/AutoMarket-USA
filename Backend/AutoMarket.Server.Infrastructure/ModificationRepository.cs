using AutoMarket.Server.Core;
using AutoMarket.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoMarket.Server.Infrastructure
{
    public class ModificationRepository : IRepository<Modification>
    {
        private readonly DataContext _ctx;
        public ModificationRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }
        public async Task AddAsync(Modification entity)
        {
            await _ctx.Set<Modification>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Modification entity)
        {
            _ctx.Set<Modification>().Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Modification entity)
        {
            _ctx.Set<Modification>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Modification> GetByIdAsync(int id)
        {
            return await _ctx.Set<Modification>().FindAsync(id);
        }

        public Modification GetById(int id)
        {
            return _ctx.Set<Modification>().Find(id);
        }

        public async Task<Modification> GetFirstAsync(Expression<Func<Modification, bool>> expression)
        {
            return await _ctx.Set<Modification>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Modification>> GetAllAsync()
        {
            return await _ctx.Set<Modification>().ToListAsync();
        }

        public IEnumerable<Modification> GetModificationByModel(int id)
        {
            return _ctx.Set<Modification>()
                .Where(m => m.ModelId == id)
                .ToList();
        }
    }
}
