using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Addresses.Commands.CreateAddress;
using DreamLuso.Application.CQ.Addresses.Commands.UpdateAddress;
using DreamLuso.Application.CQ.Addresses.Queries.RetrieveAllAddress;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController(ISender sender) : Controller
{
    //private readonly ISender _sender;

    //public AddressController(ISender sender)
    //{
    //    _sender = sender;
    //}
    
    [HttpGet("RetrieveAll")]
    public async Task<IActionResult> RetrieveAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveAllAddressQuery(), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrieveUserQuery { Id = id };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateAddressCommand), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand command)
    {
        var updatedMember = await sender.Send(command);

        return updatedMember != null ? Ok(updatedMember.IsSuccess) : NotFound("Member not found.");
    }
}