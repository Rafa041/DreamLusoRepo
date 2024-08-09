using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using DreamLuso.Application.CQ.Users.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

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
            => Ok(await _sender.Send(new RetrieveAllUsersQuery(), cancellationToken));
}
