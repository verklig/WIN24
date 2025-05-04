using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public interface IImageService
{
  bool Delete(string relativePath);
  Task<string> UploadAsync(IFormFile file, string folder);
}

public class ImageService(IWebHostEnvironment env) : IImageService
{
  private readonly string _rootPath = env.WebRootPath;

  public async Task<string> UploadAsync(IFormFile file, string folder)
  {
    if (file == null || file.Length == 0)
    {
      throw new ArgumentException("Invalid image file.");
    }

    var uploadsFolder = Path.Combine(_rootPath, "uploads", folder);
    Directory.CreateDirectory(uploadsFolder);

    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
    var filePath = Path.Combine(uploadsFolder, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }

    return $"/uploads/{folder}/{fileName}";
  }

  // Not using this yet
  public bool Delete(string relativePath)
  {
    if (string.IsNullOrWhiteSpace(relativePath))
    {
      return false;
    }

    var fullPath = Path.Combine(_rootPath, relativePath.TrimStart('/'));
    if (File.Exists(fullPath))
    {
      File.Delete(fullPath);
      return true;
    }

    return false;
  }
}