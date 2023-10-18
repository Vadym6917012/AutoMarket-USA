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
            await _ctx.Set<Car>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task AddAsyncWithImages(Car entity, List<IFormFile> images)
        {
            await _ctx.Set<Car>().AddAsync(entity);

            Upload(entity, images);

            _ctx.SaveChanges();
        }

        public void Upload(Car entity, List<IFormFile> images)
        {
            if (images != null)
            {
                foreach (var image in images)
                {
                    var filePath = _imagesRepository.AddImages(image);

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        var imageAdded = new Images { ImagePath = filePath, CarId = entity.Id, Car = entity };
                        _ctx.Set<Images>().AddAsync(imageAdded);
                        _ctx.SaveChanges();
                    }
                }
            }
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
            return await _ctx.Set<Car>().Include(i => i.Images).ToListAsync();
        }
    }
}
