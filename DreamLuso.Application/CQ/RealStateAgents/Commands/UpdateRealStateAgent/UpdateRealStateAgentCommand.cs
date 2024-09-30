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

namespace DreamLuso.Application.CQ.RealStateAgents.Commands.UpdateRealStateAgent;

public class UpdateRealStateAgentCommand : IRequest<Result<UpdateRealStateAgentResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public required string OfficeEmail { get; init; }
    public int TotalSales { get; init; }
    public int TotalListings { get; init; }
    public List<string> Certifications { get; init; }
    public List<string> LanguagesSpoken { get; init; } = new();
}
public class UpdateRealStateAgentResponse { }


public class UpdateRealStateAgentCommandHadler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRealStateAgentCommand, Result<UpdateRealStateAgentResponse, Success, Error>>
{
    public async Task<Result<UpdateRealStateAgentResponse, Success, Error>> Handle(UpdateRealStateAgentCommand request, CancellationToken cancellationToken)
    {
        var existingRealSateAgent = await unitOfWork.RealStateAgentRepository.RetrieveAsync(request.Id);

        if (existingRealSateAgent == null)
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

        existingRealSateAgent.OfficeEmail = request.OfficeEmail;
        existingRealSateAgent.TotalSales = request.TotalSales;
        existingRealSateAgent.TotalListings = request.TotalListings;
        existingRealSateAgent.Certifications = request.Certifications;
        existingRealSateAgent.LanguagesSpoken = languagesSpoken;  


        await unitOfWork.RealStateAgentRepository.UpdateAsync(existingRealSateAgent);
        await unitOfWork.CommitAsync();
        return Success.Ok;
    }
        
}