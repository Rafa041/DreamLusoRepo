namespace DreamLuso.Application.CQ.Users.Commands.CreateUser;

public class CreateUserResponse
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}
