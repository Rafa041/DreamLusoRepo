using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Message.Queries.RetrieveChat;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Message.Queries.GetChatMessage;

public class GetChatMessagesQuery : IRequest<Result<GetChatMessagesResponse, Success, Error>>
{
    public required Guid ChatId { get; init; }
}

public class GetChatMessagesResponse
{
    public Guid ChatId { get; init; }
    public IEnumerable<MessageDto> Messages { get; init; }
}
public class MessageResponse
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; }
    public string SenderImageUrl { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
    public MessageType Type { get; set; }
    public bool IsCurrentUserMessage { get; set; }
    public DateTime? ReadAt { get; set; }
}
public class GetChatMessagesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetChatMessagesQuery, Result<GetChatMessagesResponse, Success, Error>>
{


    public async Task<Result<GetChatMessagesResponse, Success, Error>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await unitOfWork.MessageRepository.GetChatMessagesAsync(request.ChatId, cancellationToken);

        var messageDtos = messages.Select(m => new MessageDto
        {
            Id = m.Id,
            ChatId = m.ChatId,
            SenderId = m.SenderId,
            SenderName = m.Chat?.User != null
                ? $"{m.Chat.User.Name.FirstName} {m.Chat.User.Name.LastName}"
                : "Unknown User",
            Content = m.Content,
            SentAt = m.SentAt,
            IsRead = m.IsRead,
            Type = m.Type,
            SenderImageUrl = m.Chat?.User?.ImageUrl
        });

        return new GetChatMessagesResponse
        {
            ChatId = request.ChatId,
            Messages = messageDtos
        };
    }
}

public class GetChatMessagesQueryValidator : AbstractValidator<GetChatMessagesQuery>
{
    public GetChatMessagesQueryValidator()
    {
        RuleFor(x => x.ChatId)
            .NotEmpty().WithMessage("ChatId is required.");
    }
}