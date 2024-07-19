using DreamLuso.Security.Interfaces;
using DreamLuso.Security.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DreamLuso.Security
{
    public static class DependencyInjection
    {
        //JWT
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataProtectionService, DataProtectionService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
