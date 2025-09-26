using DocumentMiddleware.Core.Models;

namespace DocumentMiddleware.Api.Services;
public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile imageFile, string[] allowedFileExtensions);
}