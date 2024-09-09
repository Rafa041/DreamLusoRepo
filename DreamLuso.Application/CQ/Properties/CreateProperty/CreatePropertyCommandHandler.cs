using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.CreateProperty;

public class CreatePropertyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePropertyCommand, Result<CreatePropertyResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyResponse, Success, Error>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(
            Guid.NewGuid(),
            request.Street,
            request.City,
            request.State,
            request.PostalCode,
            request.Country,
            request.AdditionalInfo
        );

        // Verifica se a propriedade já existe
        var existingProperty = await unitOfWork.AddressRepository.GetByFullAddressAsync(request.Title,
            request.City,
            request.State,
            request.PostalCode,
            request.Country);
        if (existingProperty != null)
            return Error.ExistingProperty;

        // Cria uma nova instância de Property
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

        // Adiciona o endereço ao repositório de endereços
        await unitOfWork.AddressRepository.AddAsync(newAddress, cancellationToken);

        // Adiciona a propriedade ao repositório
        await unitOfWork.PropertyRepository.AddAsync(newProperty, cancellationToken);

        // Commit
        await unitOfWork.CommitAsync();

        // Retorna sucesso com a resposta
        var response = new CreatePropertyResponse
        {
            Id = newProperty.Id,
            Title = newProperty.Title,
            DateListed = newProperty.DateListed,
            LastModifiedDate = newProperty.LastModifiedDate
        };

        return response;
    }
}