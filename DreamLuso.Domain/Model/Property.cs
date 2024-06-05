using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Common;
using System.Security.Principal;
namespace DreamLuso.Domain.Model;

public class Property : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AddressId { get; set; }
    public Address Address { get; set; }
    public PropertyType Type { get; set; }
    public double Size { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal Price { get; set; }
    public string Amenities { get; set; }
    public PropertyStatus Status { get; set; }
    public List<PropertyImages> Images { get; set; }
    public DateTime DateListed { get; set; }
    public DateTime LastModifiedDate { get; set; }

    // Propriedades sugeridas
    // - Ano de construção da propriedade
    public DateTime YearBuilt { get; set; }
    // - Informações sobre o proprietário da propriedade
    public string OwnerInformation { get; set; }
    // - Detalhes sobre o sistema de aquecimento da propriedade
    public string HeatingSystem { get; set; }
    // - Detalhes sobre o sistema de refrigeração da propriedade
    public string CoolingSystem { get; set; }

    public Property(
    Guid id,
    string title,
    string description,
    Address address,
    Guid addressId,
    PropertyType type,
    double size,
    int bedrooms,
    int bathrooms,
    decimal price,
    string amenities,
    PropertyStatus status,
    List<PropertyImages> images,
    DateTime dateListed,
    DateTime lastModifiedDate,
    DateTime yearBuilt,
    string ownerInformation,
    string heatingSystem,
    string coolingSystem)
    {
        Id = id;
        Title = title;
        Description = description;
        Address = address;
        AddressId = addressId;
        Type = type;
        Size = size;
        Bedrooms = bedrooms;
        Bathrooms = bathrooms;
        Price = price;
        Amenities = amenities;
        Status = status;
        Images = images;
        DateListed = dateListed;
        LastModifiedDate = lastModifiedDate;
        YearBuilt = yearBuilt;
        OwnerInformation = ownerInformation;
        HeatingSystem = heatingSystem;
        CoolingSystem = coolingSystem;

    }
}


public enum PropertyType
{
    House,
    Apartment,
    Condo,
    Townhouse,
    Land,
    Commercial,
    Other
}
public enum PropertyStatus
{
    Available,
    Sold,
    Pending,
    Rented,
    UnderContract,
    Unavailable,
    Other
}
