using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Notifications.Commands.Create;

public class CreateNotificationCommand : IRequest<Result<CreateNotificationResponse, Success, Error>>
{
    public Guid Id { get; set; } = new Guid();
    public Guid SenderId { get; init; }
    public Guid RecipientId { get; init; }
    public string Message { get; init; }
    public NotificationType Type { get; init; }
    public NotificationPriority Priority { get; init; }
}
