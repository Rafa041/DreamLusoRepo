using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateAddressCommand, Result<UpdateAddressResponse, Success, Error>>
{
    public async Task<Result<UpdateAddressResponse, Success, Error>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var existingAddress = await unitOfWork.AddressRepository.RetrieveAsync(request.Id);

        if (existingAddress == null)
            return Error.NotFound;

        existingAddress.Street = request.Street;
        existingAddress.City = request.City;
        existingAddress.State = request.State;
        existingAddress.PostalCode = request.PostalCode;
        existingAddress.Country = request.Country;
        existingAddress.AdditionalInfo = request.AdditionalInfo;

        await unitOfWork.AddressRepository.UpdateAsync(existingAddress);
        await unitOfWork.CommitAsync();

        var response = new UpdateAddressResponse
        {
            Id = existingAddress.Id,
            Street = existingAddress.Street,
            City = existingAddress.City,
            State = existingAddress.State,
            PostalCode = existingAddress.PostalCode,
            Country = existingAddress.Country,
            AdditionalInfo = existingAddress.AdditionalInfo
        };

        return response;
    }
}
