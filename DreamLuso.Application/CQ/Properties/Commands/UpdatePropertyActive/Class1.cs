using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Properties.Commands.UpdatePropertyActive;
public class UpdatePropertyIsActiveCommand : IRequest<Result<UpdatePropertyIsActiveResponse, Success, Error>>
{
    public Guid Id { get; init; }
    public bool IsActive { get; init; }
}
public class UpdatePropertyIsActiveResponse
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
}
public class UpdatePropertyIsActiveCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePropertyIsActiveCommand, Result<UpdatePropertyIsActiveResponse, Success, Error>>
{


    public async Task<Result<UpdatePropertyIsActiveResponse, Success, Error>> Handle(UpdatePropertyIsActiveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.Id, cancellationToken);
            if (property == null)
                return Error.PropertyNotFound;

            property.IsActive = request.IsActive;
            property.LastModifiedDate = DateTime.UtcNow;

            await unitOfWork.PropertyRepository.UpdateAsync(property, cancellationToken);
            await unitOfWork.CommitAsync();

            var response = new UpdatePropertyIsActiveResponse
            {
                Id = property.Id,
                IsActive = property.IsActive
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