using Application.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration; 

        public FileService(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env ;
            _configuration = configuration;
        }

        public async Task<string> UploadAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty");

            var uploads = Path.Combine(_env.WebRootPath, "uploads", folderName);

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploads, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            
            var image = _configuration["Image:ImagePath"] ?? "";

            return $"{image}/uploads/{folderName}/{fileName}";
        }
    }
}