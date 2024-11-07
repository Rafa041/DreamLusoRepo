using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;


namespace DreamLuso.Application.CQ.Notifications.Commands.MarkNotificationAsReadCommand;

public record MarkNotificationAsReadCommand(Guid NotificationId)
    : IRequest<Result<bool, Success, Error>>;

public class MarkNotificationAsReadCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkNotificationAsReadCommand, Result<bool, Success, Error>>
{
    public async Task<Result<bool, Success, Error>> Handle(
        MarkNotificationAsReadCommand request,
        CancellationToken cancellationToken)
    {
        await unitOfWork.NotificationRepository.MarkAsReadAsync(request.NotificationId, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Success.Ok;
    }
}