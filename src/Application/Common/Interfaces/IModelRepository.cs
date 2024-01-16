using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IModelRepository
    {
        Task<Model> AddAsync(Model entity);
        Task AddWithGeneration(Model entity, string generationName, int yearFrom, int yearTo);
        Task AddGenerationToModel(Model entity, string generationName, int yearFrom, int yearTo);
        Task<Model> UpdateAsync(Model entity);
        Task DeleteAsync(Model entity);
        Task<Model> GetByIdAsync(int id);
        Model GetById(int id);
        Task<Model> GetFirstAsync(Expression<Func<Model, bool>> expression);
        Task<IEnumerable<Model>> GetAllAsync();
        Task<IEnumerable<Model>> GetModelByMake(int makeId);
    }
}
