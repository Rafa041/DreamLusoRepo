using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Message.Queries.RetrieveChat;

public class RetrieveChatQuery : IRequest<Result<RetrieveChatResponse, Success, Error>>
{
    public required Guid Id { get; init; }
}

public class RetrieveChatResponse
{
    public Guid Id { get; init; }
    public Guid PropertyId { get; init; }
    public Guid UserId { get; init; }
    public Guid RealEstateAgentId { get; init; }
    public ChatStatus Status { get; init; }
    public List<MessageDto> Messages { get; init; }
}

public class RetrieveChatQueryHandler : IRequestHandler<RetrieveChatQuery, Result<RetrieveChatResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RetrieveChatQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RetrieveChatResponse, Success, Error>> Handle(RetrieveChatQuery request, CancellationToken cancellationToken)
    {
        var chat = await _unitOfWork.ChatRepository.RetrieveAsync(request.Id, cancellationToken);
        if (chat == null) return Error.NotFound;

        var response = new RetrieveChatResponse
        {
            Id = chat.Id,
            PropertyId = chat.PropertyId,
            UserId = chat.UserId,
            RealEstateAgentId = chat.RealEstateAgentId,
            Status = chat.Status,
            Messages = chat.Messages.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                SentAt = m.SentAt,
                IsRead = m.IsRead,
                Type = m.Type
            }).ToList()
        };

        return response;
    }
}
public class MessageDto
{
    public Guid Id { get; init; }
    public Guid ChatId { get; init; }
    public Guid SenderId { get; init; }
    public string SenderName { get; init; }
    public string Content { get; init; }
    public DateTime SentAt { get; init; }
    public bool IsRead { get; init; }
    public MessageType Type { get; init; }
    public string SenderImageUrl { get; init; }
}