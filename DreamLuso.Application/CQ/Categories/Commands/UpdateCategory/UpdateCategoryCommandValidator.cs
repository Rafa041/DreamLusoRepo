using FluentValidation;

namespace DreamLuso.Application.CQ.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Category Id is required.")
               .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Category Id must be a valid GUID.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
    }
}
