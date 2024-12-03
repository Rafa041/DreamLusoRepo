using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Message.Commands.CreateMessage;
using DreamLuso.Application.CQ.Message.Commands.MarkMessageRead;
using DreamLuso.Application.CQ.Message.Queries.GetChatMessage;
using DreamLuso.Application.CQ.Message.Queries.GetUnreadMessages;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

//public static class MessageEndpoints
//{
//    public static void RegisterMessageEndpoints(this IEndpointRouteBuilder routes)
//    {
//        var messages = routes.MapGroup("api/message");

//        messages.MapPost("/create", Queries.CreateMessage)
//            .WithName("CreateMessage")
//            .Produces<CreateMessageResponse>(200)
//            .Produces<Error>(400);

//        messages.MapGet("/chat/{chatId:guid}", Queries.GetChatMessages)
//            .WithName("GetChatMessages")
//            .Produces<List<MessageResponse>>(200)
//            .Produces<Error>(404);

//        messages.MapGet("/unread/{userId:guid}", Queries.GetUnreadMessages)
//            .WithName("GetUnreadMessages")
//            .Produces<List<MessageResponse>>(200)
//            .Produces<Error>(404);

//        messages.MapPatch("/{id:guid}/read", Queries.MarkMessageAsRead)
//            .WithName("MarkMessageAsRead")
//            .Produces<bool>(200)
//            .Produces<Error>(404);
//    }

//    private static class Queries
//    {
//        public static async Task<Results<Ok<CreateMessageResponse>, BadRequest<Error>>> CreateMessage(
//            [FromServices] ISender sender,
//            [FromBody] CreateMessageCommand command,
//            CancellationToken cancellationToken)
//        {
//            var result = await sender.Send(command, cancellationToken);
//            return result.IsSuccess
//                ? TypedResults.Ok(result.Value)
//                : TypedResults.BadRequest(result.Error);
//        }

//        public static async Task<Results<Ok<IEnumerable<MessageResponse>>, NotFound<Error>>> GetChatMessages(
//    [FromServices] ISender sender,
//    [FromRoute] Guid chatId,
//    CancellationToken cancellationToken)
//        {
//            var query = new GetChatMessagesQuery { ChatId = chatId };
//            var result = await sender.Send(query, cancellationToken);

//            if (!result.IsSuccess)
//                return TypedResults.NotFound(result.Error);

//            var messages = result.Value.Messages.Select(m => new MessageResponse
//            {
//                Id = m.Id,
//                ChatId = m.ChatId,
//                SenderId = m.SenderId,
//                SenderName = m.SenderName,
//                SenderImageUrl = m.SenderImageUrl,
//                Content = m.Content,
//                SentAt = m.SentAt,
//                IsRead = m.IsRead,
//                Type = m.Type
//            });

//            return TypedResults.Ok(messages);
//        }

//        public static async Task<Results<Ok<IEnumerable<MessageResponse>>, NotFound<Error>>> GetUnreadMessages(
//            [FromServices] ISender sender,
//            [FromRoute] Guid userId,
//            CancellationToken cancellationToken)
//        {
//            var query = new GetUnreadMessagesQuery { UserId = userId };
//            var result = await sender.Send(query, cancellationToken);
//            return result.IsSuccess
//                ? TypedResults.Ok(result.Value.Messages)
//                : TypedResults.NotFound(result.Error);
//        }

//        public static async Task<Results<Ok<bool>, NotFound<Error>>> MarkMessageAsRead(
//            [FromServices] ISender sender,
//            [FromRoute] Guid id,
//            CancellationToken cancellationToken)
//        {
//            var command = new MarkMessageAsReadCommand { MessageId = id };
//            var result = await sender.Send(command, cancellationToken);
//            return result.IsSuccess
//                ? TypedResults.Ok(true)
//                : TypedResults.NotFound(result.Error);
//        }
//    }
//}