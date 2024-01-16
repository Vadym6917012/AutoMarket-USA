using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly DataContext _ctx;

        public ImagesRepository(DataContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
        }

        public async Task AddAsync(Images entities)
        {
            _ctx.Set<Images>().Add(entities);
            await _ctx.SaveChangesAsync();
        }

        public void RemoveImages(ICollection<Images> images)
        {
            if (images != null)
            {
                foreach (var image in images)
                {
                    var existImages = GetById(image.Id);

                    if (existImages != null)
                    {
                        var imagePath = existImages.ImagePath;

                        if (!string.Equals(Path.GetFileName(imagePath), "NoImage.png", StringComparison.OrdinalIgnoreCase))
                        {
                            File.Delete(imagePath);
                        }
                    }
                }
            }
        }

        public async Task DeleteAsync(Images entity)
        {
            _ctx.Set<Images>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Images entity)
        {
            _ctx.Set<Images>().Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Images>> GetByCarIdAsync(int id)
        {
            return await _ctx.Set<Images>().Where(p => p.CarId == id).ToListAsync();
        }

        public Images GetById(int id)
        {
            return _ctx.Set<Images>().Find(id);
        }

        public async Task<Images> GetByIdAsync(int id)
        {
            return await _ctx.Set<Images>().FindAsync(id);
        }

        public async Task<IEnumerable<Images>> GetAllAsync()
        {
            return await _ctx.Set<Images>().ToListAsync();
        }

        public async Task<Images> GetFirstAsync(Expression<Func<Images, bool>> expression)
        {
            return await _ctx.Set<Images>().FirstOrDefaultAsync(expression);
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
