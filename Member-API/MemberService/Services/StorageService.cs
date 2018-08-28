using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace MemberService.Services
{
    public class StorageService : IStorageService
    {
        private readonly CloudStorageAccount account;

        public StorageService(CloudStorageAccount account)
        {
            this.account = account;
        }

        private async Task<CloudBlobContainer> GetBlobContainer()
        {
            var blobClient = account.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("images");
            if (await blobContainer.CreateIfNotExistsAsync())
            {
                await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }); 
            }
            return blobContainer;
        }

        public async Task<string> UploadPicture(IFormFile image,long profileId)
        {
            var blobContainer = await this.GetBlobContainer();
            var imageExtension = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            var blob = blobContainer.GetBlockBlobReference(profileId.ToString()+imageExtension);
            await blob.UploadFromStreamAsync(image.OpenReadStream());
            return blob.Uri.ToString();
        }
    }
}
