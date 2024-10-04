using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePropertyCommand, Result<CreatePropertyResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyResponse, Success, Error>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var realStateAgent = await unitOfWork.RealStateAgentRepository.GetByUserIdAsync(request.UserId);
        if (realStateAgent == null)
            return Error.RealStateAgentNotFound;

        var address = new Address
        {
            Id = new Guid(),
            Street = request.Street,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            AdditionalInfo = request.AdditionalInfo
        };
        var addressResult = await unitOfWork.AddressRepository.GetByFullAddressAsync(address, cancellationToken);
        if (addressResult != null)
            return Error.ExistingAddress;

        await unitOfWork.AddressRepository.AddAsync(address, cancellationToken);

        var newProperty = new Property
        {
            Id = new Guid(),
            Title = request.Title,
            Description = request.Description,
            AddressId = address.Id,  // ID do endereço
            RealStateAgentId = realStateAgent.Id,  // ID do agente imobiliário existente
            Type = request.Type,
            Size = request.Size,
            Bedrooms = request.Bedrooms,
            Bathrooms = request.Bathrooms,
            Price = request.Price,
            Amenities = request.Amenities,
            Status = request.Status,
            YearBuilt = request.YearBuilt,
            OwnerInformation = request.OwnerInformation,
            HeatingSystem = request.HeatingSystem,
            CoolingSystem = request.CoolingSystem,
            DateListed = DateTime.UtcNow,  // Data de listagem
            LastModifiedDate = DateTime.UtcNow  // Última modificação
        };

        if (request.Images != null && request.Images.Any())
            foreach (var image in request.Images)
            {
                var fileName = await unitOfWork.FileStorageService.SaveFileAsync(image, cancellationToken);
                newProperty.Images.Add(new PropertyImages(newProperty.Id, fileName, request.IsMainImage));
            }

        await unitOfWork.PropertyRepository.AddAsync(newProperty, cancellationToken);
        await unitOfWork.CommitAsync();

        var response = new CreatePropertyResponse
        {
            Id = newProperty.Id,
            Title = newProperty.Title,
            Description = newProperty.Description,
            Price = newProperty.Price,
            Status = newProperty.Status,
            UserId = realStateAgent.Id
        };
        return response;
    }
}
