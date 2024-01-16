using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class MakeRepository : IMakeRepository
    {
        private readonly DataContext _ctx;
        public MakeRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }
        public async Task<Make> AddAsync(Make entity)
        {
            await _ctx.Set<Make>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<Make> UpdateAsync(Make entity)
        {
            _ctx.Set<Make>().Update(entity);
            await _ctx.SaveChangesAsync();

            return entity;
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
