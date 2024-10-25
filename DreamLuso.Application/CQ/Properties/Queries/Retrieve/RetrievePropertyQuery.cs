using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.PropertyImages.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

using Microsoft.AspNetCore.Http;

namespace DreamLuso.Application.CQ.Properties.Queries.Retrieve;

public class RetrievePropertyQuery : IRequest<Result<RetrievePropertyResponse, Success, Error>>
{
    public required Guid Id { get; init; }
}
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
    public Guid RealStateAgentId { get; set; }
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
public class RetrievePropertyQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrievePropertyQuery, Result<RetrievePropertyResponse, Success, Error>>
{

    public async Task<Result<RetrievePropertyResponse, Success, Error>> Handle(RetrievePropertyQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
        var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.Id);

        if (property == null)
            return Error.PropertyNotFound;

        var response = new RetrievePropertyResponse
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Street = property.Address.Street,
            City = property.Address.City,
            State = property.Address.State,
            PostalCode = property.Address.PostalCode,
            Country = property.Address.Country,
            AdditionalInfo = property.Address.AdditionalInfo,
            RealStateAgentId = property.RealStateAgentId,
            Type = property.Type.ToString(),
            Size = property.Size,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Price = property.Price,
            Amenities = property.Amenities,
            Status = property.Status.ToString(),
            Images = property.Images?.Select(img => new PropertyImageResponse
            {
                Id = img.Id,
                FileName = img.FileName,
                ImagePath = $"{baseUrl}/Uploads/{img.FileName}"

            }).ToList() ?? new List<PropertyImageResponse>(),
            IsActive = property.IsActive,
            YearBuilt = property.YearBuilt
        };

        return response;
    }

}

