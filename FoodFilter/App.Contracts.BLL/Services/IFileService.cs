using App.Domain;
using Microsoft.AspNetCore.Http;

namespace App.Contracts.BLL.Services;

public interface IFileService
{
     Task<string> SaveImageToFileSystemAsync(IFormFile imageFile);
     Task<List<string>> SaveImagesToFileSystemAsync(List<IFormFile> imageFiles);
     Task DeleteImageFromFileSystemAsync(string imageUrl);


}