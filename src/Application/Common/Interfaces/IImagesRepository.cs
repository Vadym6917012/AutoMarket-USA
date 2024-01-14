using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IImagesRepository<I> where I : class
    {
        Task AddAsync(Images entity);
        string AddImagesToDirectory(I images);
        string GetPhotoByName(string name);
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
