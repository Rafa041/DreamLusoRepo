using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Addresses.Commands.CreateAddress;
using DreamLuso.Application.CQ.Addresses.Commands.UpdateAddress;
using DreamLuso.Application.CQ.Addresses.Queries.Retrieve;
using DreamLuso.Application.CQ.Addresses.Queries.RetrieveAllAddress;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class AddressEndpoints
{
    public static void RegisterAddressEndpoints(this IEndpointRouteBuilder routes)
    {
        
        var addresses = routes.MapGroup("api/address").RequireAuthorization();

        // ========== Retrieve All Addresses Endpoint ==========
        addresses.MapGet("/retrieveall", Queries.RetrieveAllAddresses)
            .WithName("RetrieveAllAddresses")
            .Produces<List<RetrieveAddressResponse>>(200)
            .Produces<Error>(404);
            

        // ========== Retrieve Address by Id Endpoint ==========
        addresses.MapGet("/{id:guid}", Queries.RetrieveAddressById)
            .WithName("RetrieveAddress")
            .Produces<RetrieveAddressResponse>(200)
            .Produces<Error>(404);

        // ========== Create Address Endpoint ==========
        addresses.MapPost("/create", Queries.CreateAddress)
            .WithName("CreateAddress")
            .Produces<CreateAddressResponse>(200)
            .Produces<Error>(400);

        // ========== Update Address Endpoint ==========
        addresses.MapPut("/{id:guid}", Queries.UpdateAddress)
            .WithName("UpdateAddress")
            .Produces<UpdateAddressResponse>(200)
            .Produces<Error>(404);
    }

    private static class Queries
    {
        // ========== Retrieve All Addresses ==========
        public static async Task<Results<Ok<IEnumerable<RetrieveAddressResponse>>, NotFound<Error>>> RetrieveAllAddresses(
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RetrieveAllAddressQuery(), cancellationToken);
            return !result.IsSuccess ? TypedResults.NotFound(result.Error) : TypedResults.Ok(result.Value.AddressResponses);
        }

        // ========== Retrieve Address by Id ==========
        public static async Task<Results<Ok<RetrieveAddressResponse>, NotFound<Error>>> RetrieveAddressById(
            [FromServices] ISender sender,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new RetrieveAddressQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }

        // ========== Create Address ==========
        public static async Task<Results<Ok<bool>, BadRequest<Error>>> CreateAddress(
            [FromServices] ISender sender,
            [FromBody] CreateAddressCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.IsSuccess) : TypedResults.BadRequest(result.Error);
        }

        // ========== Update Address ==========
        public static async Task<Results<Ok<bool>, NotFound<string>>> UpdateAddress(
            [FromServices] ISender sender,
            [FromBody] UpdateAddressCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result != null && result.IsSuccess ? TypedResults.Ok(result.IsSuccess) : TypedResults.NotFound("Address not found.");
        }
    }
}
