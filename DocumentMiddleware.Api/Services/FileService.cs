using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using DocumentMiddleware.Api.Services;
using DocumentMiddleware.Core.Models;

namespace DocumentMiddleware.Api.Services;
public class FileService(IWebHostEnvironment environment, ILogger<Antique> logger) : IFileService
{
    public async Task<string> UploadFileAsync(
        IFormFile imageFile,
        string[] allowedFileExtensions
        )
    {
        if (imageFile == null)
        {
            throw new ArgumentNullException(nameof(imageFile));
        }

        var contentPath = environment.ContentRootPath;
        var path = Path.Combine(contentPath, "Uploads");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var ext = Path.GetExtension(imageFile.FileName);
        if (!allowedFileExtensions.Contains(ext))
        {
            throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
        }

        // Update to automatically configure local/remote setup
        logger.LogInformation("Connecting to storage container...");
        // BlobServiceClient blobServiceClient = BlobStorageService.GetBlobContainerClient(Constants.BlobStorage.STORAGE_ACCOUNT_NAME);
        BlobContainerClient blobContainerClient = BlobStorageService.GetBlobContainerClientLocal();
        
        var fileName = $"{Guid.NewGuid().ToString()}{ext}";
        var fileNameWithPath = Path.Combine(path, fileName);

        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName); // Create a new blob for image file
        
        await using (var stream = imageFile.OpenReadStream())
        {
            logger.LogInformation("Uploading blob...");
            await blobClient.UploadAsync(stream, true); // true causes overwrite of existing same fileName
        };
        
        logger.LogInformation("Blob upload succeeded.");
        return fileName;
    }
}