using FluentValidation;

namespace DreamLuso.Application.CQ.Clients.Commands.CreateClient;

public class CreateClientCommandValidation : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidation()
    {
        RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("User ID is required.");
    }
}