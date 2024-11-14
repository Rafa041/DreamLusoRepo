using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.PropertyVisits.Commands.Create;

public class CreatePropertyVisitCommand : IRequest<Result<CreatePropertyVisitResponse, Success, Error>>
{
    public Guid PropertyId { get; init; }
    public Guid UserId { get; init; }
    public Guid RealStateAgentId { get; init; }
    public DateOnly VisitDate { get; init; }
    public TimeSlot TimeSlot { get; init; }
}
public class CreatePropertyVisitResponse
{
    public Guid VisitId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public Guid RealStateAgentId { get; set; }
    public DateOnly VisitDate { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreatePropertyVisitCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePropertyVisitCommand, Result<CreatePropertyVisitResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyVisitResponse, Success, Error>> Handle(CreatePropertyVisitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request == null)
                return Error.PropertyVisitNotFound;

            var realStateAgent = await unitOfWork.RealStateAgentRepository.RetrieveAsync(request.RealStateAgentId, cancellationToken);
            if (realStateAgent == null)
                return Error.RealStateAgentNotFound;

            var isSlotAvailable = await unitOfWork.PropertyVisitRepository.IsTimeSlotAvailable(
                request.PropertyId,
                request.VisitDate,
                request.TimeSlot,
                cancellationToken);

            if (!isSlotAvailable)
                return Error.SlotNotAvailable;

            var propertyVisit = new PropertyVisit
            {
                Id = Guid.NewGuid(),
                PropertyId = request.PropertyId,
                UserId = request.UserId,
                RealStateAgentId = request.RealStateAgentId,
                VisitStatus = VisitStatus.Pending,
                VisitDate = request.VisitDate,
                TimeSlot = request.TimeSlot,
                CreatedAt = DateTime.UtcNow
            };

            // Create notification for the real estate agent
            var notification = new Notification(
                Guid.NewGuid(),
                request.UserId,
                realStateAgent.UserId,
                $"New visit scheduled for property {request.PropertyId} on {request.VisitDate} at {request.TimeSlot}",
                NotificationType.Visit,
                NotificationPriority.High
            );

            await unitOfWork.PropertyVisitRepository.AddAsync(propertyVisit, cancellationToken);
            await unitOfWork.NotificationRepository.AddAsync(notification, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            var response = new CreatePropertyVisitResponse
            {
                VisitId = propertyVisit.Id,
                PropertyId = propertyVisit.PropertyId,
                UserId = propertyVisit.UserId,
                RealStateAgentId = propertyVisit.RealStateAgentId,
                VisitDate = propertyVisit.VisitDate,
                TimeSlot = propertyVisit.TimeSlot,
                CreatedAt = propertyVisit.CreatedAt
            };

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Error.PropertyVisitInvalid;
        }
    }
}

