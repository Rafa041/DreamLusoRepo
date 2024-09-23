using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Addresses.Commands.UpdateAddress;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Addresses.Commands.CreateAddress;

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

}
