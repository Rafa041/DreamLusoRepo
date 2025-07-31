using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Categories.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.RealEstateAgents.Queries.RetrieveRealEstateAgent;

public class RetrieveRealEstateAgentQuery : IRequest<Result<RetrieveRealEstateAgentResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
public class RetrieveRealEstateAgentResponse
{
    public Guid Id { get; set; }
    public string? OfficeEmail { get; set; }
    public int TotalSales { get; set; }
    public int TotalListings { get; set; }
    public List<string>? Certifications { get; set; }
    public List<string>? LanguagesSpoken { get; set; }

    //User
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Access { get; set; }
    public string ImageUrl { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}
public class RetrieveRealEstateAgentQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveRealEstateAgentQuery, Result<RetrieveRealEstateAgentResponse, Success, Error>>
{
    public async Task<Result<RetrieveRealEstateAgentResponse, Success, Error>> Handle(RetrieveRealEstateAgentQuery request, CancellationToken cancellationToken)
    {


        var realEstateAgent = await unitOfWork.RealEstateAgentRepository.RetrieveAsync(request.Id);

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
            LanguagesSpoken = realEstateAgent.LanguagesSpoken.Select(lang => lang.ToString()).ToList(),

        };

        return response;

    }


}
