using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Addresses.Queries.Retrieve;

public class RetrieveAddressQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAddressQuery, Result<RetrieveAddressResponse, Success, Error>>
{

    public async Task<Result<RetrieveAddressResponse, Success, Error>> Handle(RetrieveAddressQuery request, CancellationToken cancellationToken)
    {

        var address = await unitOfWork.AddressRepository.RetrieveAsync(request.Id);

        if (address == null)
            return Error.NotFound;

        var response = new RetrieveAddressResponse
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country,
            AdditionalInfo = address.AdditionalInfo
        };

        return response;
    }
}
