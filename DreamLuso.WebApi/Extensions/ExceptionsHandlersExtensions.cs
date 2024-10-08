using DreamLuso.WebApi.ExceptionHandlers;

namespace DreamLuso.WebApi.Extensions;

public static class ExceptionsHandlersExtensions
{
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}
