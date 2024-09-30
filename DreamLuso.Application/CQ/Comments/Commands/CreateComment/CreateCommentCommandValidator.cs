using FluentValidation;

namespace DreamLuso.Application.CQ.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("PropertyId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(500).WithMessage("Message cannot exceed 500 characters.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.DateTimePosted)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DatePosted cannot be in the future.");

    }
}
