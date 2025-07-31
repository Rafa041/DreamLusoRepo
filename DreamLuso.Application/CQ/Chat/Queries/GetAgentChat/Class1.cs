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

public class GetRealEstateAgentChatsQuery : IRequest<Result<GetRealEstateAgentChatsResponse, Success, Error>>
{
    public required Guid RealEstateAgentId { get; init; }
}

    public class GetRealEstateAgentChatsResponse
{
    public IEnumerable<RealEstateAgentChatDto> Chats { get; set; }
}

    public class RealEstateAgentChatDto
{
    public Guid Id { get; init; }
    public Guid PropertyId { get; init; }
    public string PropertyTitle { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; }
    public Guid RealEstateAgentId { get; init; }
            public string RealEstateAgentName { get; init; }
    public ChatStatus Status { get; init; }
    public DateTime LastMessageAt { get; init; }
    public int UnreadMessagesCount { get; init; }
    public string LastMessageContent { get; init; }
}

public class GetRealEstateAgentChatsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRealEstateAgentChatsQuery, Result<GetRealEstateAgentChatsResponse, Success, Error>>
{

    public async Task<Result<GetRealEstateAgentChatsResponse, Success, Error>> Handle(GetRealEstateAgentChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await unitOfWork.ChatRepository.GetRealEstateAgentChatsAsync(request.RealEstateAgentId, cancellationToken);

        var chatDtos = chats.Select(c => new RealEstateAgentChatDto
        {
            Id = c.Id,
            PropertyId = c.PropertyId,
            PropertyTitle = c.Property.Title,
            UserId = c.UserId,
            UserName = $"{c.User.Name.FirstName} {c.User.Name.LastName}",
            RealEstateAgentId = c.RealEstateAgentId,
            RealEstateAgentName = $"{c.RealEstateAgent.User.Name.FirstName} {c.RealEstateAgent.User.Name.LastName}",
            Status = c.Status,
            LastMessageAt = c.LastMessageAt,
            UnreadMessagesCount = c.Messages.Count(m => !m.IsRead),
            LastMessageContent = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.Content ?? string.Empty
        }).ToList();

        return new GetRealEstateAgentChatsResponse { Chats = chatDtos };
    }
}