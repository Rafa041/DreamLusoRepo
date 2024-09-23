using DreamLuso.Application.CQ.Addresses.Queries.Retrieve;

namespace DreamLuso.Application.CQ.Addresses.Queries.RetrieveAllAddress;

public class RetrieveAllAddressResponse
{
    public IEnumerable<RetrieveAddressResponse> AddressResponses { get; set; }
}