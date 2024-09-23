using DreamLuso.Domain.Interface;
namespace DreamLuso.Domain.Model;

public class Category : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category() { }
    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
