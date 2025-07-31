using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;


namespace DreamLuso.Application.CQ.Message.Commands.CreateMessage;

public class CreateMessageCommand : IRequest<Result<CreateMessageResponse, Success, Error>>
{
    public required Guid ChatId { get; init; }
    public required Guid SenderId { get; init; }
    public required string Content { get; init; }
    public required MessageType Type { get; init; }
}

public class CreateMessageResponse
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public MessageType Type { get; set; }
}
public class CreateMessageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateMessageCommand, Result<CreateMessageResponse, Success, Error>>
{
    public async Task<Result<CreateMessageResponse, Success, Error>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var chat = await unitOfWork.ChatRepository.RetrieveAsync(request.ChatId, cancellationToken);
            if (chat == null)
                return Error.ChatNotFound;

            var agent = await unitOfWork.RealEstateAgentRepository.RetrieveAsync(chat.RealEstateAgentId);

            var message = new Domain.Model.Message
            {
                Id = Guid.NewGuid(),
                ChatId = request.ChatId,
                SenderId = request.SenderId,
                Content = request.Content,
                Type = request.Type,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            // Create notification with correct sender and recipient IDs
            var notification = new Notification(
                Guid.NewGuid(),
                request.SenderId, 
                chat.UserId,
                $"New message received: {request.Content.Substring(0, Math.Min(50, request.Content.Length))}...",
                NotificationType.Message,
                NotificationPriority.Medium
            );

            await unitOfWork.MessageRepository.AddAsync(message, cancellationToken);
            await unitOfWork.NotificationRepository.AddAsync(notification, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return new CreateMessageResponse
            {
                Id = message.Id,
                ChatId = message.ChatId,
                SenderId = message.SenderId,
                Content = message.Content,
                SentAt = message.SentAt,
                Type = message.Type
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Error.EmptyDatabase;
        }
    }
}