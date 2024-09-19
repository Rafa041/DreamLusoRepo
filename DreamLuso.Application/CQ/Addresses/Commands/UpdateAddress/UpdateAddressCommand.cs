using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.UpdateUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommand : IRequest<Result<UpdateAddressResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }
    public required string AdditionalInfo { get; init; }
}
