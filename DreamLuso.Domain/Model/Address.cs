using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Common;
namespace DreamLuso.Domain.Model;

public class Address : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string AdditionalInfo { get; set; }  // Informações adicionais como número de apartamento

    public Address() { }
    public Address(Guid id, string street, string city, string state, string postalCode, string country, string additionalInfo)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
        AdditionalInfo = additionalInfo;
    }
}