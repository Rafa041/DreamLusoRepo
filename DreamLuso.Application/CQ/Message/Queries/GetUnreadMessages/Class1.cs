using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Message.Queries.GetChatMessage;
using DreamLuso.Domain.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Message.Queries.GetUnreadMessages;

public class GetUnreadMessagesQuery : IRequest<Result<GetUnreadMessagesResponse, Success, Error>>
{
    public required Guid UserId { get; init; }
}

public class GetUnreadMessagesResponse
{
    public int TotalUnread { get; init; }
    public IEnumerable<MessageResponse> Messages { get; init; }
}

public class GetUnreadMessagesQueryHandler : IRequestHandler<GetUnreadMessagesQuery, Result<GetUnreadMessagesResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUnreadMessagesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetUnreadMessagesResponse, Success, Error>> Handle(GetUnreadMessagesQuery request, CancellationToken cancellationToken)
    {
        var unreadMessages = await _unitOfWork.MessageRepository.GetUnreadMessagesAsync(request.UserId, cancellationToken);

        var messageResponses = unreadMessages.Select(m => new MessageResponse
        {
            Id = m.Id,
            ChatId = m.ChatId,
            SenderId = m.SenderId,
            SenderName = $"{m.Chat.User.Name.FirstName} {m.Chat.User.Name.LastName}",
            SenderImageUrl = m.Chat.User.ImageUrl,
            Content = m.Content,
            SentAt = m.SentAt,
            IsRead = false,
            Type = m.Type,
            IsCurrentUserMessage = m.SenderId == request.UserId
        });

        return new GetUnreadMessagesResponse
        {
            TotalUnread = messageResponses.Count(),
            Messages = messageResponses
        };
    }
}

public class GetUnreadMessagesQueryValidator : AbstractValidator<GetUnreadMessagesQuery>
{
    public GetUnreadMessagesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}