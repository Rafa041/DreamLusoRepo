using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.RealEstateAgents.Queries.RetrieveRealEstateAgent;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.RealEstateAgents.Queries.RetrieveByUserId;

public class RetrieveRealEstateAgentByUserIdQuery : IRequest<Result<RetrieveRealEstateAgentResponse, Success, Error>>
{
    public Guid UserId { get; init; }
}

public class RetrieveRealEstateAgentByUserIdQueryHandler : IRequestHandler<RetrieveRealEstateAgentByUserIdQuery, Result<RetrieveRealEstateAgentResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RetrieveRealEstateAgentByUserIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RetrieveRealEstateAgentResponse, Success, Error>> Handle(RetrieveRealEstateAgentByUserIdQuery request, CancellationToken cancellationToken)
    {
        var realEstateAgent = await _unitOfWork.RealEstateAgentRepository.RetrieveByUserIdAsync(request.UserId, cancellationToken);

        if (realEstateAgent == null)
            return Error.RealStateAgentNotFound;

        var response = new RetrieveRealEstateAgentResponse
        {
            Id = realEstateAgent.Id,
            FirstName = realEstateAgent.User.Name.FirstName,
            LastName = realEstateAgent.User.Name.LastName,
            Email = realEstateAgent.User.Account.Email,
            Access = realEstateAgent.User.Access.ToString(),
            ImageUrl = realEstateAgent.User.ImageUrl,
            PhoneNumber = realEstateAgent.User.PhoneNumber,
            DateOfBirth = realEstateAgent.User.DateOfBirth,
            OfficeEmail = realEstateAgent.OfficeEmail,
            TotalSales = realEstateAgent.TotalSales,
            TotalListings = realEstateAgent.TotalListings,
            Certifications = realEstateAgent.Certifications,
            LanguagesSpoken = realEstateAgent.LanguagesSpoken.Select(lang => lang.ToString()).ToList()
        };

        return response;
    }
}
