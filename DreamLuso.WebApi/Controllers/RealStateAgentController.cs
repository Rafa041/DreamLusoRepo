using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.RealStateAgents.Commands.CreateRealStateAgent;
using DreamLuso.Application.CQ.RealStateAgents.Commands.UpdateRealStateAgent;
using DreamLuso.Application.CQ.RealStateAgents.Queries.Retrieve;
using DreamLuso.Application.CQ.RealStateAgents.Queries.RetrieveAll;
using DreamLuso.Application.CQ.Users.Commands.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RealStateAgentController : Controller
{
    private readonly ISender _sender;

    public RealStateAgentController(ISender sender)
    {
        _sender = sender;
    }
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateRealStateAgentResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateRealStateAgent([FromBody] CreateRealStateAgentCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.IsSuccess);

        return BadRequest(result.Error);
    }
    [HttpGet("RetrieveAll")]
    public async Task<IActionResult> RetrieveAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new RetrieveAllRealStateAgentsQuery(), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrieveRealStateAgentQuery { Id = id };
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember([FromBody] UpdateRealStateAgentCommand command)
    {
        var updatedRealStateAgent = await _sender.Send(command);

        return updatedRealStateAgent != null ? Ok(updatedRealStateAgent.IsSuccess) : NotFound("RealStateAgent not found.");
    }


}