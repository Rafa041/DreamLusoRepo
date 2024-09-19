using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Addresses.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;


namespace DreamLuso.Application.CQ.Addresses.Queries.RetrieveAllAddress;

public class RetrieveAllAddressQuery : IRequest<Result<RetrieveAllAddressResponse, Success, Error>>
{
}
public class RetrieveAllAddressQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllAddressQuery, Result<RetrieveAllAddressResponse, Success, Error>>
{

    public async Task<Result<RetrieveAllAddressResponse, Success, Error>> Handle(RetrieveAllAddressQuery request, CancellationToken cancellationToken)
    {
        var addresses = await unitOfWork.AddressRepository.RetrieveAllAsync();

        if (addresses == null || !addresses.Any())
            return Error.NotFound;

        var addressResponses = addresses.Select(address => new RetrieveAddressResponse
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country,
            AdditionalInfo = address.AdditionalInfo
        }).ToList();

        var response = new RetrieveAllAddressResponse
        {
            AddressResponses = addressResponses
        };

        return response;
    }
}

