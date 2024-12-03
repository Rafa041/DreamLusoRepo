using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Chat.Queries.GetUserChat;

public class GetUserChatsQuery : IRequest<Result<GetUserChatsResponse, Success, Error>>
{
    public required Guid UserId { get; init; }
}

public class GetUserChatsResponse
{
    public IEnumerable<UserChatDto> Chats { get; set; }
}

public class UserChatDto
{
    public Guid Id { get; init; }
    public Guid PropertyId { get; init; }
    public string PropertyTitle { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; }
    public Guid RealStateAgentId { get; init; }
    public string RealStateAgentName { get; init; }
    public ChatStatus Status { get; init; }
    public DateTime LastMessageAt { get; init; }
    public int UnreadMessagesCount { get; init; }
    public string LastMessageContent { get; init; }
}

public class GetUserChatsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserChatsQuery, Result<GetUserChatsResponse, Success, Error>>
{


    public async Task<Result<GetUserChatsResponse, Success, Error>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await unitOfWork.ChatRepository.GetUserChatsAsync(request.UserId, cancellationToken);

        var chatDtos = chats.Select(c => new UserChatDto
        {
            Id = c.Id,
            PropertyId = c.PropertyId,
            PropertyTitle = c.Property.Title,
            UserId = c.UserId,
            UserName = $"{c.User.Name.FirstName} {c.User.Name.LastName}",
            RealStateAgentId = c.RealStateAgentId,
            RealStateAgentName = $"{c.RealStateAgent.User.Name.FirstName} {c.RealStateAgent.User.Name.LastName}",
            Status = c.Status,
            LastMessageAt = c.LastMessageAt,
            UnreadMessagesCount = c.Messages.Count(m => !m.IsRead),
            LastMessageContent = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.Content ?? string.Empty
        }).ToList();

        return new GetUserChatsResponse { Chats = chatDtos };
    }
}
