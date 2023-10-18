using AutoMarket.Server.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoMarket.Server.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _ctx;
        public Repository(DataContext ctx) 
        { 
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }
        public async Task AddAsync(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _ctx.Set<T>().Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public T GetById(int id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression)
        {
            return await _ctx.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }
    }
}
