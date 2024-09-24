using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace DreamLuso.Domain.Model;

public class User : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Name Name { get; set; }
    public Account Account { get; set; }
    public Access Access { get; set; }
    public string ImageUrl { get; set; }
    [NotMapped]
    public IFormFile Upload { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Property> FavoriteProperty { get; set; }
    public List<Property> Properties { get; set; }
    public User()
    {
        Id = Guid.NewGuid();
        FavoriteProperty = [];
    }
    public User(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt,
        Access access, string imageUrl, string phoneNumber, DateTime dateOfBirth)
    {
        Name = new(firstName, lastName);
        Account = new(email, passwordHash,passwordSalt);
        Access = access;
        ImageUrl = imageUrl;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }
}
public enum Access
{
    None,
    Guest,
    User,
    RealStateAgent,
    Admin,
    Blocked
}
