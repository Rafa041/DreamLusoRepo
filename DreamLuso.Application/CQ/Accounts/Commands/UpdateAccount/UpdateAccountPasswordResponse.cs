namespace DreamLuso.Application.CQ.Accounts.Commands.UpdateAccount;

public class UpdateAccountPasswordResponse
{
    public required Guid UserId { get; set; }
    public required string Message { get; set; }
}
