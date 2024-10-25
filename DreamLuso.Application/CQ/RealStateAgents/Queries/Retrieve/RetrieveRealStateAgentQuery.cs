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

namespace DreamLuso.Application.CQ.RealStateAgents.Queries.Retrieve;

public class RetrieveRealStateAgentQuery : IRequest<Result<RetrieveRealStateAgentResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
public class RetrieveRealStateAgentResponse
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
public class RetieveRealStateAgentQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveRealStateAgentQuery, Result<RetrieveRealStateAgentResponse, Success, Error>>
{
    public async Task<Result<RetrieveRealStateAgentResponse, Success, Error>> Handle(RetrieveRealStateAgentQuery request, CancellationToken cancellationToken)
    {


        var realStateAgent = await unitOfWork.RealStateAgentRepository.RetrieveAsync(request.Id);

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
            LanguagesSpoken = realStateAgent.LanguagesSpoken.Select(lang => lang.ToString()).ToList(),

        };

        return response;

    }


}
