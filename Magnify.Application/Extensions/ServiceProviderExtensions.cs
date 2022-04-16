using Magnify.Application.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Magnify.Application.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddMediatR(typeof(ServiceProviderExtensions));

            return serviceCollection;
        }
    }
}
