using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Categories.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Categories.Queries.RetrieveAllCategories;

public class RetrieveAllCategoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllCategoriesQuery, Result<RetrieveAllCategoriesResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllCategoriesResponse, Success, Error>> Handle(RetrieveAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.CategoryRepository.RetrieveAllAsync();

        if (categories == null || !categories.Any())
            return Error.NotFound;

        var categoriesResponses = categories.Select(category => new RetrieveCategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        }).ToList();

        var response = new RetrieveAllCategoriesResponse
        {
            Categories = categoriesResponses
        };

        return response;
    }
}
