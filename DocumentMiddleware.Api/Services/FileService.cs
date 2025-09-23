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
         * Needs to instaead upload to Azure blob storage
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

        var fileName = $"{Guid.NewGuid().ToString()}{ext}";
        var fileNameWithPath = Path.Combine(path, fileName);


        /*
         * Replace with Azure Storage upload
         */
        using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        await imageFile.CopyToAsync(stream);


        return fileName;
    }
}