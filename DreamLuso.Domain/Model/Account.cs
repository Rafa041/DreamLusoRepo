using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class Account : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    private Account()
    {
        Id = Guid.NewGuid();
    }

    public Account(string username, string password) : this()
    {
        Email = username;
        Password = password;
    }
}