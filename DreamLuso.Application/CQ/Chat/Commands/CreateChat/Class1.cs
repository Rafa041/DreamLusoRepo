using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Chat.Commands.CreateChat;

    public class CreateChatCommand : IRequest<Result<CreateChatResponse, Success, Error>>
    {
        public required Guid PropertyId { get; init; }
        public required Guid UserId { get; init; }
        public required Guid RealEstateAgentId { get; init; }
    }

    public class CreateChatResponse
    {
        public Guid Id { get; init; }
        public Guid PropertyId { get; init; }
        public Guid UserId { get; init; }
        public Guid RealEstateAgentId { get; init; }
        public ChatStatus Status { get; init; }
    }

    public class CreateChatCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateChatCommand, Result<CreateChatResponse, Success, Error>>
    {


        public async Task<Result<CreateChatResponse, Success, Error>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = new DreamLuso.Domain.Model.Chat
            {
                PropertyId = request.PropertyId,
                UserId = request.UserId,
                RealEstateAgentId = request.RealEstateAgentId,
                Status = ChatStatus.Active,
                LastMessageAt = DateTime.UtcNow
            };

            await unitOfWork.ChatRepository.AddAsync(chat, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            var response = new CreateChatResponse
            {
                Id = chat.Id,
                PropertyId = chat.PropertyId,
                UserId = chat.UserId,
                RealEstateAgentId = chat.RealEstateAgentId,
                Status = chat.Status
            };

            return response;
        }
    }

