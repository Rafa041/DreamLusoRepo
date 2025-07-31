using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Queries.Retrieve;
using DreamLuso.Application.CQ.Properties.Queries.RetrieveAll;
using DreamLuso.Application.CQ.PropertyImages.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Properties.Queries.RetrieveAgentPropertiesQuery;

public class RetrieveAgentPropertiesQuery : IRequest<Result<RetrieveAllPropertiesResponse, Success, Error>>
{
    public Guid AgentId { get; set; }
}

public class RetrieveAgentPropertiesQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveAgentPropertiesQuery, Result<RetrieveAllPropertiesResponse, Success, Error>>
{

    public async Task<Result<RetrieveAllPropertiesResponse, Success, Error>> Handle(RetrieveAgentPropertiesQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
        var realStatAgent = await unitOfWork.RealEstateAgentRepository.RetrieveByUserIdAsync(request.AgentId, cancellationToken);
        if (realStatAgent == null)
            return Error.RealStateAgentNotFound;

        var properties = await unitOfWork.PropertyRepository.GetPropertiesByAgentIdAsync(realStatAgent.Id, cancellationToken);

        if (properties is null || !properties.Any())
            return Error.PropertyNotFound;

        var propertiesResponses = properties.Select(property => new RetrievePropertyResponse
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
        });

        var response = new RetrieveAllPropertiesResponse { Properties = propertiesResponses };
        return response;
    }
}