using Application.Common.Interfaces;
using Application.DTOs.Car;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _ctx;
        private readonly IImagesRepository _imagesRepository;
        private readonly IConfiguration _config;

        public CarRepository(DataContext ctx, IImagesRepository imagesRepository, IConfiguration config)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
            _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(_imagesRepository));
            _config = config;
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

        public async Task<VinLookupResponse> CheckVin(string vin)
        {
            var vinLookupResponse = new VinLookupResponse();

            using (var client = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(@"https://api.api-ninjas.com/v1/vinlookup?vin=" + vin),
                        Method = HttpMethod.Get,
                    };

                    request.Headers.Add("X-Api-Key", _config["VinLookup:ApiKey"]);

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        vinLookupResponse = JsonConvert.DeserializeObject<VinLookupResponse>(responseContent);

                        var car = await _ctx.Set<Car>()
                            .Include(x => x.Model)
                            .ThenInclude(x => x.Make)
                            .ThenInclude(x => x.ProducingCountry)
                            .FirstOrDefaultAsync(c => c.VIN == vinLookupResponse.Vin);

                        if (car != null)
                        {
                            bool matchesAllFields = car.Model.Make.ProducingCountry.Name == vinLookupResponse.Country || car.Year == vinLookupResponse.Year;

                            vinLookupResponse.IsFound = true;

                            if (matchesAllFields)
                            {
                                vinLookupResponse.Model = car.Model.Name;
                                vinLookupResponse.Manufacturer = car.Model.Make.Name;
                                vinLookupResponse.Year = car.Year;
                                vinLookupResponse.Country = car.Model.Make.ProducingCountry.Name;

                                vinLookupResponse.Message = "VIN-код успішно перевірено та підтверджено.";
                            }
                            else
                            {
                                vinLookupResponse.Message = "Перевірка VIN-коду виявила розбіжності з даним оголошенням. Будьте уважні при купівлі цього автомобіля.";
                            }
                        } 
                        else
                        {
                            vinLookupResponse.IsFound = false;
                            vinLookupResponse.Message = "У базі даних за таким VIN не знайдено відповідного автомобіля";
                        }

                    }
                    else
                    {
                        vinLookupResponse.IsFound = false;
                        vinLookupResponse.Message = "Помилка API-запиту";
                    }

                    return vinLookupResponse;
                }
                catch (HttpRequestException e)
                {
                    vinLookupResponse.IsFound = false;
                    vinLookupResponse.Message = "Помилка API-запиту: " + e.Message;
                    return vinLookupResponse;
                }
                catch (Exception e)
                {
                    vinLookupResponse.IsFound = false;
                    vinLookupResponse.Message = "Загальна помилка: " + e.Message;
                    return vinLookupResponse;
                }
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


