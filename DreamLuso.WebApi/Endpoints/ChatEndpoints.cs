using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Chat.Commands.CreateChat;
using DreamLuso.Application.CQ.Chat.Commands.UpdateChatStatus;
using DreamLuso.Application.CQ.Chat.Queries.GetAgentChat;
using DreamLuso.Application.CQ.Chat.Queries.GetUserChat;
using DreamLuso.Application.CQ.Chat.Queries.RetrieveAllChat;
using DreamLuso.Application.CQ.Message.Queries.RetrieveChat;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class ChatEndpoints
{
    public static void RegisterChatEndpoints(this IEndpointRouteBuilder routes)
    {
        var chats = routes.MapGroup("api/chat");

        chats.MapPost("/create", Queries.CreateChat)
            .WithName("CreateChat")
            .Produces<CreateChatResponse>(200)
            .Produces<Error>(400);

        chats.MapGet("/retrieveall", Queries.RetrieveAllChats)
            .WithName("RetrieveAllChats")
            .Produces<List<RetrieveChatResponse>>(200)
            .Produces<Error>(404);

        chats.MapGet("/{id:guid}", Queries.RetrieveChatById)
            .WithName("RetrieveChat")
            .Produces<RetrieveChatResponse>(200)
            .Produces<Error>(404);

        chats.MapGet("/user/{userId:guid}", Queries.GetUserChats)
            .WithName("GetUserChats")
            .Produces<List<RetrieveChatResponse>>(200)
            .Produces<Error>(404);

        chats.MapPatch("/{id:guid}/status", Queries.UpdateChatStatus)
            .WithName("UpdateChatStatus")
            .Produces<bool>(200)
            .Produces<Error>(404);

        chats.MapGet("/realestateagent/{agentId:guid}", Queries.GetRealEstateAgentChats)
        .WithName("GetRealEstateAgentChats")
        .Produces<List<RetrieveChatResponse>>(200)
        .Produces<Error>(404);
    }

    private static class Queries
    {
        public static async Task<Results<Ok<CreateChatResponse>, BadRequest<Error>>> CreateChat(
            [FromServices] ISender sender,
            [FromBody] CreateChatCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Error);
        }

        public static async Task<Results<Ok<IEnumerable<ChatDto>>, NotFound<Error>>> RetrieveAllChats(
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RetrieveAllChatsQuery(), cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Chats)
                : TypedResults.NotFound(result.Error);
        }

        public static async Task<Results<Ok<RetrieveChatResponse>, NotFound<Error>>> RetrieveChatById(
            [FromServices] ISender sender,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new RetrieveChatQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.NotFound(result.Error);
        }

        public static async Task<Results<Ok<IEnumerable<UserChatDto>>, NotFound<Error>>> GetUserChats(
    [FromServices] ISender sender,
    [FromRoute] Guid userId,
    CancellationToken cancellationToken)
        {
            var query = new GetUserChatsQuery { UserId = userId };
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Chats)
                : TypedResults.NotFound(result.Error);
        }


        public static async Task<Results<Ok<bool>, NotFound<Error>>> UpdateChatStatus(
            [FromServices] ISender sender,
            [FromRoute] Guid id,
            [FromBody] UpdateChatStatusCommand command,
            CancellationToken cancellationToken)
        {
            var updateCommand = new UpdateChatStatusCommand
            {
                ChatId = id,
                Status = command.Status
            };

            var result = await sender.Send(updateCommand, cancellationToken);
            return result.IsSuccess
                ? TypedResults.Ok(true)
                : TypedResults.NotFound(result.Error);
        }
        public static async Task<Results<Ok<IEnumerable<RealEstateAgentChatDto>>, NotFound<Error>>> GetRealEstateAgentChats(
       [FromServices] ISender sender,
       [FromRoute] Guid agentId,
       CancellationToken cancellationToken)
        {
            var query = new GetRealEstateAgentChatsQuery { RealEstateAgentId = agentId };
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Chats)
                : TypedResults.NotFound(result.Error);
        }
    }
}