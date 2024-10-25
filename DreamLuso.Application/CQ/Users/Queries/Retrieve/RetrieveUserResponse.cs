using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Access { get; set; }
    public string ImageUrl { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}
