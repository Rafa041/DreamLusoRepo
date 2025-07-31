using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;


namespace DreamLuso.Application.CQ.Contracts.Commands.CreateContract;

public class CreateContractCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateContractCommand, Result<CreateContractResponse, Success, Error>>
{
    public async Task<Result<CreateContractResponse, Success, Error>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var existingClient = await unitOfWork.ClientRepository.RetrieveAsync(request.ClientId);

        if (existingClient == null)
            return Error.ClientNotFound;

        var contract = new Domain.Model.Contract
        {
            PropertyId = request.PropertyId,
            ClientId = request.ClientId,
            RealEstateAgentId = request.RealEstateAgentId,
            StartDate = request.StartTime,
            EndDate = request.EndTime,
            Value = request.Value,
            TermsAndConditions = request.TermsAndConditions,
            AdditionalFees = request.AdditionalFees,
            PaymentFrequency = request.PaymentFrequency,
            RenewalOption = request.RenewalOption,
            TerminationClauses = request.TerminationClauses
        };

        await unitOfWork.ContractRepository.AddAsync(contract);
        await unitOfWork.CommitAsync();

        var response = new CreateContractResponse
        {
            ContractId = contract.Id,
            PropertyId = contract.PropertyId,
            ClientId = contract.ClientId,
                            RealEstateAgentId = contract.RealEstateAgentId,
            StartTime = contract.StartDate,
            EndTime = contract.EndDate,
            Value = contract.Value,
            AdditionalFees = contract.AdditionalFees,
            PaymentFrequency = contract.PaymentFrequency,
            RenewalOption = contract.RenewalOption,
            TermsAndConditions = contract.TermsAndConditions,
            TerminationClauses = contract.TerminationClauses
        };
        return response;
    }
}
