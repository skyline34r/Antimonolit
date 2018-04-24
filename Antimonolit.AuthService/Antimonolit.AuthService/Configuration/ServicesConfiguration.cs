using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Antimonolith.AuthenticationService.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthService, Auth1Service>();

            return serviceCollection;
        }
    }
}
