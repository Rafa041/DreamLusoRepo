using DreamLuso.Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace DreamLuso.Application.Common.Behaviours;

internal class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ApplicationValidationException(failures);
        }

        return await next();
    }
}