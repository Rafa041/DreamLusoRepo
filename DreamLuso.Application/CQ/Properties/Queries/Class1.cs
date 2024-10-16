

using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Queries;
public class GetTotalSalesQuery : IRequest<Result<GetTotalSalesResponse, Success, Error>>
{
}
public class GetTotalSalesResponse
{
    public decimal TotalSales { get; set; }
}
public class GetTotalSalesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTotalSalesQuery, Result<GetTotalSalesResponse, Success, Error>>
{
    public async Task<Result<GetTotalSalesResponse, Success, Error>> Handle(GetTotalSalesQuery request, CancellationToken cancellationToken)
    {
        var currentDate = DateTime.UtcNow;
        var month = currentDate.Month;
        var year = currentDate.Year;

        var totalSales = await unitOfWork.PropertyRepository
            .GetTotalSalesForMonthAsync(month, year, cancellationToken);

        var response = new GetTotalSalesResponse
        {
            TotalSales = totalSales
        };

        return response;
    }
}