using FluentValidation;

namespace DreamLuso.Application.CQ.Notifications.Commands.Create;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {

        // Validate SenderId (check if it is not an empty Guid)
        RuleFor(command => command.SenderId)
            .NotEqual(Guid.Empty).WithMessage("The sender Id cannot be an empty Guid.");

        // Validate RecipientId (check if it is not an empty Guid)
        RuleFor(command => command.RecipientId)
            .NotEqual(Guid.Empty).WithMessage("The recipient Id cannot be an empty Guid.");

        // Validate Message (check if it is not empty and has a max length)
        RuleFor(command => command.Message)
            .NotEmpty().WithMessage("The message cannot be empty.")
            .MaximumLength(500).WithMessage("The message cannot exceed 500 characters.");

        // Validate Notification Type (check if it is a valid enum value)
        RuleFor(command => command.Type)
            .IsInEnum().WithMessage("The notification type is invalid.");

        // Validate Notification Priority (check if it is a valid enum value)
        RuleFor(command => command.Priority)
            .IsInEnum().WithMessage("The notification priority is invalid.");
    }
}