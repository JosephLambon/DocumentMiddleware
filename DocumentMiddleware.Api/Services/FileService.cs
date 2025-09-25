using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using DocumentMiddleware.Api.Services;

namespace DocumentMiddleware.Api.Services;
public class FileService(IWebHostEnvironment environment) : IFileService
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

        /*
         * Uploads files in /Uploads directory
         * Needs to instead upload to Azure blob storage
        */
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

        /* 
         *  Upload image to Blob Storage
         */
        // BlobServiceClient blobServiceClient = BlobStorageService.GetBlobServiceClient(Constants.BlobStorage.STORAGE_ACCOUNT_NAME);
        BlobServiceClient blobServiceClient = BlobStorageService.GetBlobServiceClientLocal();
        var containerClient = blobServiceClient.GetBlobContainerClient(Constants.BlobStorage.ANTIQUE_IMAGE_CONTAINER);
        
        var fileName = $"{Guid.NewGuid().ToString()}{ext}";
        var fileNameWithPath = Path.Combine(path, fileName);

        using var stream = new FileStream(fileNameWithPath, FileMode.Create);

        BlobClient blobClient = containerClient.GetBlobClient(fileName); // Create a new blob for image file
        await blobClient.UploadAsync(stream, true); // true causes overwrite of existing same fileName

        // Upload to local '/Uploads' folder
        await imageFile.CopyToAsync(stream);

        stream.Close();

        return fileName;
    }
}