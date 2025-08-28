using KopiBudget.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace KopiBudget.Infrastructure.Services
{
    public class FileService : IFileService
    {
        #region Public Methods

        private readonly IConfiguration _configuration;
        private readonly string FolderPath = "";

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
            FolderPath = _configuration["UploadSettings:UploadPath"]!;
        }

        public async Task<string> UploadImage(IFormFile img)
        {
            var uploadsPath = Path.Combine(FolderPath);
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + img.FileName;
            var filePath = Path.Combine(uploadsPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await img.CopyToAsync(stream);
            }
            return uniqueFileName;
        }

        #endregion Public Methods
    }
}