using AutoMarket.Server.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoMarket.Server.Infrastructure
{
    public class CarRepository : IRepository<Car>
    {
        private readonly DataContext _ctx;
        private readonly ImagesRepository _imagesRepository;

        public CarRepository(DataContext ctx, ImagesRepository imagesRepository)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
            _imagesRepository = imagesRepository ?? throw new ArgumentNullException(nameof(_imagesRepository));
        }
        public async Task AddAsync(Car entity)
        {
            _ctx.Set<Car>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car entity)
        {
            _ctx.Set<Car>().Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Car entity)
        {
            var imagesToDelete = _ctx.Set<Images>().Where(image => image.CarId == entity.Id);

            _imagesRepository.RemoveImages(imagesToDelete.ToList());

            _ctx.Set<Images>().RemoveRange(imagesToDelete);
            _ctx.Set<Car>().Remove(entity);

            await _ctx.SaveChangesAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _ctx.Set<Car>().FindAsync(id);
        }

        public Car GetById(int id)
        {
            return _ctx.Set<Car>().Find(id);
        }

        public async Task<Car> GetFirstAsync(Expression<Func<Car, bool>> expression)
        {
            return await _ctx.Set<Car>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _ctx.Set<Car>()
                .Include(m => m.Model)
                .Include(m => m.Modification)
                .Include(g => g.Generation)
                .Include(b => b.BodyType)
                .Include(g => g.GearBoxType)
                .Include(f => f.FuelType)
                .Include(i => i.Images)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
           await _ctx.SaveChangesAsync();
        }
    }
}


