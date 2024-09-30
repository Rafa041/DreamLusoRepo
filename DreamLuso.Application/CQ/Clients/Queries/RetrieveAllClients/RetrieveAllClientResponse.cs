using DreamLuso.Application.CQ.Clients.Queries.Retrieve;

namespace DreamLuso.Application.CQ.Clients.Queries.RetrieveAllClients;

public class RetrieveAllClientResponse
{
    public IEnumerable<RetrieveClientResponse> Clients { get; set; }
}
