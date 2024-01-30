using Microsoft.AspNetCore.Http;

namespace App.Contracts.BLL.Services;

public interface IImageService
{
     Task<string> SaveImageToFileSystemAsync(IFormFile imageFile);
     Task<List<string>> SaveImagesToFileSystemAsync(List<IFormFile> imageFiles);
}