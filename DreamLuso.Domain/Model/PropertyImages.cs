using DreamLuso.Domain.Interface;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
namespace DreamLuso.Domain.Model;

public class PropertyImages : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public string FileName { get; set; }
    [NotMapped]
    public IFormFile Upload { get; set; }
    private PropertyImages()
    {
        Id = Guid.NewGuid();
    }
    public PropertyImages(Guid propertyId, string fileName)
    {
        PropertyId = propertyId;
        FileName = fileName;
    }
}
