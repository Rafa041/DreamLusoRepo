using DreamLuso.Application.CQ.Accounts.Commands.UpdateAccount;
using DreamLuso.Application.CQ.Accounts.Queries.Login;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }
        return BadRequest(result.Error);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember([FromBody] UpdateAccountPasswordCommand command)
    {
        var updatedAccountPassword = await _sender.Send(command);

        return updatedAccountPassword != null ? Ok(updatedAccountPassword.IsSuccess) : NotFound("Member not found.");
    }
}
