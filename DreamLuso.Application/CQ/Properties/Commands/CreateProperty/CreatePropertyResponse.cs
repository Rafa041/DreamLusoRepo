using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public PropertyStatus Status { get; set; }
    public Guid UserId { get; set; }
}
