using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommand : IRequest<Result<CreatePropertyResponse, Success, Error>>
{


    //Address
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string AdditionalInfo { get; set; }  // Informações adicionais como número de apartamento

    //Real State Agent
    public required Guid RealStateAgentId { get; init; }
    public Guid UserId { get; set; }
    public string OfficeEmail { get; set; }
    public int TotalSales { get; set; }
    public int TotalListings { get; set; }
    public string Certifications { get; set; }
    public List<Languages> LanguagesSpoken { get; set; }

    //Property
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required Guid AddressId { get; init; }
    public required PropertyType Type { get; init; }
    public required double Size { get; init; }
    public required int Bedrooms { get; init; }
    public required int Bathrooms { get; init; }
    public required decimal Price { get; init; }
    public string? Amenities { get; init; }
    public required PropertyStatus Status { get; init; }
    public List<PropertyImages>? Images { get; init; }  // Apenas IDs das imagens
    public required DateTime YearBuilt { get; init; }
    public string? OwnerInformation { get; init; }
    public string? HeatingSystem { get; init; }
    public string? CoolingSystem { get; init; }
}

