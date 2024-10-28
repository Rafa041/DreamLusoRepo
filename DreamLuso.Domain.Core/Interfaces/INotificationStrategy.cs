using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface INotificationStrategy 
{
    Task StoreTransientNotification(Notifications notification);
    Task<IEnumerable<Notifications>> GetRecentNotifications(Guid userId);

    //Notificações importantes (Banco de dados )
    Task StorePersistentNotification(Notifications notification);
    Task<IEnumerable<Notifications>> GetPersistentNotification(Guid userId);
    Task UpdateNotificationStatus(Notifications notification);
}
