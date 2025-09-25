using Azure.Identity;
using Azure.Storage.Blobs;

namespace DocumentMiddleware.Api.Services
{
    public static class BlobStorageService
    {
        public static BlobServiceClient GetBlobServiceClient(string accountName)
        {
            BlobServiceClient client = new(
                new Uri($"https://{accountName}.blob.core.windows.net"),
                new DefaultAzureCredential()
                );

            return client;
        }
        
        public static BlobServiceClient GetBlobServiceClientLocal()
        {
            BlobServiceClient client = new BlobServiceClient("UseDevelopmentStorage=true",
                new DefaultAzureCredential()
                );
            return client;
        }
    }
}
