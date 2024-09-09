using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommand : IRequest<Result<CreatePropertyResponse, Success, Error>>
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required PropertyType Type { get; init; }
    public required double Size { get; init; }
    public required int Bedrooms { get; init; }
    public required int Bathrooms { get; init; }
    public required decimal Price { get; init; }
    public required string Amenities { get; init; }
    public required PropertyStatus Status { get; init; }
    public required List<PropertyImages> Images { get; init; }
    public required DateTime YearBuilt { get; init; }
    public required string OwnerInformation { get; init; }
    public required string HeatingSystem { get; init; }
    public required string CoolingSystem { get; init; }

    // Propriedades do Address
public required Address Address { get; init; }
}