using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;


public class RealStateAgent
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public List<Property> Properties { get; set; }
    public string OfficeEmail { get; set; }
    public int TotalSales { get; set; }
    public int TotalListings { get; set; }
    public string Certifications { get; set; }
    public List<Languages> LanguagesSpoken { get; set; }

    public RealStateAgent()
    {
        Properties = new List<Property>();
        LanguagesSpoken = new List<Languages>();
    }

    public RealStateAgent(User user, string officeEmail, int totalSales, int totalListings, string certifications, List<Languages> languagesSpoken)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        UserId = user.Id;
        OfficeEmail = officeEmail;
        TotalSales = totalSales;
        TotalListings = totalListings;
        Certifications = certifications;
        LanguagesSpoken = languagesSpoken ?? new List<Languages>();
        Properties = new List<Property>();
    }
}

public enum Languages
{
    English,
    Spanish,
    French,
    German,
    Italian,
    Portuguese
}