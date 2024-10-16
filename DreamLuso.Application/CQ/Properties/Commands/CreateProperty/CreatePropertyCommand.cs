using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommand : IRequest<Result<CreatePropertyResponse, Success, Error>>
{
    // Address
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string AdditionalInfo { get; set; } // Informações adicionais como número de apartamento

    // Real State Agent
    public Guid UserId { get; init; }

    // Property
    public string Title { get; init; }
    public string Description { get; init; }
    public PropertyType Type { get; init; }
    public double Size { get; init; }
    public int Bedrooms { get; init; }
    public int Bathrooms { get; init; }
    public decimal Price { get; init; }
    public string? Amenities { get; init; }
    public PropertyStatus Status { get; init; }
    public DateTime YearBuilt { get; init; }
    public string? OwnerInformation { get; init; }
    public string? HeatingSystem { get; init; }
    public string? CoolingSystem { get; init; }

    // Images
    public List<IFormFile> Images { get; set; }
    public bool IsMainImage { get; init; }

}

