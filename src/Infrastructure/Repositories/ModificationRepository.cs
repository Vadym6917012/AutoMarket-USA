using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ModificationRepository : IModificationRepository
    {
        private readonly DataContext _ctx;
        public ModificationRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }
        public async Task<Modification> AddAsync(Modification entity)
        {
            var addedEntity = await _ctx.Set<Modification>().AddAsync(entity);
            await _ctx.SaveChangesAsync();

            return addedEntity.Entity;
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

        public async Task<IEnumerable<Modification>> GetModificationByModel(int id)
        {
            return await _ctx.Set<Modification>()
                .Where(m => m.ModelId == id)
                .ToListAsync();
        }
    }
}
