using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Addresses.Queries.Retrieve;

public class RetrieveAddressQuery : IRequest<Result<RetrieveAddressResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
