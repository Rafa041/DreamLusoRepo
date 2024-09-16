using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required PropertyStatus Status { get; set; }
    public required Guid RealStateAgentId { get; set; }
}
