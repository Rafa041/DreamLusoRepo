using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;


namespace DreamLuso.Application.CQ.PropertyVisits.Queries.RetrieveAvailableTimeSlotsQuery;

public class GetAvailableTimeSlotsQuery : IRequest<Result<GetAvailableTimeSlotsResponse, Success, Error>>
{
    public Guid PropertyId { get; set; }
    public string Date { get; set; }
}

public class GetAvailableTimeSlotsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAvailableTimeSlotsQuery, Result<GetAvailableTimeSlotsResponse, Success, Error>>
{

    public async Task<Result<GetAvailableTimeSlotsResponse, Success, Error>> Handle(
        GetAvailableTimeSlotsQuery request,
        CancellationToken cancellationToken)
    {
        var existingVisits = await unitOfWork.PropertyVisitRepository
            .GetVisitsByDateAndProperty(request.PropertyId, request.Date, cancellationToken);

        var bookedTimeSlots = existingVisits.Select(v => v.TimeSlot);
        var allTimeSlots = Enum.GetValues(typeof(TimeSlot)).Cast<TimeSlot>();

        var availableTimeSlots = allTimeSlots.Except(bookedTimeSlots)
            .Select(slot => slot.ToString())
            .ToList();

        return new GetAvailableTimeSlotsResponse
        {
            AvailableSlots = availableTimeSlots
        };
    }
}


public class GetAvailableTimeSlotsResponse
{
    public List<string> AvailableSlots { get; set; }
}
