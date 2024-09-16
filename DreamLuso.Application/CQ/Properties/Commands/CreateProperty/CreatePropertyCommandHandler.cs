using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePropertyCommand, Result<CreatePropertyResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyResponse, Success, Error>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        // 1. Verificação do Agente Imobiliário (RealStateAgent)
        var realStateAgent = await unitOfWork.RealStateAgentRepository.RetrieveAsync(request.RealStateAgentId, cancellationToken);
        if (realStateAgent == null)
        {
            return Error.NotFound; // Caso o agente não exista
        }

        // 2. Verificação do Endereço (Address)
        var address = await unitOfWork.AddressRepository.RetrieveAsync(request.AddressId, cancellationToken);
        if (address == null)
        {
            // Caso o endereço não exista, cria um novo
            address = new Address
            {
                Street = request.Street,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Country = request.Country,
                AdditionalInfo = request.AdditionalInfo
            };
            await unitOfWork.AddressRepository.AddAsync(address, cancellationToken);
        }

        // 3. Criação da Propriedade (Property)
        var newProperty = new Property
        {
            Title = request.Title,
            Description = request.Description,
            AddressId = address.Id,  // ID do endereço (existente ou recém-criado)
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

        // Adicionar as imagens se houver
        if (request.Images != null && request.Images.Any())
        {
            newProperty.Images = request.Images;
        }

        // 4. Adiciona a propriedade ao repositório
        await unitOfWork.PropertyRepository.AddAsync(newProperty, cancellationToken);

        // 5. Commit no UnitOfWork
        await unitOfWork.CommitAsync();

        // 6. Preparar a resposta
        var response = new CreatePropertyResponse
        {
            Id = newProperty.Id,
            Title = newProperty.Title,
            Description = newProperty.Description,
            Price = newProperty.Price,
            Status = newProperty.Status,
            RealStateAgentId = realStateAgent.Id
        };

        // 7. Retornar sucesso com a resposta
        return response;
    }
}
