using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using FluentValidation;
using MediatR;


namespace DreamLuso.Application.CQ.RealStateAgents.Commands.CreateRealStateAgent;

public class CreateRealStateAgentCommand : IRequest<Result<CreateRealStateAgentResponse, Success, Error>>
{
    public required Guid UserId { get; init; }
    public required string OfficeEmail { get; init; }
    public int TotalSales { get; init; }
    public int TotalListings { get; init; }
    public List<string> Certifications { get; init; }
    public List<string> LanguagesSpoken { get; init; } = new();
}
public class CreateRealStateAgentResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string OfficeEmail { get; init; }
    public int TotalSales { get; init; }
    public int TotalListings { get; init; }
    public List<string> Certifications { get; init; }
    public List<string> LanguagesSpoken { get; init; }
}

public class CreateRealStateAgentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRealStateAgentCommand, Result<CreateRealStateAgentResponse, Success, Error>>
{
    public async Task<Result<CreateRealStateAgentResponse, Success, Error>> Handle(CreateRealStateAgentCommand request, CancellationToken cancellationToken)
    {

        var languagesSpoken = new List<Languages>();
        foreach (var language in request.LanguagesSpoken)
        {
            if (Enum.TryParse<Languages>(language, out var parsedLanguage))
            {
                languagesSpoken.Add(parsedLanguage);
            }
            else
            {
                return Error.NotFound;
            }
        }

        var realStateAgent = new RealStateAgent
        {
            UserId = request.UserId,
            OfficeEmail = request.OfficeEmail,
            TotalSales = request.TotalSales,
            TotalListings = request.TotalListings,
            Certifications = request.Certifications,
            LanguagesSpoken = languagesSpoken
        };

        await unitOfWork.RealStateAgentRepository.AddAsync(realStateAgent, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var updateAccess = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);
        if (updateAccess == null)
        {
            return Error.NotFound;
        }

        updateAccess.Access = Access.RealStateAgent;

        await unitOfWork.UserRepository.UpdateAsync(updateAccess, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateRealStateAgentResponse
        {
            Id = realStateAgent.Id,
            UserId = realStateAgent.UserId,
            OfficeEmail = realStateAgent.OfficeEmail,
            TotalSales = realStateAgent.TotalSales,
            TotalListings = realStateAgent.TotalListings,
            Certifications = realStateAgent.Certifications,
            LanguagesSpoken = realStateAgent.LanguagesSpoken.Select(lang => lang.ToString()).ToList()
        };

        return response;

    }
}


public class CreateRealStateAgentCommandValidator : AbstractValidator<CreateRealStateAgentCommand>
{
    public CreateRealStateAgentCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.OfficeEmail)
            .NotEmpty().WithMessage("OfficeEmail is required.")
            .EmailAddress().WithMessage("OfficeEmail must be a valid email address.");

        RuleFor(x => x.TotalSales)
            .GreaterThanOrEqualTo(0).WithMessage("TotalSales must be 0 or greater.");

        RuleFor(x => x.TotalListings)
            .GreaterThanOrEqualTo(0).WithMessage("TotalListings must be 0 or greater.");

        RuleFor(x => x.Certifications)
            .NotNull().WithMessage("Certifications cannot be null.")
            .Must(certifications => certifications.All(c => !string.IsNullOrWhiteSpace(c)))
            .WithMessage("Certifications cannot contain empty values.");

        RuleFor(x => x.LanguagesSpoken)
            .NotNull().WithMessage("LanguagesSpoken cannot be null.")
            .Must(languages => languages.All(lang => Enum.TryParse<Languages>(lang, out _)))
            .WithMessage("All LanguagesSpoken must be valid.");
    }
}
