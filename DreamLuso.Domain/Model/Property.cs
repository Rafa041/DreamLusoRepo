using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Common;
namespace DreamLuso.Domain.Model;

public class Property : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AddressId { get; set; }
    public Address Address { get; set; }
    public Guid RealStateAgentId { get; set; }
    public RealStateAgent RealStateAgent { get; set; }
    public PropertyType Type { get; set; }
    public double Size { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal Price { get; set; }
    public string Amenities { get; set; }
    public PropertyStatus Status { get; set; }
    public List<PropertyImages> Images { get; set; } = new List<PropertyImages>();  // Inicializado para evitar nulls

    public DateTime DateListed { get; set; }
    public DateTime LastModifiedDate { get; set; }

    // Propriedades adicionais
    public DateTime YearBuilt { get; set; }
    public bool IsActive { get; set; } = false;  // Soft delete flag
    public Property() { }

    // Construtor completo
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
        bool isActive)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id; 
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
        Images = images ?? new List<PropertyImages>();  // Garantia de que não seja null
        DateListed = dateListed;
        LastModifiedDate = lastModifiedDate;
        YearBuilt = yearBuilt;
        IsActive = isActive;
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
    Sale,          // Venda
    Rent,          // Para Arrendar
    Other
}