using DreamLuso.Application.CQ.PropertyImages.Queries.Retrieve;

namespace DreamLuso.Application.CQ.Properties.Queries.Retrieve;

public class RetrievePropertyResponse
{
    public Guid Id { get; set; }
    // Address
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string AdditionalInfo { get; set; } // Informações adicionais como número de apartamento

    // Real State Agent
    public Guid RealEstateAgentId { get; set; }
    public Guid userId { get; set; }

    // Property
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public double Size { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal Price { get; set; }
    public string? Amenities { get; set; }
    public string Status { get; set; }
    public DateTime YearBuilt { get; set; }
    // Images
    public List<PropertyImageResponse> Images { get; set; }  
    public bool IsActive { get; set; }
}

