using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Notifications.Queries.GetUnreadNotificationsQuery;
public record GetUnreadNotificationsQuery(Guid UserId) : IRequest<Result<IEnumerable<NotificationResponse>, Success, Error>>;
public class GetUnreadNotificationsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUnreadNotificationsQuery, Result<IEnumerable<NotificationResponse>, Success, Error>>
{
    public async Task<Result<IEnumerable<NotificationResponse>, Success, Error>> Handle(
        GetUnreadNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications = await unitOfWork.NotificationRepository
            .GetUnreadNotificationsAsync(request.UserId, cancellationToken);

        var response = notifications.Select(n => new NotificationResponse
        {
            Id = n.Id,
            Message = n.Message,
            CreatedAt = n.CreateAt,
            Type = n.Type.ToString(),
            Priority = n.Priority.ToString(),
            Status = n.Status.ToString(),
            SenderId = n.SenderId,
            RecipientId = n.RecipientId,
            SenderName = n.SenderUser != null
                ? $"{n.SenderUser.Name.FirstName} {n.SenderUser.Name.LastName}"
                : "System",
            ExpirationDate = n.ExpirationDate
        }).ToList();

        return response;
    }
}

public class NotificationResponse
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Type { get; set; }
    public string Priority { get; set; }
    public string Status { get; set; }
    public Guid SenderId { get; set; }
    public Guid RecipientId { get; set; }
    public string SenderName { get; set; }
    public DateTime ExpirationDate { get; set; }
}