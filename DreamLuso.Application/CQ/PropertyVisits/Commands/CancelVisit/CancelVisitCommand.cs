using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.PropertyVisits.Commands.Create;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.PropertyVisits.Commands.CancelVisit;

public class CancelVisitCommand : IRequest<Result<CancelVisitResponse, Success, Error>>
{
    public Guid VisitId { get; init; }

}
public class CancelVisitResponse
{
    public bool Result { get; set; }
}


public class CancelVisitCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelVisitCommand, Result<CancelVisitResponse, Success, Error>>
{


    public async Task<Result<CancelVisitResponse, Success, Error>> Handle(CancelVisitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Verificar se a visita existe
            var propertyVisit = await unitOfWork.PropertyVisitRepository.RetrieveAsync(request.VisitId, cancellationToken);
            if (propertyVisit == null)
                return Error.PropertyVisitNotFound;

            // 2. Alterar o status da visita para "Cancelado"
            propertyVisit.Cancel();

            // 3. Enviar notificação para o usuário relevante
            var userId = propertyVisit.UserId; // O usuário que agendou a visita
            var realEstateAgentId = propertyVisit.RealStateAgentId; // O corretor da visita

            // Determinar qual usuário deve ser notificado (o agente ou o usuário)
            var notificationRecipientId = propertyVisit.VisitStatus == VisitStatus.Canceled
                ? userId
                : realEstateAgentId;

            var notification = new Notification(
                Guid.NewGuid(),
                propertyVisit.UserId,
                notificationRecipientId,
                $"Visit scheduled for property {propertyVisit.PropertyId} on {propertyVisit.VisitDate} at {propertyVisit.TimeSlot} has been canceled.",
                NotificationType.Visit,
                NotificationPriority.High
            );

            // 4. Salvar as alterações e enviar a notificação
            await unitOfWork.PropertyVisitRepository.UpdateAsync(propertyVisit, cancellationToken);
            await unitOfWork.NotificationRepository.AddAsync(notification, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            // 5. Criar a resposta
            var response = new CancelVisitResponse
            {
                Result = true // Operação de cancelamento foi bem-sucedida
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