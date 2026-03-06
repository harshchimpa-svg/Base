using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, string folderName);
    }
}
