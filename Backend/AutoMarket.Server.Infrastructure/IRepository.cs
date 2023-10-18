using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
       
    }
}
