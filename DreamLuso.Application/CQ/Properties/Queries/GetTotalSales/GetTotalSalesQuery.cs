using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Queries.GetTotalSales;
public class GetTotalSalesQuery : IRequest<Result<GetTotalSalesResponse, Success, Error>>
{
}
