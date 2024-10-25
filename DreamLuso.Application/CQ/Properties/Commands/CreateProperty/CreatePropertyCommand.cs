using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommand : IRequest<Result<CreatePropertyResponse, Success, Error>>
{
    // Address
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }
    public string AdditionalInfo { get; init; } // Informações adicionais como número de apartamento

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
    public bool ForSale { get; set; }
    public bool ForRent { get; set; }
    // Images
    public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    //public List<PropertyImages> Images { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>(); // Guardar as URLs geradas

}

