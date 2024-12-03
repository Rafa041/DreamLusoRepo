using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Message.Commands.MarkMessageRead;

public class MarkMessageAsReadCommand : IRequest<Result<MarkMessageAsReadResponse, Success, Error>>
{
    public required Guid MessageId { get; init; }
}

public class MarkMessageAsReadResponse
{
    public Guid Id { get; init; }
    public bool IsRead { get; init; }
}

public class MarkMessageAsReadCommandHandler : IRequestHandler<MarkMessageAsReadCommand, Result<MarkMessageAsReadResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public MarkMessageAsReadCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MarkMessageAsReadResponse, Success, Error>> Handle(MarkMessageAsReadCommand request, CancellationToken cancellationToken)
    {
        var message = await _unitOfWork.MessageRepository.MarkAsReadAsync(request.MessageId, cancellationToken);
        if (message == null) return Error.NotFound;

        var response = new MarkMessageAsReadResponse
        {
            Id = message.Id,
            IsRead = message.IsRead
        };

        return response;
    }
}
