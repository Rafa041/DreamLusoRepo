using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using MediatR;

namespace DreamLuso.Application.CQ.Addresses.Commands.CreateAddress;

public class CreateAddressCommand : IRequest<Result<CreateAddressResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }
    public required string AdditionalInfo { get; init; }
}
