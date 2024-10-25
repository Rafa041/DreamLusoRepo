using DreamLuso.Domain.Interface;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace DreamLuso.Domain.Model;

public class PropertyImages : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    [JsonIgnore]
    public Property Property { get; set; }
    public string FileName { get; set; }
    [NotMapped]
    [JsonIgnore]
    public IFormFile Upload { get; set; }
    public PropertyImages() { }
    public PropertyImages(Guid propertyId, string fileName)
    {
        PropertyId = propertyId;
        FileName = fileName;
    }
}
