using DreamLuso.Application.CQ.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }
        return BadRequest(result.Error);
    }
}