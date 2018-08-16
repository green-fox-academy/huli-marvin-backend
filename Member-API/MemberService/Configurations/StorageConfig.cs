using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;

namespace MemberService.Configurations
{
    public static class StorageConfig
    {
        public static void AddStorage(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            //var storageAccount = CloudStorageAccount.Parse($"{Configuration["StorageConnection"]}");
            //services.AddSingleton(storageAccount);
        }
    }
}
