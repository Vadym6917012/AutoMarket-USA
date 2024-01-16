using Application.Common.Interfaces;
using Application.DTOs.Car;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _ctx;
        private readonly IImagesRepository _imagesRepository;

        public CarRepository(DataContext ctx, IImagesRepository imagesRepository)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
            _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(_imagesRepository));
        }
        public async Task<Car> AddAsync(Car entity)
        {
            await _ctx.Set<Car>().AddAsync(entity);
            await _ctx.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> CheckYear(Car entity)
        {
            var isVadlidGeneration = await _ctx.Set<Generation>().FirstOrDefaultAsync(g =>
            entity.Year >= g.YearFrom && entity.Year <= g.YearTo);

            if (isVadlidGeneration == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Car> UpdateAsync(Car entity)
        {
            _ctx.Set<Car>().Update(entity);
            await _ctx.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Car entity)
        {
            var imagesToDelete = _ctx.Set<Images>().Where(image => image.CarId == entity.Id);

            _imagesRepository.RemoveImages(imagesToDelete.ToList());

            _ctx.Set<Images>().RemoveRange(imagesToDelete);
            _ctx.Set<Car>().Remove(entity);

            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetNotVerifiedCarsAsync()
        {
            return await _ctx.Set<Car>()
                .Include(m => m.Model).ThenInclude(m => m.Make).ThenInclude(c => c.ProducingCountry)
                .Include(m => m.Modification)
                .Include(g => g.Generation)
                .Include(b => b.BodyType)
                .Include(g => g.GearBoxType)
                .Include(d => d.DriveTrain)
                .Include(t => t.TechnicalCondition)
                .Include(f => f.FuelType)
                .Include(i => i.Images)
                .Include(u => u.User)
                .Where(x => x.IsAdvertisementApproved == false)
                .ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _ctx.Set<Car>().Include(m => m.Model).ThenInclude(m => m.Make).ThenInclude(c => c.ProducingCountry)
                .Include(m => m.Modification)
                .Include(g => g.Generation)
                .Include(b => b.BodyType)
                .Include(g => g.GearBoxType)
                .Include(d => d.DriveTrain)
                .Include(t => t.TechnicalCondition)
                .Include(f => f.FuelType)
                .Include(u => u.User)
                .Include(i => i.Images).FirstAsync(c => c.Id == id);
        }

        public Car GetById(int id)
        {
            return _ctx.Set<Car>().Find(id);
        }

        public async Task<string> CheckVin(string vin)
        {
            try
            {
                var apiUrl = $"https://api.api-ninjas.com/v1/vinlookup?vin={vin}";

                using (var client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(apiUrl))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        return responseBody;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<Car>> HomeFilter(CarHomeFilter filter)
        {
            var query = _ctx.Set<Car>().AsQueryable();

            if (filter.MakeId.HasValue)
            {
                query = query.Include(src => src.Model).ThenInclude(src => src.Make).Where(car => car.Model.Make.Id == filter.MakeId);
            }

            if (filter.ModelId.HasValue)
            {
                query = query.Where(car => car.ModelId == filter.ModelId);
            }

            if (filter.PriceFrom.HasValue)
            {
                query = query.Where(car => car.Price >= filter.PriceFrom);
            }

            if (filter.PriceTo.HasValue)
            {
                query = query.Where(car => car.Price <= filter.PriceTo);
            }

            query = query.Include(m => m.Model).ThenInclude(m => m.Make).ThenInclude(c => c.ProducingCountry)
                .Include(m => m.Modification)
                .Include(g => g.Generation)
                .Include(b => b.BodyType)
                .Include(g => g.GearBoxType)
                .Include(d => d.DriveTrain)
                .Include(t => t.TechnicalCondition)
                .Include(f => f.FuelType)
                .Include(u => u.User)
                .Include(i => i.Images);

            var filteredCars = await query.ToListAsync();

            return filteredCars;
        }

        public async Task<IEnumerable<Car>> CarFilter(CarFilter filter)
        {
            var query = _ctx.Set<Car>().AsQueryable();

            if (filter.MakeId.HasValue)
            {
                query = query.Include(src => src.Model).ThenInclude(src => src.Make).Where(car => car.Model.Make.Id == filter.MakeId);
            }

            if (filter.ModelId.HasValue)
            {
                query = query.Where(car => car.ModelId == filter.ModelId);
            }

            if (filter.GenerationId.HasValue)
            {
                query = query.Where(car => car.GenerationId == filter.GenerationId);
            }

            if (filter.ModificationId.HasValue)
            {
                query = query.Where(car => car.ModificationId == filter.ModificationId);
            }

            if (filter.BodyTypeId.HasValue)
            {
                query = query.Where(car => car.BodyTypeId == filter.BodyTypeId);
            }

            if (filter.GearBoxTypeId.HasValue)
            {
                query = query.Where(car => car.GearBoxTypeId == filter.GearBoxTypeId);
            }

            if (filter.DriveTrainId.HasValue)
            {
                query = query.Where(car => car.DriveTrainId == filter.DriveTrainId);
            }

            if (filter.TechnicalConditionId.HasValue)
            {
                query = query.Where(car => car.TechnicalConditionId == filter.TechnicalConditionId);
            }

            if (filter.FuelTypeId.HasValue)
            {
                query = query.Where(car => car.FuelTypeId == filter.FuelTypeId);
            }

            if (filter.YearFrom.HasValue)
            {
                query = query.Where(car => car.Year >= filter.YearFrom);
            }

            if (filter.YearTo.HasValue)
            {
                query = query.Where(car => car.Year <= filter.YearTo);
            }

            if (filter.MileageFrom.HasValue)
            {
                query = query.Where(car => car.Mileage >= filter.MileageFrom);
            }

            if (filter.MileageTo.HasValue)
            {
                query = query.Where(car => car.Mileage <= filter.MileageTo);
            }

            if (filter.PriceFrom.HasValue)
            {
                query = query.Where(car => car.Price >= filter.PriceFrom);
            }

            if (filter.PriceTo.HasValue)
            {
                query = query.Where(car => car.Price <= filter.PriceTo);
            }

            query = query.Include(m => m.Model).ThenInclude(m => m.Make).ThenInclude(c => c.ProducingCountry)
                .Include(m => m.Modification)
                .Include(g => g.Generation)
                .Include(b => b.BodyType)
                .Include(g => g.GearBoxType)
                .Include(d => d.DriveTrain)
                .Include(t => t.TechnicalCondition)
                .Include(f => f.FuelType)
                .Include(u => u.User)
                .Include(i => i.Images);

            var filteredCars = await query.ToListAsync();

            return filteredCars;
        }

        public async Task<IEnumerable<Car>> GetRecentCars(int count)
        {
            return await _ctx.Cars
               .OrderByDescending(car => car.DateCreated)
               .Take(count)
               .Include(m => m.Model).ThenInclude(m => m.Make).ThenInclude(c => c.ProducingCountry)
                .Include(m => m.Modification)
                .Include(g => g.Generation)
                .Include(b => b.BodyType)
                .Include(g => g.GearBoxType)
                .Include(d => d.DriveTrain)
                .Include(t => t.TechnicalCondition)
                .Include(f => f.FuelType)
                .Include(i => i.Images)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task<Car> GetFirstAsync(Expression<Func<Car, bool>> expression)
        {
            return await _ctx.Set<Car>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Car>> GetCarByUserId(string userId)
        {
            return await _ctx.Set<Car>().Where(u => u.UserId == userId).Include(m => m.Model).ThenInclude(m => m.Make).ThenInclude(c => c.ProducingCountry)
                .Include(m => m.Modification)
                .Include(g => g.Generation)
                .Include(b => b.BodyType)
                .Include(g => g.GearBoxType)
                .Include(d => d.DriveTrain)
                .Include(t => t.TechnicalCondition)
                .Include(f => f.FuelType)
                .Include(i => i.Images)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _ctx.Set<Car>()
                .Include(m => m.Model).ThenInclude(m => m.Make).ThenInclude(c => c.ProducingCountry)
                .Include(m => m.Modification)
                .Include(g => g.Generation)
                .Include(b => b.BodyType)
                .Include(g => g.GearBoxType)
                .Include(d => d.DriveTrain)
                .Include(t => t.TechnicalCondition)
                .Include(f => f.FuelType)
                .Include(i => i.Images)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}


