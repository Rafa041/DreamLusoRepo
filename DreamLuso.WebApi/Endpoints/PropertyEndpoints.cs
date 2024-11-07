using Azure.Core;
using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.Properties.Commands.UpdateProperty;
using DreamLuso.Application.CQ.Properties.Queries.GetTotalSales;
using DreamLuso.Application.CQ.Properties.Queries.Retrieve;
using DreamLuso.Application.CQ.Properties.Queries.RetrieveAgentPropertiesQuery;
using DreamLuso.Application.CQ.Properties.Queries.RetrieveAll;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;
using DreamLuso.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class PropertyEndpoints
{
    
    public static void RegisterPropertyEndpoints(this IEndpointRouteBuilder routes)
    {
        var properties = routes.MapGroup("api/property");

        // ========== Get Total Sales Endpoint ==========
        properties.MapGet("/GetTotalSales", Queries.GetTotalSales)
            .WithName("GetTotalSales")
            .Produces<GetTotalSalesResponse>(200)
            .Produces<Error>(404);

        // ========== Get Property by ID Endpoint ==========
        properties.MapGet("/retrieve/{id:guid}", Queries.RetrievePropertyId)
            .WithName("Retrieve")
            .Produces<RetrievePropertyResponse>(200)
            .Produces<Error>(404);
        // ========== Retrieve All Users Endpoint ==========
        properties.MapGet("/retrieveall", Queries.RetrieveAllProperties)
            .WithName("RetrieveAllProperties")
            .Produces<List<RetrieveAllPropertiesResponse>>(200)
            .Produces<Error>(404);

        properties.MapGet("/agent/{agentId:guid}", Queries.RetrieveAgentProperties)
        .WithName("RetrieveAgentProperties")
        .Produces<List<RetrieveAllPropertiesResponse>>(200)
        .Produces<Error>(404);

        properties.MapPut("/{id:guid}", Commands.UpdateProperty)
        .WithName("UpdateProperty")
        .Produces<UpdatePropertyResponse>(200)
        .Produces<Error>(404)
        .Produces<Error>(400);
    }
    private static class Queries
    {
     
        public static async Task<Results<Ok<GetTotalSalesResponse>, NotFound<Error>>> GetTotalSales(
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetTotalSalesQuery(), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.Error);
        }
        // ========== Retrieve Property by ID ==========
        public static async Task<Results<Ok<RetrievePropertyResponse>, NotFound<Error>>> RetrievePropertyId(
            [FromRoute] Guid id,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var query = new RetrievePropertyQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.Error);
        }
        // ========== Retrieve All Users ==========
        public static async Task<Results<Ok<IEnumerable<RetrievePropertyResponse>>, NotFound<Error>>> RetrieveAllProperties(
    [FromServices] ISender sender,
    CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RetrieveAllPropertiesQuery(), cancellationToken);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Properties)  // Retornar a lista de propriedades
                : TypedResults.NotFound(result.Error);  
        }
        public static async Task<Results<Ok<IEnumerable<RetrievePropertyResponse>>, NotFound<Error>>> RetrieveAgentProperties(
        [FromRoute] Guid agentId,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        {
            var query = new RetrieveAgentPropertiesQuery { AgentId = agentId };
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Properties)
                : TypedResults.NotFound(result.Error);
        }
    }
    private static class Commands
    {
        public static async Task<Results<Ok<UpdatePropertyResponse>, NotFound<Error>, BadRequest<Error>>> UpdateProperty(
            [FromRoute] Guid id,
            [FromForm] UpdatePropertyCommand command,
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return TypedResults.BadRequest(Error.UpdateFailed);

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : result.Error switch
                {
                    _ when result.Error == Error.PropertyNotFound => TypedResults.NotFound(result.Error),
                    _ when result.Error == Error.RealStateAgentNotFound => TypedResults.NotFound(result.Error),
                    _ => TypedResults.BadRequest(result.Error)
                };
        }
    }
}
