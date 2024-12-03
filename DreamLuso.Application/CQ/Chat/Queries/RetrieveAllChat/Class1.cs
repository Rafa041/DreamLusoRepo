using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Chat.Queries.RetrieveAllChat;

public class RetrieveAllChatsQuery : IRequest<Result<RetrieveAllChatsResponse, Success, Error>>
{
}

public class RetrieveAllChatsResponse
{
    public IEnumerable<ChatDto> Chats { get; init; }
}

public class ChatDto
{
    public Guid Id { get; init; }
    public Guid PropertyId { get; init; }
    public Guid UserId { get; init; }
    public Guid RealStateAgentId { get; init; }
    public ChatStatus Status { get; init; }
    public DateTime LastMessageAt { get; init; }
    public int UnreadMessagesCount { get; init; }
}

public class RetrieveAllChatsQueryHandler : IRequestHandler<RetrieveAllChatsQuery, Result<RetrieveAllChatsResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RetrieveAllChatsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RetrieveAllChatsResponse, Success, Error>> Handle(RetrieveAllChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _unitOfWork.ChatRepository.RetrieveAllAsync(cancellationToken);

        var chatDtos = chats.Select(c => new ChatDto
        {
            Id = c.Id,
            PropertyId = c.PropertyId,
            UserId = c.UserId,
            RealStateAgentId = c.RealStateAgentId,
            Status = c.Status,
            LastMessageAt = c.LastMessageAt,
            UnreadMessagesCount = c.Messages.Count(m => !m.IsRead)
        });

        return new RetrieveAllChatsResponse { Chats = chatDtos };
    }
}
