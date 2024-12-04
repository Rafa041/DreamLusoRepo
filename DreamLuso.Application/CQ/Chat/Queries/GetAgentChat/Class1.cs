using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Chat.Queries.GetAgentChat;

public class GetRealStateAgentChatsQuery : IRequest<Result<GetRealStateAgentChatsResponse, Success, Error>>
{
    public required Guid AgentId { get; init; }
}

public class GetRealStateAgentChatsResponse
{
    public IEnumerable<RealStateAgentChatDto> Chats { get; set; }
}

public class RealStateAgentChatDto
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

public class GetRealStateAgentChatsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRealStateAgentChatsQuery, Result<GetRealStateAgentChatsResponse, Success, Error>>
{

    public async Task<Result<GetRealStateAgentChatsResponse, Success, Error>> Handle(GetRealStateAgentChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await unitOfWork.ChatRepository.GetRealStateAgentChatsAsync(request.AgentId, cancellationToken);

        var chatDtos = chats.Select(c => new RealStateAgentChatDto
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

        return new GetRealStateAgentChatsResponse { Chats = chatDtos };
    }
}