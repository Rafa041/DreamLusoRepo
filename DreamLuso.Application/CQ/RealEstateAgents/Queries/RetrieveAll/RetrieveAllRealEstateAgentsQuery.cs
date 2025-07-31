using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.RealEstateAgents.Queries.RetrieveRealEstateAgent;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.RealEstateAgents.Queries.RetrieveAll;

public class RetrieveAllRealEstateAgentsQuery : IRequest<Result<RetrieveAllRealEstateAgentsResponse, Success, Error>>
{
}
public class RetrieveAllRealEstateAgentsResponse
{
    public IEnumerable<RetrieveRealEstateAgentResponse> RealEstateAgents { get; set; }
}

public class RetrieveAllRealEstateAgentsHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllRealEstateAgentsQuery, Result<RetrieveAllRealEstateAgentsResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllRealEstateAgentsResponse, Success, Error>> Handle(RetrieveAllRealEstateAgentsQuery request, CancellationToken cancellationToken)
    {
        var realEstateAgents = await unitOfWork.RealEstateAgentRepository.RetrieveAllAsync(cancellationToken);

        if (realEstateAgents == null || !realEstateAgents.Any())
            return Error.RealStateAgentNotFound;

        var realEstateAgentsResponses = realEstateAgents.Select(realEstateAgent => new RetrieveRealEstateAgentResponse
        {
                         Id = realEstateAgent.Id,
             OfficeEmail = realEstateAgent.OfficeEmail,
             TotalSales = realEstateAgent.TotalSales,
             TotalListings = realEstateAgent.TotalListings,
             Certifications = realEstateAgent.Certifications,
             LanguagesSpoken = realEstateAgent.LanguagesSpoken.Select(lang => lang.ToString()).ToList(),
             FirstName = realEstateAgent.User.Name.FirstName,
             LastName = realEstateAgent.User.Name.LastName,
             Email = realEstateAgent.User.Account.Email,
             Access = realEstateAgent.User.Access.ToString(),
             ImageUrl = realEstateAgent.User.ImageUrl,
             PhoneNumber = realEstateAgent.User.PhoneNumber,
             DateOfBirth = realEstateAgent.User.DateOfBirth
        }).ToList();
        var response = new RetrieveAllRealEstateAgentsResponse
        {
            RealEstateAgents = realEstateAgentsResponses
        };

        return response;
    }
}
