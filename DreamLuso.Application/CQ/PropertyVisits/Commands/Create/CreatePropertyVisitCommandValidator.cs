using FluentValidation;

namespace DreamLuso.Application.CQ.PropertyVisits.Commands.Create;

public class CreatePropertyVisitCommandValidator : AbstractValidator<CreatePropertyVisitCommand>
{
    public CreatePropertyVisitCommandValidator()
    {
        //RuleFor(x => x.PropertyId)
        //    .NotEmpty()
        //    .WithMessage("Property ID is required");

        //RuleFor(x => x.UserId)
        //    .NotEmpty()
        //    .WithMessage("User ID is required");

        //RuleFor(x => x.RealStateAgentId)
        //    .NotEmpty()
        //    .WithMessage("Real State Agent ID is required");

        //RuleFor(x => x.TimeSlot)
        //    .IsInEnum()
        //    .WithMessage("Invalid time slot");
    }
}

