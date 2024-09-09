using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePropertyCommand, Result<CreatePropertyResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyResponse, Success, Error>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(
            Guid.NewGuid(),
            request.Address.Street,
            request.Address.City,
            request.Address.State,
            request.Address.PostalCode,
            request.Address.Country,
            request.Address.AdditionalInfo
        );

        // Verifica se a propriedade já existe
        var existingProperty = await unitOfWork.AddressRepository.GetByFullAddressAsync(newAddress, cancellationToken);
        if (existingProperty != null)
            return Error.ExistingProperty;


        var newProperty = new Property
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Address = newAddress, // Associa o novo endereço à propriedade
            AddressId = newAddress.Id,
            Type = request.Type,
            Size = request.Size,
            Bedrooms = request.Bedrooms,
            Bathrooms = request.Bathrooms,
            Price = request.Price,
            Amenities = request.Amenities,
            Status = request.Status,
            Images = request.Images,
            DateListed = DateTime.UtcNow,
            LastModifiedDate = DateTime.UtcNow,
            YearBuilt = request.YearBuilt,
            OwnerInformation = request.OwnerInformation,
            HeatingSystem = request.HeatingSystem,
            CoolingSystem = request.CoolingSystem
        };


        await unitOfWork.AddressRepository.AddAsync(newAddress, cancellationToken);


        await unitOfWork.PropertyRepository.AddAsync(newProperty, cancellationToken);


        await unitOfWork.CommitAsync();


        var response = new CreatePropertyResponse
        {
            Id = newProperty.Id,
            Title = newProperty.Title,
            Description = newProperty.Description,
            DateListed = newProperty.DateListed,
            LastModifiedDate = newProperty.LastModifiedDate,
            Status = newProperty.Status,
            Price = newProperty.Price,
            Amenities = newProperty.Amenities,

            Street = newAddress.Street,
            City = newAddress.City,
            State = newAddress.State,
            PostalCode = newAddress.PostalCode,
            Country = newAddress.Country,
            AdditionalInfo = newAddress.AdditionalInfo

        };

        return response;
    }
}