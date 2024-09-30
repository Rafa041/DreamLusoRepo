using FluentValidation;

namespace DreamLuso.Application.CQ.Comments.Commands.UpdateComment;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty().WithMessage("CommentId is required.");

        When(x => x.Message != null, () =>
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message cannot be empty.")
                .MaximumLength(500).WithMessage("Message cannot exceed 500 characters.");
        });

        When(x => x.Rating.HasValue, () =>
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        });
    }
}
