using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : Controller
{
    private readonly ISender _sender;

    public PropertyController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> CreateUser([FromBody] CreatePropertyCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }
}


