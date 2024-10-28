using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class PropertyVisit : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
    public Guid RealStateAgentId { get; set; }
    public RealStateAgent RealStateAgent { get; set; }
    public DateTime Date { get; set; }
    public VisitStatus VisitStatus { get; set; }

    public PropertyVisit() { }

    public PropertyVisit(Guid id, Property property, Guid propertyId, Client client, Guid clientId, RealStateAgent realStateAgent, Guid realStateAgentId, DateTime date, VisitStatus visitStatus)
    {
        Id = id;
        Property = property ?? throw new ArgumentNullException(nameof(property));
        PropertyId = propertyId;
        Client = client ?? throw new ArgumentNullException(nameof(client));
        ClientId = clientId;
        RealStateAgent = realStateAgent ?? throw new ArgumentNullException(nameof(realStateAgent));
        RealStateAgentId = realStateAgentId;
        Date = date;
        VisitStatus = visitStatus;
    }
}

public enum VisitStatus
{
    Scheduled,
    Completed,
    Pending,
    Canceled,
    Confirmed
}
