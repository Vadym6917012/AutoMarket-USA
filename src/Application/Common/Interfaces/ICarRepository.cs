using Application.DTOs.Car;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface ICarRepository
    {
        Task AddAsync(Car entity);
        Task<bool> CheckYear(Car entity);
        Task UpdateAsync(Car entity);
        Task DeleteAsync(Car entity);
        Task<IEnumerable<Car>> GetNotVerifiedCarsAsync();
        Task<Car> GetByIdAsync(int id);
        Car GetById(int id);
        Task<string> CheckVin(string vin);
        Task<IEnumerable<Car>> HomeFilter(CarHomeFilter filter);
        Task<IEnumerable<Car>> CarFilter(CarFilter filter);
        Task<IEnumerable<Car>> GetByCount(int count);
        Task<Car> GetFirstAsync(Expression<Func<Car, bool>> expression);
        Task<IEnumerable<Car>> GetCarByUserId(string userId);
        Task<IEnumerable<Car>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
