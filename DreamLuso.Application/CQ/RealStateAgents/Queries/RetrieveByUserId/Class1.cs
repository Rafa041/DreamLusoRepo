using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.RealStateAgents.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.RealStateAgents.Queries.RetrieveByUserId;

public class RetrieveRealStateAgentByUserIdQuery : IRequest<Result<RetrieveRealStateAgentResponse, Success, Error>>
{
    public Guid UserId { get; init; }
}

public class RetrieveRealStateAgentByUserIdQueryHandler : IRequestHandler<RetrieveRealStateAgentByUserIdQuery, Result<RetrieveRealStateAgentResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RetrieveRealStateAgentByUserIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RetrieveRealStateAgentResponse, Success, Error>> Handle(RetrieveRealStateAgentByUserIdQuery request, CancellationToken cancellationToken)
    {
        var realStateAgent = await _unitOfWork.RealStateAgentRepository.RetrieveByUserIdAsync(request.UserId, cancellationToken);

        if (realStateAgent == null)
            return Error.RealStateAgentNotFound;

        var response = new RetrieveRealStateAgentResponse
        {
            Id = realStateAgent.Id,
            FirstName = realStateAgent.User.Name.FirstName,
            LastName = realStateAgent.User.Name.LastName,
            Email = realStateAgent.User.Account.Email,
            Access = realStateAgent.User.Access.ToString(),
            ImageUrl = realStateAgent.User.ImageUrl,
            PhoneNumber = realStateAgent.User.PhoneNumber,
            DateOfBirth = realStateAgent.User.DateOfBirth,
            OfficeEmail = realStateAgent.OfficeEmail,
            TotalSales = realStateAgent.TotalSales,
            TotalListings = realStateAgent.TotalListings,
            Certifications = realStateAgent.Certifications,
            LanguagesSpoken = realStateAgent.LanguagesSpoken.Select(lang => lang.ToString()).ToList()
        };

        return response;
    }
}
