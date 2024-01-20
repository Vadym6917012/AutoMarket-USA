using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IGenerationRepository
    {
        Task AddAsync(Generation entity);
        Task AddToModelAsync(int modelId, Generation entity);
        Task<Generation> UpdateAsync(Generation entity);
        Task DeleteAsync(Generation entity);
        Task<Generation> GetByIdAsync(int id);
        Generation GetById(int id);
        Task<IEnumerable<Generation>> GetGenerationsByModel(int modelId);
        Task<Generation> GetFirstAsync(Expression<Func<Generation, bool>> expression);
        Task<IEnumerable<Generation>> GetAllAsync();
    }
}
