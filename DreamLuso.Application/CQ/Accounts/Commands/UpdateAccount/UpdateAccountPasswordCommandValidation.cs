using FluentValidation;

namespace DreamLuso.Application.CQ.Accounts.Commands.UpdateAccount;

public class UpdateAccountPasswordCommandValidation : AbstractValidator<UpdateAccountPasswordCommand>
{
    public UpdateAccountPasswordCommandValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Old password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters long.");
    }
}
