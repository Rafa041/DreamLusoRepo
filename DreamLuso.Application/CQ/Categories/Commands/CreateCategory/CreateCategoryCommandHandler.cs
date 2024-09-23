using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryResponse, Success, Error>>
{
    public async Task<Result<CreateCategoryResponse, Success, Error>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await unitOfWork.CategoryRepository.GetByNameAsync(request.Name);
        if (existingCategory != null)
        {
            return Error.ExistingCategory;
        }

        var newCategory = new Category
        {
            Name = request.Name
        };

        await unitOfWork.CategoryRepository.AddAsync(newCategory, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateCategoryResponse
        {
            Name = newCategory.Name
        };

        return response;
    }
}