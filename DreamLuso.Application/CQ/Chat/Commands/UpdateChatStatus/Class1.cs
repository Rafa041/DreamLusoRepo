using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Chat.Commands.UpdateChatStatus;

public class UpdateChatStatusCommand : IRequest<Result<UpdateChatStatusResponse, Success, Error>>
{
    public required Guid ChatId { get; init; }
    public required ChatStatus Status { get; init; }
}

public class UpdateChatStatusResponse
{
    public Guid Id { get; init; }
    public ChatStatus Status { get; init; }
    public DateTime UpdatedAt { get; init; }
}

public class UpdateChatStatusCommandHandler : IRequestHandler<UpdateChatStatusCommand, Result<UpdateChatStatusResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateChatStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateChatStatusResponse, Success, Error>> Handle(UpdateChatStatusCommand request, CancellationToken cancellationToken)
    {
        var chat = await _unitOfWork.ChatRepository.UpdateStatusAsync(request.ChatId, request.Status, cancellationToken);

        if (chat == null)
            return Error.NotFound;

        var response = new UpdateChatStatusResponse
        {
            Id = chat.Id,
            Status = chat.Status,
            UpdatedAt = DateTime.UtcNow
        };

        return response;
    }
}

public class UpdateChatStatusCommandValidator : AbstractValidator<UpdateChatStatusCommand>
{
    public UpdateChatStatusCommandValidator()
    {
        RuleFor(x => x.ChatId)
            .NotEmpty().WithMessage("ChatId is required.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid chat status.");
    }
}