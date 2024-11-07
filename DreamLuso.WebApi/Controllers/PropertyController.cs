using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Application.CQ.Properties.Queries;
using DreamLuso.Application.CQ.Properties.Queries.GetTotalSales;
using DreamLuso.Application.CQ.PropertyVisits.Commands.Create;
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
[Route("api/[controller]")]
[ApiController]
public class PropertyVisitController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<PropertyVisitController> _logger;

    public PropertyVisitController(ISender sender, ILogger<PropertyVisitController> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreatePropertyVisitResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateVisit([FromForm] CreatePropertyVisitCommand command)
    {
        _logger.LogInformation("Received visit command: {@Command}", command);

        if (command == null)
        {
            _logger.LogWarning("Command is null");
            return BadRequest("Command cannot be null");
        }

        var result = await _sender.Send(command);
        _logger.LogInformation("Visit creation result: {@Result}", result);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}