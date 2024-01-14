using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IMakeRepository
    {
        Task AddAsync(Make entity);
        Task UpdateAsync(Make entity);
        Task DeleteAsync(Make entity);
        Task<Make> GetByIdAsync(int id);
        Make GetById(int id);
        Task<Make> GetFirstAsync(Expression<Func<Make, bool>> expression);
        Task<IEnumerable<Make>> GetAllAsync();
        Make GetMakeByModel(int modelId);
        IEnumerable<Make> GetMakeByCountry(int id);
    }
}
