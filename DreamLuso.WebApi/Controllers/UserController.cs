using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using DreamLuso.Application.CQ.Users.Commands.UpdateUser;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DreamLuso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }
    [HttpPost("Register")]
    [ProducesResponseType(typeof(CreateUserResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("RetrieveAll")]
    public async Task<IActionResult> RetrieveAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new RetrieveAllUsersQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember([FromBody] UpdateUserCommand command)
    {
        var updatedMember = await _sender.Send(command);

        return updatedMember != null ? Ok(updatedMember.IsSuccess) : NotFound("Member not found.");
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrieveUserQuery { Id = id };
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }
}
