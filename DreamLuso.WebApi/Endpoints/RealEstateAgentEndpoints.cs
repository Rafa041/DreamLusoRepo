using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.RealEstateAgents.Commands.CreateRealEstateAgent;
using DreamLuso.Application.CQ.RealEstateAgents.Commands.UpdateRealEstateAgent;
using DreamLuso.Application.CQ.RealEstateAgents.Queries.RetrieveRealEstateAgent;
using DreamLuso.Application.CQ.RealEstateAgents.Queries.RetrieveAll;
using DreamLuso.Application.CQ.RealEstateAgents.Queries.RetrieveByUserId;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class RealEstateAgentEndpoints
{
    public static void RegisterRealEstateAgentEndpoints(this IEndpointRouteBuilder routes)
    {
        var agents = routes.MapGroup("api/realestateagent");

        // ========== Create Real Estate Agent Endpoint ==========
        agents.MapPost("/create", Queries.CreateRealEstateAgent)
            .WithName("CreateRealEstateAgent")
            .Produces<CreateRealEstateAgentResponse>(200)
            .Produces<Error>(400)
            .DisableAntiforgery();

        // ========== Retrieve All Real Estate Agents Endpoint ==========
        agents.MapGet("/retrieveall", Queries.RetrieveAllRealEstateAgents)
            .WithName("RetrieveAllRealEstateAgents")
            .Produces<List<RetrieveRealEstateAgentResponse>>(200)
            .Produces<Error>(404);

        // ========== Retrieve Real Estate Agent by Id Endpoint ==========
        agents.MapGet("/{id:guid}", Queries.RetrieveRealEstateAgentById)
            .WithName("RetrieveRealEstateAgent")
            .Produces<RetrieveRealEstateAgentResponse>(200)
            .Produces<Error>(404);

        // ========== Update Real Estate Agent Endpoint ==========
        agents.MapPut("/{id:guid}", Queries.UpdateRealEstateAgent)
            .WithName("UpdateRealEstateAgent")
            .Produces<UpdateRealEstateAgentResponse>(200)
            .Produces<Error>(404);
        // ========== Retrieve Real Estate Agent by UserId Endpoint ==========
        agents.MapGet("/user/{userId:guid}", Queries.RetrieveRealEstateAgentByUserId)
            .WithName("RetrieveRealEstateAgentByUserId")
            .Produces<RetrieveRealEstateAgentResponse>(200)
            .Produces<Error>(404);

    }

    private static class Queries
    {
        // ========== Create Real Estate Agent ==========
        public static async Task<Results<Ok<bool>, BadRequest<Error>>> CreateRealEstateAgent(
            [FromServices] ISender sender,
            [FromBody] CreateRealEstateAgentCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.IsSuccess) : TypedResults.BadRequest(result.Error);
        }

        // ========== Retrieve All Real Estate Agents ==========
        public static async Task<Results<Ok<IEnumerable<RetrieveRealEstateAgentResponse>>, NotFound<Error>>> RetrieveAllRealEstateAgents(
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RetrieveAllRealEstateAgentsQuery(), cancellationToken);
            return !result.IsSuccess ? TypedResults.NotFound(result.Error) : TypedResults.Ok(result.Value.RealEstateAgents);
        }

        // ========== Retrieve Real Estate Agent by Id ==========
        public static async Task<Results<Ok<RetrieveRealEstateAgentResponse>, NotFound<Error>>> RetrieveRealEstateAgentById(
            [FromServices] ISender sender,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new RetrieveRealEstateAgentQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }

        // ========== Update Real Estate Agent ==========
        public static async Task<Results<Ok<bool>, NotFound<string>>> UpdateRealEstateAgent(
            [FromServices] ISender sender,
            [FromBody] UpdateRealEstateAgentCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.IsSuccess) : TypedResults.NotFound("RealEstateAgent not found.");
        }
        // ========== Retrieve Real Estate Agent by UserId ==========
        public static async Task<Results<Ok<RetrieveRealEstateAgentResponse>, NotFound<Error>>> RetrieveRealEstateAgentByUserId(
            [FromServices] ISender sender,
            [FromRoute] Guid userId,
            CancellationToken cancellationToken)
        {
            var query = new RetrieveRealEstateAgentByUserIdQuery { UserId = userId };
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }
    }
}
