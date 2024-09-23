using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Result<CreateCategoryResponse, Success, Error>>
{
    public string Name { get; init; }
}
