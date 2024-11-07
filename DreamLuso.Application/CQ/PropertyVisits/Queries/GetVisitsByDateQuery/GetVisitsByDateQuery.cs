using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.PropertyVisits.Queries.GetVisitsByDateQuery;
public record GetVisitsByDateQuery(Guid PropertyId, string Date) : IRequest<Result<IEnumerable<PropertyVisitResponse>, Success, Error>>;

public class GetVisitsByDateQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetVisitsByDateQuery, Result<IEnumerable<PropertyVisitResponse>, Success, Error>>
{


    public async Task<Result<IEnumerable<PropertyVisitResponse>, Success, Error>> Handle(GetVisitsByDateQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var visits = await unitOfWork.PropertyVisitRepository.GetVisitsByDateAndProperty(
                request.PropertyId,
                request.Date,
                cancellationToken);

            var visitResponses = visits.Select(v => new PropertyVisitResponse
            {
                Id = v.Id,
                PropertyId = v.PropertyId,
                TimeSlot = v.TimeSlot,
                VisitDate = v.VisitDate
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
public class PropertyVisitResponse
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public DateOnly VisitDate { get; set; }
    public TimeSlot TimeSlot { get; set; }
}