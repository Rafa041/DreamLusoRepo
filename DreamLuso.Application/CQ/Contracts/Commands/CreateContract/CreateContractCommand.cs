using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Contracts.Commands.CreateContract;

public class CreateContractCommand : IRequest<Result<CreateContractResponse, Success, Error>>
{
    public required Guid PropertyId { get; init; }
    public required Guid ClientId { get; init; }
    public required Guid RealStateAgentId { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required double Value { get; init; }
    public required string TermsAndConditions { get; init; }
    public required double AdditionalFees { get; init; }
    public required string PaymentFrequency { get; init; }
    public required bool RenewalOption { get; init; }
    public required string TerminationClauses { get; init; }
}
