using FluentValidation;

namespace DreamLuso.Application.CQ.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidation : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Address ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Address ID cannot be an empty GUID.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.")
            .MaximumLength(50).WithMessage("State must not exceed 50 characters.");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal Code is required.")
            .Matches(@"^\d{5}-\d{3}$").WithMessage("Postal Code must be in the format 12345-678.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");

        RuleFor(x => x.AdditionalInfo)
            .MaximumLength(200).WithMessage("Additional Info must not exceed 200 characters.");
    }
}