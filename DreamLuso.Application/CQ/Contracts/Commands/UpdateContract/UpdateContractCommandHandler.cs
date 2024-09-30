using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Contracts.Commands.UpdateContract;

public class UpdateContractCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateContractCommand, Result<UpdateContractResponse, Success, Error>>
{
    public async Task<Result<UpdateContractResponse, Success, Error>> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
    {
        var existingContract = await unitOfWork.ContractRepository.RetrieveAsync(request.ContractId);

        if (existingContract == null)
            return Error.ContractNotFound;

        existingContract.PropertyId = request.PropertyId;
        existingContract.ClientId = request.ClientId;
        existingContract.RealStateAgentId = request.RealStateAgentId;
        existingContract.StartDate = request.StartTime;
        existingContract.EndDate = request.EndTime;
        existingContract.Value = request.Value;
        existingContract.TermsAndConditions = request.TermsAndConditions;
        existingContract.AdditionalFees = request.AdditionalFees;
        existingContract.PaymentFrequency = request.PaymentFrequency;
        existingContract.RenewalOption = request.RenewalOption;
        existingContract.TerminationClauses = request.TerminationClauses;

        await unitOfWork.ContractRepository.UpdateAsync(existingContract);
        await unitOfWork.CommitAsync();

        var response = new UpdateContractResponse
        {
            ContractId = existingContract.Id,
            PropertyId = existingContract.PropertyId,
            ClientId = existingContract.ClientId,
            RealStateAgentId = existingContract.RealStateAgentId,
            StartTime = existingContract.StartDate,
            EndTime = existingContract.EndDate,
            Value = existingContract.Value,
            AdditionalFees = existingContract.AdditionalFees,
            PaymentFrequency = existingContract.PaymentFrequency,
            RenewalOption = existingContract.RenewalOption,
            TermsAndConditions = existingContract.TermsAndConditions,
            TerminationClauses = existingContract.TerminationClauses
        };

        return response;
    }
}
