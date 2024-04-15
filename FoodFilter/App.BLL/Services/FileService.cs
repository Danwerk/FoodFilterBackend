using App.Contracts.BLL.Services;
using Microsoft.AspNetCore.Http;
using App.Domain;

namespace App.BLL.Services
{
    public class FileService : IFileService
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

        public async Task<List<string>> SaveImagesToFileSystemAsync(List<IFormFile> imageFiles)
        {
            var imagePaths = new List<string>();

            foreach (var imageFile in imageFiles)
            {
                var imagePath = await SaveImageToFileSystemAsync(imageFile);
                imagePaths.Add(imagePath);
            }

            return imagePaths;
        }

        public async Task DeleteImageFromFileSystemAsync(string imageUrl)
        {
            try
            {
                // Extract the filename from the URL
                string fileName = Path.GetFileName(imageUrl);
                if (string.IsNullOrEmpty(fileName))
                {
                    throw new ArgumentException("Invalid image URL");
                }

                // Construct the file path based on the directory where images are stored
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var filePath = Path.Combine(directory, fileName);

                // Check if the file exists before attempting to delete it
                if (File.Exists(filePath))
                {
                    // Delete the image file from the file system
                    File.Delete(filePath);
                }
                else
                {
                    throw new FileNotFoundException("Image file not found", filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete image from file system", ex);
            }
        }
    }
}