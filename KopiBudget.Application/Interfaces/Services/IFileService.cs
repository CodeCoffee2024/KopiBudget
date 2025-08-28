using Microsoft.AspNetCore.Http;

namespace KopiBudget.Application.Interfaces.Services
{
    public interface IFileService
    {
        #region Public Methods

        Task<string> UploadImage(IFormFile file);

        //string UploadFile(IFormFile file);

        #endregion Public Methods
    }
}