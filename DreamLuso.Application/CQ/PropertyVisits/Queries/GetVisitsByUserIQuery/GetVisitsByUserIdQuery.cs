using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.PropertyVisits.Queries.GetVisitsByDateQuery;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;


namespace DreamLuso.Application.CQ.PropertyVisits.Queries.GetVisitsByUserIQuery;

public record GetVisitsByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<PropertyVisitResponse>, Success, Error>>;

public class GetVisitsByUserIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetVisitsByUserIdQuery, Result<IEnumerable<PropertyVisitResponse>, Success, Error>>
{

    public async Task<Result<IEnumerable<PropertyVisitResponse>, Success, Error>> Handle(GetVisitsByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var visits = await unitOfWork.PropertyVisitRepository.RetrieveByRealEstateAgentIdSingleAsync(request.UserId, cancellationToken);

            var visitResponses = visits.Select(v => new PropertyVisitResponse
            {
                Id = v.Id,
                PropertyId = v.PropertyId,
                TimeSlot = v.TimeSlot,
                VisitDate = v.VisitDate,
                Status = v.VisitStatus.ToString()
            });

            return visitResponses.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Error.FailedToRetrieveVisits;
        }
    }
}
