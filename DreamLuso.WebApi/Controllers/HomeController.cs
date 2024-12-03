using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Message.Commands.CreateMessage;
using DreamLuso.Application.CQ.Message.Commands.MarkMessageRead;
using DreamLuso.Application.CQ.Message.Queries.GetChatMessage;
using DreamLuso.Application.CQ.Message.Queries.GetUnreadMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly ISender _sender;

    public MessageController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateMessageResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateMessage([FromBody] CreateMessageCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpGet("Chat/{chatId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<MessageResponse>), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> GetChatMessages([FromRoute] Guid chatId)
    {
        var query = new GetChatMessagesQuery { ChatId = chatId };
        var result = await _sender.Send(query);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        var messages = result.Value.Messages.Select(m => new MessageResponse
        {
            Id = m.Id,
            ChatId = m.ChatId,
            SenderId = m.SenderId,
            SenderName = m.SenderName,
            SenderImageUrl = m.SenderImageUrl,
            Content = m.Content,
            SentAt = m.SentAt,
            IsRead = m.IsRead,
            Type = m.Type
        });

        return Ok(messages);
    }

    [HttpGet("Unread/{userId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<MessageResponse>), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> GetUnreadMessages([FromRoute] Guid userId)
    {
        var query = new GetUnreadMessagesQuery { UserId = userId };
        var result = await _sender.Send(query);
        return result.IsSuccess
            ? Ok(result.Value.Messages)
            : NotFound(result.Error);
    }

    [HttpPatch("{id:guid}/Read")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> MarkMessageAsRead([FromRoute] Guid id)
    {
        var command = new MarkMessageAsReadCommand { MessageId = id };
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok(true)
            : NotFound(result.Error);
    }
}