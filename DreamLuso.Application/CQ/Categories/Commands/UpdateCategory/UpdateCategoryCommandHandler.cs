using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result<UpdateCategoryResponse, Success, Error>>
{
    public async Task<Result<UpdateCategoryResponse, Success, Error>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.RetrieveAsync(request.Id);
        if (category == null)
        {
            return Error.CategoryNotFound;
        }

        var existingCategoryWithName = await unitOfWork.CategoryRepository.GetByNameAsync(request.Name);
        if (existingCategoryWithName != null && existingCategoryWithName.Id != request.Id)
        {
            return Error.CategoryNameAlreadyInUse; 
        }

        category.Name = request.Name;

        await unitOfWork.CategoryRepository.UpdateAsync(category);

        await unitOfWork.CommitAsync(cancellationToken);

        var response = new UpdateCategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };

        return response;
    }
}