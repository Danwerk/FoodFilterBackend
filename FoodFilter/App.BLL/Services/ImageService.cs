using App.Contracts.BLL.Services;
using Microsoft.AspNetCore.Http;

namespace App.BLL.Services;

public class ImageService : IImageService
{
    public async Task<string> SaveImageToFileSystemAsync(IFormFile file)
    {
        string fileNameWithPath = "";
        if (file.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/RestaurantImages");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        return fileNameWithPath;
    }
}