using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.Properties.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class PropertyEndpoints
{
    public static void RegisterPropertyEndpoints(this IEndpointRouteBuilder routes)
    {
        var properties = routes.MapGroup("api/property");

        // ========== Register Property Endpoint ==========
        properties.MapPost("/register", Queries.RegisterProperty)
            .WithName("RegisterProperty")
            .Produces<CreatePropertyResponse>(200)
            .Produces<Error>(400)
            .DisableAntiforgery(); // Desabilitar proteção contra CSRF, se necessário

        // ========== Get Total Sales Endpoint ==========
        properties.MapGet("/GetTotalSales", Queries.GetTotalSales)
            .WithName("GetTotalSales")
            .Produces<GetTotalSalesResponse>(200)
            .Produces<Error>(404);

        // ========== Retrieve All Properties Endpoint ==========
        //properties.MapGet("/retrieveall", Queries.RetrieveAllProperties)
        //    .WithName("RetrieveAllProperties")
        //    .Produces<List<RetrievePropertyResponse>>(200)
        //    .Produces<Error>(404);

        // ========== Update Property Endpoint ==========
        //properties.MapPut("/{id:guid}", Queries.UpdateProperty)
        //    .WithName("UpdateProperty")
        //    .Produces<UpdatePropertyResponse>(200)
        //    .Produces<Error>(404);

        // ========== Retrieve Property by Id Endpoint ==========
        //properties.MapGet("/{id:guid}", Queries.RetrievePropertyById)
        //    .WithName("RetrieveProperty")
        //    .Produces<RetrievePropertyResponse>(200)
        //    .Produces<Error>(404)
        //    .DisableAntiforgery();
    }

    private static class Queries
    {
        // ========== Register Property ==========
        public static async Task<Results<Ok<CreatePropertyResponse>, BadRequest<Error>>> RegisterProperty(
            [FromServices] ISender sender,
            [FromForm] CreatePropertyCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Error);
        }
        // ========== Get Total Sales ==========
        public static async Task<Results<Ok<GetTotalSalesResponse>, NotFound<Error>>> GetTotalSales(
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetTotalSalesQuery(), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.Error);
        }
        // ========== Retrieve All Properties ==========
        //public static async Task<Results<Ok<IEnumerable<RetrievePropertyResponse>>, NotFound<Error>>> RetrieveAllProperties(
        //    [FromServices] ISender sender,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await sender.Send(new RetrieveAllPropertiesQuery(), cancellationToken);
        //    return !result.IsSuccess ? TypedResults.NotFound(result.Error) : TypedResults.Ok(result.Value.Properties);
        //}

        // ========== Update Property ==========
        //public static async Task<Results<Ok<bool>, NotFound<string>>> UpdateProperty(
        //    [FromServices] ISender sender,
        //    [FromBody] UpdatePropertyCommand command)
        //{
        //    var result = await sender.Send(command);
        //    return result.IsSuccess ? TypedResults.Ok(result.IsSuccess) : TypedResults.NotFound("Property not found.");
        //}

        // ========== Retrieve Property by Id ==========
        //public static async Task<Results<Ok<RetrievePropertyResponse>, NotFound<Error>>> RetrievePropertyById(
        //    [FromServices] ISender sender,
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var query = new RetrievePropertyQuery { Id = id };
        //    var result = await sender.Send(query, cancellationToken);
        //    return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        //}
    }
}
