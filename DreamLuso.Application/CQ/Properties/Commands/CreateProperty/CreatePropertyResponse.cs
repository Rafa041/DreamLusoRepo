using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyResponse
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required string Amenities { get; set; }
    public required PropertyStatus Status { get; set; }
    public required DateTime DateListed { get; set; }
    public required DateTime LastModifiedDate { get; set; }

    // Propriedades do Address
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public required string AdditionalInfo { get; set; }
}
