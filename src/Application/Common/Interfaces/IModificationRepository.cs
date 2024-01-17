using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IModificationRepository
    {
        Task<Modification> AddAsync(Modification entity);
        Task UpdateAsync(Modification entity);
        Task DeleteAsync(Modification entity);
        Task<Modification> GetByIdAsync(int id);
        Modification GetById(int id);
        Task<Modification> GetFirstAsync(Expression<Func<Modification, bool>> expression);
        Task<IEnumerable<Modification>> GetAllAsync();
        Task<IEnumerable<Modification>> GetModificationByModel(int id);
    }
}
