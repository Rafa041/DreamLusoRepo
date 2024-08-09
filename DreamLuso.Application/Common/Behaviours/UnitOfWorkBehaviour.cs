using Azure.Core;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;


namespace DreamLuso.Application.Common.Behaviours;

internal class UnitOfWorkBehaviour<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsCommand())
        {
            return await HandleCommand(next, cancellationToken);
        }
        else
        {
            return await HandleQuery(next);
        }
    }

    private async Task<TResponse> HandleCommand(RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await next();
            unitOfWork.DebugChanges();

            await unitOfWork.CommitTransactioAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTrasactionAsync(cancellationToken);
            throw;
        }
    }

    private async Task<TResponse> HandleQuery(RequestHandlerDelegate<TResponse> next)
    {
        return await next();
    }

    private static bool IsCommand()
    {
        return typeof(TRequest).Name.EndsWith("Command");
    }
}
