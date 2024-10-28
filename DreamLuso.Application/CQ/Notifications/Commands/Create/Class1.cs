using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Notifications.Commands.Create;

public class CreateNotificationCommand : IRequest<Result<CreateNotificationResponse, Success, Error>>
{
    public Guid SenderId { get; set; }
    public Guid RecipientId { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public NotificationPriority Priority { get; set; }
    public Guid? ReferenceId { get; set; }
    public string? ReferenceType { get; set; }
}
public class CreateNotificationResponse { }

public class CreateNotificationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateNotificationCommand, Result<CreateNotificationResponse, Success, Error>>
{
    public async Task<Result<CreateNotificationResponse, Success, Error>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Domain.Model.Notifications(request.SenderId, request.RecipientId, request.Message, request.Type, request.Priority);

        if(notification.Priority is NotificationPriority.High)
        {
            await notificationStrategy.StorePersistentNotification(notification);
        }
    }
}