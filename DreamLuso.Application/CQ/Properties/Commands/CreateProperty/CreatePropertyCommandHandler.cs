using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePropertyCommand, Result<CreatePropertyResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyResponse, Success, Error>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var realStateAgent = await unitOfWork.RealStateAgentRepository.RetrieveByUserIdAsync(request.UserId, cancellationToken);
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
                DateListed = DateTime.UtcNow,  // Data de listagem
                LastModifiedDate = DateTime.UtcNow, // Última modificação
                Images = new List<Domain.Model.PropertyImages>(), // Inicializando a lista de imagens
                

            };
            await unitOfWork.PropertyRepository.AddAsync(newProperty, cancellationToken);
            // Verificar e salvar arquivos de imagem
            if (request.Images != null && request.Images.Any())
            {
                foreach (var imageFile in request.Images)
                {
                    // Salvar arquivo e obter o caminho ou nome do arquivo salvo
                    var fileName = await unitOfWork.FileStorageService.SaveFileAsync(imageFile, cancellationToken);
                    // Adicionar imagem à propriedade
                    newProperty.Images.Add(new Domain.Model.PropertyImages(newProperty.Id, fileName));
                }
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Error.PropertyNotFound;
        }
    }
}
