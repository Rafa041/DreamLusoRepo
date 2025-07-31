namespace DreamLuso.WebApi.Endpoints;

public static class RegisterAllEndpoints
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.RegisterAddressEndpoints();
        routes.RegisterAuthEndpoints();
        routes.RegisterRealEstateAgentEndpoints();
        routes.RegisterUserEndpoints();
        routes.RegisterPropertyEndpoints();
        routes.RegisterNotificationEndpoints();
        routes.RegisterPropertyVisitEndpoints();
        //routes.RegisterMessageEndpoints();
        routes.RegisterChatEndpoints();
    }
}

