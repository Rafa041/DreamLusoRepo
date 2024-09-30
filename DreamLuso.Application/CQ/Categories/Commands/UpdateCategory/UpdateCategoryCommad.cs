using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Result<UpdateCategoryResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
