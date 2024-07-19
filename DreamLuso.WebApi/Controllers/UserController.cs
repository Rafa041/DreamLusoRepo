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
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(command, cancellationToken));


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await _sender.Send(query);

        if (result is not null)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }
}
