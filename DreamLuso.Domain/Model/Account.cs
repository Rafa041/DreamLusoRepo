using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class Account : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Account()
    {
        Id = Guid.NewGuid();
    }

    public Account(string email, byte[] passwordHash, byte[] passwordSalt) : this()
    {
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
}