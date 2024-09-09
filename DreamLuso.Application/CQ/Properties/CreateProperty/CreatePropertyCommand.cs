using DreamLuso.Application.Common.Responses;

using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.CreateProperty;

public class CreatePropertyCommand : IRequest<Result<CreatePropertyResponse, Success, Error>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public PropertyType Type { get; set; }
    public double Size { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal Price { get; set; }
    public string Amenities { get; set; }
    public PropertyStatus Status { get; set; }
    public List<PropertyImages> Images { get; set; }
    public DateTime YearBuilt { get; set; }
    public string OwnerInformation { get; set; }
    public string HeatingSystem { get; set; }
    public string CoolingSystem { get; set; }

    // Propriedades do Address
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string AdditionalInfo { get; set; }
}