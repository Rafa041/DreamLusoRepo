using FluentValidation;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommandValidation : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidation()
    {
    //    //Property Title validation
    //    RuleFor(x => x.Title)
    //        .NotEmpty().WithMessage("The property title is required.")
    //        .MaximumLength(100).WithMessage("The property title must not exceed 100 characters.");

    //    // Property Description validation
    //    RuleFor(x => x.Description)
    //        .NotEmpty().WithMessage("The property description is required.")
    //        .MaximumLength(500).WithMessage("The property description must not exceed 500 characters.");

    //    // Property Type validation
    //    RuleFor(x => x.Type)
    //        .IsInEnum().WithMessage("Invalid property type.");

    //    // Property Size validation
    //    RuleFor(x => x.Size)
    //        .GreaterThan(0).WithMessage("The property size must be greater than 0.");

    //    // Bedrooms validation
    //    RuleFor(x => x.Bedrooms)
    //        .GreaterThanOrEqualTo(0).WithMessage("The number of bedrooms must be 0 or greater.");

    //    // Bathrooms validation
    //    RuleFor(x => x.Bathrooms)
    //        .GreaterThanOrEqualTo(0).WithMessage("The number of bathrooms must be 0 or greater.");

    //    // Price validation
    //    RuleFor(x => x.Price)
    //        .GreaterThan(0).WithMessage("The property price must be greater than 0.");

    //    // Property Status validation
    //    RuleFor(x => x.Status)
    //        .IsInEnum().WithMessage("Invalid property status.");

    //    // Year Built validation
    //    RuleFor(x => x.YearBuilt)
    //        .LessThanOrEqualTo(DateTime.Now).WithMessage("The year built cannot be in the future.");

    //    // Owner Information validation
    //    RuleFor(x => x.OwnerInformation)
    //        .NotEmpty().WithMessage("Owner information is required.")
    //        .MaximumLength(200).WithMessage("Owner information must not exceed 200 characters.");

    //    // Heating System validation
    //    RuleFor(x => x.HeatingSystem)
    //        .NotEmpty().WithMessage("The heating system is required.")
    //        .MaximumLength(100).WithMessage("The heating system must not exceed 100 characters.");

    //    // Cooling System validation
    //    RuleFor(x => x.CoolingSystem)
    //        .NotEmpty().WithMessage("The cooling system is required.")
    //        .MaximumLength(100).WithMessage("The cooling system must not exceed 100 characters.");

        //// Address validations
        //RuleFor(x => x.Street)
        //    .NotEmpty().WithMessage("The street is required.")
        //    .MaximumLength(150).WithMessage("The street must not exceed 150 characters.");

        //RuleFor(x => x.City)
        //    .NotEmpty().WithMessage("The city is required.")
        //    .MaximumLength(100).WithMessage("The city must not exceed 100 characters.");

        //RuleFor(x => x.State)
        //    .NotEmpty().WithMessage("The state is required.")
        //    .MaximumLength(100).WithMessage("The state must not exceed 100 characters.");

        //RuleFor(x => x.PostalCode)
        //    .NotEmpty().WithMessage("The postal code is required.")
        //    .MaximumLength(20).WithMessage("The postal code must not exceed 20 characters.");

        //RuleFor(x => x.Country)
        //    .NotEmpty().WithMessage("The country is required.")
        //    .MaximumLength(100).WithMessage("The country must not exceed 100 characters.");

        //RuleFor(x => x.AdditionalInfo)
        //    .MaximumLength(200).WithMessage("The additional information must not exceed 200 characters.");
    }
}