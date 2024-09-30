using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.RealStateAgents.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.RealStateAgents.Queries.RetrieveAll;

public class RetrieveAllRealStateAgentsQuery : IRequest<Result<RetrieveAllRealStateAgentsResponse, Success, Error>>
{
}
public class RetrieveAllRealStateAgentsResponse
{
    public IEnumerable<RetrieveRealStateAgentResponse> RealStateAgents { get; set; }
}

public class RetrieveAllRealStateAgentsHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllRealStateAgentsQuery, Result<RetrieveAllRealStateAgentsResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllRealStateAgentsResponse, Success, Error>> Handle(RetrieveAllRealStateAgentsQuery request, CancellationToken cancellationToken)
    {
        var realStateAgents = await unitOfWork.RealStateAgentRepository.RetrieveAllAsync();

        if (realStateAgents == null)
            return Error.RealStateAgentNotFound;

        var realStateAgentResponse = realStateAgents.Select(realStateAgent => new RetrieveRealStateAgentResponse
        {
            Id = realStateAgent.Id,
            UserId = realStateAgent.UserId,
            OfficeEmail = realStateAgent.OfficeEmail,
            TotalSales = realStateAgent.TotalSales,
            TotalListings = realStateAgent.TotalListings,
            Certifications = realStateAgent.Certifications,
            LanguagesSpoken = realStateAgent.LanguagesSpoken.Select(lang => lang.ToString()).ToList()
        }).ToList();

        var response = new RetrieveAllRealStateAgentsResponse
        {
            RealStateAgents = realStateAgentResponse
        };
        return response;
    }
}