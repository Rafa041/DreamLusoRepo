using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using static DreamLuso.Application.CQ.Addresses.Commands.CreateAddress.CreateAddressCommandHandler;

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
public class CreateAddressCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateAddressCommand, Result<CreateAddressResponse, Success, Error>>
{

    public async Task<Result<CreateAddressResponse, Success, Error>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(
            Guid.NewGuid(),
            request.Street,
            request.City,
            request.State,
            request.PostalCode,
            request.Country,
            request.AdditionalInfo
        );
        // Verifica se a propriedade já existe
        var existingAddress = await unitOfWork.AddressRepository.GetByFullAddressAsync(newAddress,cancellationToken);
        if (existingAddress != null)
            return Error.ExistingProperty;

        await unitOfWork.AddressRepository.AddAsync(newAddress, cancellationToken);
        await unitOfWork.CommitAsync();

        return Success.Ok;
    }
    public class CreateAddressResponse
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string AdditionalInfo { get; set; }
    }

}
