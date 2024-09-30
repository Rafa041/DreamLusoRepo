using FluentValidation;

namespace DreamLuso.Application.CQ.Contracts.Commands.UpdateContract;

public class UpdateContractCommandValidator : AbstractValidator<UpdateContractCommand>
{
    public UpdateContractCommandValidator()
    {
        RuleFor(x => x.ContractId)
            .NotEmpty().WithMessage("ContractId is required.");

        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("PropertyId is required.");

        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("ClientId is required.");

        RuleFor(x => x.RealStateAgentId)
            .NotEmpty().WithMessage("RealStateAgentId is required.");

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime).WithMessage("StartTime must be less than EndTime.");

        RuleFor(x => x.EndTime)
            .GreaterThan(DateTime.UtcNow).WithMessage("EndTime must be a future date.");

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0).WithMessage("Value must be a non-negative number.");

        RuleFor(x => x.AdditionalFees)
            .GreaterThanOrEqualTo(0).WithMessage("AdditionalFees must be a non-negative number.");

        RuleFor(x => x.TermsAndConditions)
            .NotEmpty().WithMessage("Terms and Conditions are required.")
            .MaximumLength(1000).WithMessage("Terms and Conditions cannot exceed 1000 characters.");

        RuleFor(x => x.PaymentFrequency)
            .NotEmpty().WithMessage("Payment Frequency is required.");

        RuleFor(x => x.TerminationClauses)
            .NotEmpty().WithMessage("Termination Clauses are required.");
    }
}