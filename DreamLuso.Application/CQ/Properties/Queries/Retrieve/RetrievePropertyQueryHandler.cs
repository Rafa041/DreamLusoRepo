using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.PropertyImages.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Http;

namespace DreamLuso.Application.CQ.Properties.Queries.Retrieve;

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
            RealEstateAgentId = property.RealEstateAgentId,
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

