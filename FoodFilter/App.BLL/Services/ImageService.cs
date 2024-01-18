using App.Contracts.BLL.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class ImageService : IImageService
    {
        public async Task<string> SaveImageToFileSystemAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(directory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{fileName}";
        }
    }
}