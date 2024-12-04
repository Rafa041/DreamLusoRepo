using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommand : IRequest<Result<UpdatePropertyResponse, Success, Error>>
{
    public Guid Id { get; init; }

    // Address
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }
    public string AdditionalInfo { get; init; }

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
    public List<string> ImageUrls { get; set; } = new List<string>();
}
public class UpdatePropertyResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public PropertyStatus Status { get; set; }
    public Guid UserId { get; set; }
}
public class UpdatePropertyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePropertyCommand, Result<UpdatePropertyResponse, Success, Error>>
{


    public async Task<Result<UpdatePropertyResponse, Success, Error>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.Id, cancellationToken);
            if (property == null)
                return Error.PropertyNotFound;

            var realStateAgent = await unitOfWork.RealStateAgentRepository.RetrieveByUserIdAsync(request.UserId, cancellationToken);
            if (realStateAgent == null)
                return Error.RealStateAgentNotFound;

            // Update address
            var address = new Address
            {
                Id = property.AddressId,
                Street = request.Street,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Country = request.Country,
                AdditionalInfo = request.AdditionalInfo
            };

            await unitOfWork.AddressRepository.UpdateAsync(address, cancellationToken);

            // Update property details
            property.Title = request.Title;
            property.Description = request.Description;
            property.Type = request.Type;
            property.Size = request.Size;
            property.Bedrooms = request.Bedrooms;
            property.Bathrooms = request.Bathrooms;
            property.Price = request.Price;
            property.Amenities = string.IsNullOrEmpty(request.Amenities) ? "None" : request.Amenities;
            property.Status = request.Status;
            property.YearBuilt = request.YearBuilt;
            property.LastModifiedDate = DateTime.UtcNow;

            // Handle images
            if (request.Images != null && request.Images.Any())
            {
                property.Images.Clear();
                foreach (var imageFile in request.Images)
                {
                    var fileName = await unitOfWork.FileStorageService.SaveFileAsync(imageFile, cancellationToken);
                    property.Images.Add(new Domain.Model.PropertyImages(property.Id, fileName));
                }
            }

            await unitOfWork.PropertyRepository.UpdateAsync(property, cancellationToken);
            await unitOfWork.CommitAsync();

            var response = new UpdatePropertyResponse
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                Status = property.Status,
                UserId = realStateAgent.Id
            };

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Error.UpdateFailed;
        }
    }
}