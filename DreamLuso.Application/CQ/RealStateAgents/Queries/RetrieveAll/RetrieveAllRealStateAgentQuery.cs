using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.RealStateAgents.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
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
        var realStateAgents = await unitOfWork.RealStateAgentRepository.RetrieveAllAsync(cancellationToken);

        if (realStateAgents == null || !realStateAgents.Any())
            return Error.RealStateAgentNotFound;

        var realStateAgentsResponses = realStateAgents.Select(realStateAgent => new RetrieveRealStateAgentResponse
        {
            Id = realStateAgent.Id,
            OfficeEmail = realStateAgent.OfficeEmail,
            TotalSales = realStateAgent.TotalSales,
            TotalListings = realStateAgent.TotalListings,
            Certifications = realStateAgent.Certifications,
            LanguagesSpoken = realStateAgent.LanguagesSpoken.Select(lang => lang.ToString()).ToList(),
            FirstName = realStateAgent.User.Name.FirstName,
            LastName = realStateAgent.User.Name.LastName,
            Email = realStateAgent.User.Account.Email,
            Access = realStateAgent.User.Access.ToString(),
            ImageUrl = realStateAgent.User.ImageUrl,
            PhoneNumber = realStateAgent.User.PhoneNumber,
            DateOfBirth = realStateAgent.User.DateOfBirth
        }).ToList();
        var response = new RetrieveAllRealStateAgentsResponse
        {
            RealStateAgents = realStateAgentsResponses
        };

        return response;
    }
}
