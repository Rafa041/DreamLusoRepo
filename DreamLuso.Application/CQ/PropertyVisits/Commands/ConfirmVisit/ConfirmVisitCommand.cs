using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.PropertyVisits.Commands.ConfirmVisit;

public class ConfirmVisitCommand : IRequest<Result<ConfirmVisitResponse, Success, Error>>
{
    public string ConfirmationToken { get; init; } // Token da visita para confirmação
}
public class ConfirmVisitResponse
{
    public bool Result { get; set; }
}
public class ConfirmVisitCommandHandler : IRequestHandler<ConfirmVisitCommand, Result<ConfirmVisitResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmVisitCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ConfirmVisitResponse, Success, Error>> Handle(ConfirmVisitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Verificar se a visita existe através do token de confirmação
            var propertyVisit = await _unitOfWork.PropertyVisitRepository.RetrieveByTokenAsync(request.ConfirmationToken);
            if (propertyVisit == null)
                return Error.PropertyVisitNotFound;

            // 2. Verificar se a visita já foi confirmada
            if (propertyVisit.VisitStatus == VisitStatus.Confirmed)
                return Error.VisitAlreadyConfirmed;

            // 3. Confirmar a visita
            propertyVisit.Confirm();

            // 4. Enviar notificação de confirmação para o usuário relevante
            var notificationRecipientId = propertyVisit.UserId; // Usuário que agendou a visita
            var notification = new Notification(
                Guid.NewGuid(),
                propertyVisit.RealEstateAgentId, // O corretor que confirmou a visita
                notificationRecipientId,
                $"Your visit for property {propertyVisit.PropertyId} on {propertyVisit.VisitDate} at {propertyVisit.TimeSlot} has been confirmed.",
                NotificationType.Visit,
                NotificationPriority.High
            );

            // 5. Salvar as alterações (confirmar a visita e adicionar a notificação)
            await _unitOfWork.PropertyVisitRepository.UpdateAsync(propertyVisit, cancellationToken);
            await _unitOfWork.NotificationRepository.AddAsync(notification, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            // 6. Criar a resposta
            var response = new ConfirmVisitResponse
            {
                Result = true // Visita foi confirmada com sucesso
            };

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Error.PropertyVisitInvalid;
        }
    }
}