using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.Properties.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly ISender _sender;

    public PropertyController(ISender sender)
    {
        _sender = sender;
    }

    // POST api/property/register
    [HttpPost("Register")]
    [ProducesResponseType(typeof(CreatePropertyResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> RegisterProperty([FromForm] CreatePropertyCommand command)
    {
        var result = await _sender.Send(command);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    // GET api/property/GetTotalSales
    [HttpGet("GetTotalSales")]
    public async Task<IActionResult> GetTotalSales()
    {
        var result = await _sender.Send(new GetTotalSalesQuery());
        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Error);
    }
}
