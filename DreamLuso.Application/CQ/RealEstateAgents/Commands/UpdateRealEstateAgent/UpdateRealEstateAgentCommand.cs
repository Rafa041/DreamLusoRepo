using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.UpdateUser;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.RealEstateAgents.Commands.UpdateRealEstateAgent;

public class UpdateRealEstateAgentCommand : IRequest<Result<UpdateRealEstateAgentResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public required string OfficeEmail { get; init; }
    public int TotalSales { get; init; }
    public int TotalListings { get; init; }
    public List<string> Certifications { get; init; }
    public List<string> LanguagesSpoken { get; init; } = new();
}
public class UpdateRealEstateAgentResponse { }


public class UpdateRealEstateAgentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRealEstateAgentCommand, Result<UpdateRealEstateAgentResponse, Success, Error>>
{
    public async Task<Result<UpdateRealEstateAgentResponse, Success, Error>> Handle(UpdateRealEstateAgentCommand request, CancellationToken cancellationToken)
    {
        var existingRealEstateAgent = await unitOfWork.RealEstateAgentRepository.RetrieveAsync(request.Id);

        if (existingRealEstateAgent == null)
            return Error.RealStateAgentNotFound;

        var languagesSpoken = new List<Languages>();
        foreach (var language in request.LanguagesSpoken)
        {
            if (Enum.TryParse<Languages>(language, true, out var parsedLanguage))  
            {
                languagesSpoken.Add(parsedLanguage);
            }
            else
            {
                
                return Error.InvalidLanguage;
            }
        }

        existingRealEstateAgent.OfficeEmail = request.OfficeEmail;
        existingRealEstateAgent.TotalSales = request.TotalSales;
        existingRealEstateAgent.TotalListings = request.TotalListings;
        existingRealEstateAgent.Certifications = request.Certifications;
        existingRealEstateAgent.LanguagesSpoken = languagesSpoken;  


        await unitOfWork.RealEstateAgentRepository.UpdateAsync(existingRealEstateAgent);
        await unitOfWork.CommitAsync();
        return Success.Ok;
    }
        
}