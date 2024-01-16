using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IImagesRepository
    {
        Task AddAsync(Images entity);
        void RemoveImages(ICollection<Images> images);
        Task DeleteAsync(Images entity);
        Task UpdateAsync(Images entity);
        Task<IEnumerable<Images>> GetByCarIdAsync(int id);
        Images GetById(int id);
        Task<Images> GetByIdAsync(int id);
        Task<IEnumerable<Images>> GetAllAsync();
        Task<Images> GetFirstAsync(Expression<Func<Images, bool>> expression);
        Task SaveChangesAsync();
    }
}
