using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DreamLuso.WebApi.Extensions;

public static class JwtTokenConfig
{
    public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"]!,
                        ValidateAudience = true,
                        ValidAudience = configuration["JwtSettings:Audience"]!,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                    };
                });
        return services;
    }
}
