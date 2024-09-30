using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Clients.Commands.UpdateClient;

public class UpdateClientResponse
{
    public Guid UserId { get; set; }
    public User UserClient { get; set; }
    public bool IsActive { get; set; }
}
