using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IMakeRepository
    {
        Task<Make> AddAsync(Make entity);
        Task<Make> UpdateAsync(Make entity);
        Task DeleteAsync(Make entity);
        Task<Make> GetByIdAsync(int id);
        Make GetById(int id);
        Task<Make> GetFirstAsync(Expression<Func<Make, bool>> expression);
        Task<IEnumerable<Make>> GetAllAsync();
        IEnumerable<Make> GetMakeByCountry(int id);
    }
}
