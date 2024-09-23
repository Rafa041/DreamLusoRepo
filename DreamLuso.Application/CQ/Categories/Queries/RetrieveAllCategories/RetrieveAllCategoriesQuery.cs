using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Categories.Queries.RetrieveAllCategories;

public class RetrieveAllCategoriesQuery : IRequest<Result<RetrieveAllCategoriesResponse, Success, Error>>
{
}
