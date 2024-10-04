using DreamLuso.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DreamLuso.Data.Infrastructure;

public class FileStorageService : IFileStorageService
{
    private readonly string _uploadDirectory;
    public FileStorageService(IConfiguration configuration)
    {
        _uploadDirectory = configuration.GetValue<string>("FileStorage:UploadDirectory")
                               ?? Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        if (!Directory.Exists(_uploadDirectory))
        {
            Directory.CreateDirectory(_uploadDirectory);
        }
    }
    public async Task<string> SaveFileAsync(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty or null", nameof(file));
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(_uploadDirectory, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        return fileName;
    }

    public Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("File name is empty or null", nameof(fileName));
        }

        var filePath = Path.Combine(_uploadDirectory, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}
