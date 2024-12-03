using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.PropertyVisits.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

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