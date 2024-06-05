using Microsoft.Extensions.DependencyInjection;
using DreamLuso.Data;
using DreamLuso.Application;
using Microsoft.Extensions.Configuration;

namespace DreamLuso.IoC;
public static class DependencyInjection
{
    public static IServiceCollection AddIoCServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataServices(configuration);
        services.AddApplicationServices(configuration);

        return services;
    }
}
