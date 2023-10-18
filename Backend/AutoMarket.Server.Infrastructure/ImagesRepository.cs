using AutoMarket.Server.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Infrastructure
{
    public class ImagesRepository
    {
        private readonly DataContext _ctx;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImagesRepository(DataContext ctx, IHostingEnvironment hostingEnvironment)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(_hostingEnvironment));
        }

        public string AddImages(IFormFile images)
        {
            if (images != null && images.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + images.FileName;
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "User Photos");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);

                }
                var filePathCombained = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePathCombained, FileMode.Create))
                {
                    images.CopyTo(fileStream);
                }

                return filePathCombained;
            }
            else
            {
                return null;
            }
        }

        public void RemoveImages(ICollection<Images> images)
        {
            if (images != null)
            {
                foreach (var image in images)
                {
                    var existImages = GetById(image.Id);

                    var imagePath = existImages.ImagePath;

                    if (existImages != null)
                    {
                        File.Delete(imagePath);
                    }
                }
            }
        }

        public async Task DeleteAsync(Images entity)
        {
            _ctx.Set<Images>().Remove(entity);
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

        public async Task<IEnumerable<Images>> GetAllAsync()
        {
            return await _ctx.Set<Images>().ToListAsync();
        }
    }
}
