using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.RealStateAgents.Commands.CreateRealStateAgent;
using DreamLuso.Application.CQ.RealStateAgents.Commands.UpdateRealStateAgent;
using DreamLuso.Application.CQ.RealStateAgents.Queries.Retrieve;
using DreamLuso.Application.CQ.RealStateAgents.Queries.RetrieveAll;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class RealStateAgentEndpoints
{
    public static void RegisterRealStateAgentEndpoints(this IEndpointRouteBuilder routes)
    {
        var agents = routes.MapGroup("api/realstateagent");//Ter atenção ao Authorization

        // ========== Create Real State Agent Endpoint ==========
        agents.MapPost("/create", Queries.CreateRealStateAgent)
            .WithName("CreateRealStateAgent")
            .Produces<CreateRealStateAgentResponse>(200)
            .Produces<Error>(400);

        // ========== Retrieve All Real State Agents Endpoint ==========
        agents.MapGet("/retrieveall", Queries.RetrieveAllRealStateAgents)
            .WithName("RetrieveAllRealStateAgents")
            .Produces<List<RetrieveRealStateAgentResponse>>(200)
            .Produces<Error>(404);

        // ========== Retrieve Real State Agent by Id Endpoint ==========
        agents.MapGet("/{id:guid}", Queries.RetrieveRealStateAgentById)
            .WithName("RetrieveRealStateAgent")
            .Produces<RetrieveRealStateAgentResponse>(200)
            .Produces<Error>(404);

        // ========== Update Real State Agent Endpoint ==========
        agents.MapPut("/{id:guid}", Queries.UpdateRealStateAgent)
            .WithName("UpdateRealStateAgent")
            .Produces<UpdateRealStateAgentResponse>(200)
            .Produces<Error>(404);
    }

    private static class Queries
    {
        // ========== Create Real State Agent ==========
        public static async Task<Results<Ok<bool>, BadRequest<Error>>> CreateRealStateAgent(
            [FromServices] ISender sender,
            [FromBody] CreateRealStateAgentCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.IsSuccess) : TypedResults.BadRequest(result.Error);
        }

        // ========== Retrieve All Real State Agents ==========
        public static async Task<Results<Ok<IEnumerable<RetrieveRealStateAgentResponse>>, NotFound<Error>>> RetrieveAllRealStateAgents(
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RetrieveAllRealStateAgentsQuery(), cancellationToken);
            return !result.IsSuccess ? TypedResults.NotFound(result.Error) : TypedResults.Ok(result.Value.RealStateAgents);
        }

        // ========== Retrieve Real State Agent by Id ==========
        public static async Task<Results<Ok<RetrieveRealStateAgentResponse>, NotFound<Error>>> RetrieveRealStateAgentById(
            [FromServices] ISender sender,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new RetrieveRealStateAgentQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }

        // ========== Update Real State Agent ==========
        public static async Task<Results<Ok<bool>, NotFound<string>>> UpdateRealStateAgent(
            [FromServices] ISender sender,
            [FromBody] UpdateRealStateAgentCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.IsSuccess) : TypedResults.NotFound("RealStateAgent not found.");
        }
    }
}
