using Microsoft.Extensions.DependencyInjection;

namespace Magnify.Repository
{
    public static class ServiceProviderExtensions 
    {

        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IShipmentRepository, ShipmentRepository>();

            return serviceCollection;
        }
    }
}
