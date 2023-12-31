﻿using AutoMarket.Server.Core;
using AutoMarket.Server.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Server.Infrastructure
{
    public class ImagesRepository
    {
        private readonly DataContext _ctx;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImagesRepository(DataContext ctx, IWebHostEnvironment hostingEnvironment)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(_hostingEnvironment));
        }

        public async Task AddAsync(Images entities)
        {
            _ctx.Set<Images>().Add(entities);
            await _ctx.SaveChangesAsync();
        }

        public string AddImagesToDirectory(IFormFile images)
        {
            if (images == null || images.Length == 0)
            {
                var uniqueFileName = "NoImage.png";
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "User Photos");
                var filePathCombained = Path.Combine(uploadsFolder, uniqueFileName);

                return filePathCombained;
            }
            else if (images != null && images.Length > 0)
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

        public string GetPhotoByName(string name)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "User Photos", name);

            if (File.Exists(filePath))
            {
                return filePath;
            }
            return null;
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

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
