using DreamLuso.Application.Common.Responses;
using MediatR;


namespace DreamLuso.Application.CQ.Addresses.Queries.RetrieveAllAddress;

public class RetrieveAllAddressQuery : IRequest<Result<RetrieveAllAddressResponse, Success, Error>>
{
}

