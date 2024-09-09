using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Properties.CreateProperty;

public class CreatePropertyResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateListed { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public PropertyStatus Status { get; set; }
    public decimal Price { get; set; }
    // Address Information
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}
