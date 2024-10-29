namespace DreamLuso.WebApi.Endpoints;

public static class RegisterAllEndpoints
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.RegisterAddressEndpoints();
        routes.RegisterAuthEndpoints();
        routes.RegisterRealStateAgentEndpoints();
        routes.RegisterUserEndpoints();
        routes.RegisterPropertyEndpoints();
        routes.RegisterNotificationEndpoints();
    }
}

