namespace DreamLuso.Domain.Model;

public class Name
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Name()
    {
    }

    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
