using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();

    }
}
