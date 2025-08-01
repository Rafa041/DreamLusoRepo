﻿using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class RealEstateAgent : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string OfficeEmail { get; set; }
    public int TotalSales { get; set; }
    public int TotalListings { get; set; }
    public List<string> Certifications { get; set; }
    public List<Languages> LanguagesSpoken { get; set; }
    public List<Property> Properties { get; set; }

    public RealEstateAgent() { }

    public RealEstateAgent(User user, string officeEmail, int totalSales, int totalListings, List<string> certifications, List<Languages> languagesSpoken, List<Property> properties)
    {
        User = user;
        UserId = user.Id;
        OfficeEmail = officeEmail;
        TotalSales = totalSales;
        TotalListings = totalListings;
        Certifications = certifications;
        LanguagesSpoken = languagesSpoken ?? new List<Languages>();
        Properties = properties;
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
