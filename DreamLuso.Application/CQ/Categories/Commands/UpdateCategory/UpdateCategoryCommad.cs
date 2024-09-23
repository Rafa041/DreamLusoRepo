using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Result<UpdateCategoryResponse, Success, Error>>
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}
