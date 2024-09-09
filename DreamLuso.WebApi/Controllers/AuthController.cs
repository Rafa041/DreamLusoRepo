using DreamLuso.Application.CQ.Accounts;
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
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
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
}
