using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Notifications.Commands.Create;

public class CreateNotificationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateNotificationCommand, Result<CreateNotificationResponse, Success, Error>>
{
    public async Task<Result<CreateNotificationResponse, Success, Error>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            return Error.None;

        var notification = new Notification(
            request.Id,
            request.SenderId,
            request.RecipientId,
            request.Message,
            request.Type,
            request.Priority
        );

        await unitOfWork.NotificationRepository.AddAsync(notification, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        CreateNotificationResponse response = new ();
        response.NotificationId = request.Id;

        return response;
    }
}