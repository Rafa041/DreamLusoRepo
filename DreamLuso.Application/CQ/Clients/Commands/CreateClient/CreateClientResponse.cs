using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Clients.Commands.CreateClient;

public class CreateClientResponse
{
    public Guid UserId { get; set; }
    public User UserClient { get; set; }
    public bool IsActive { get; set; }
};
