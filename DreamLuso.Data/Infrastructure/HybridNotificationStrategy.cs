using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace DreamLuso.Data.Infrastructure;

//public class HybridNotificationStrategy(IDistributedCache cache, INotificationRepository repository, IConnectionMultiplexer redis) : INotificationStrategy
//{
//    private const string CACHE_KEY_PREFIX = "notifications:";

//    public async Task<IEnumerable<Notifications>> GetPersistentNotification(Guid userId)
//    {
//        return await repository.GetNotificationsAsync(userId);
//    }

//    public async Task<IEnumerable<Notifications>> GetRecentNotifications(Guid userId)
//    {
//        var db = redis.GetDatabase();
//        var cacheKey = $"{CACHE_KEY_PREFIX}{userId}";

//        var cachedValue = await db.StringGetAsync(cacheKey);
//        var cachedNotifications = cachedValue.HasValue
//            ? JsonSerializer.Deserialize<List<Notifications>>(cachedValue!)
//            : new List<Notifications>();

//        var dbNotifications = await repository.GetRecentNotificationsAsync(userId);
//        var allNotifications = new List<Notifications>();

//        if (cachedNotifications != null && cachedNotifications.Any())
//        {
//            allNotifications.AddRange(cachedNotifications);
//        }

//        if (dbNotifications != null && dbNotifications.Any())
//        {
//            allNotifications.AddRange(dbNotifications);
//        }

//        return allNotifications
//            .DistinctBy(n => n.Id)
//            .OrderByDescending(n => n.CreateAt)
//            .ToList();
//    }

//    public async Task StorePersistentNotification(Notifications notification)
//    {
//        await repository.AddAsync(notification);
//        await InvalidateUserCache(notification.RecipientId);
//    }

//    public async Task StoreTransientNotification(Notifications notification)
//    {
//        var db = redis.GetDatabase();
//        var cacheKey = $"{CACHE_KEY_PREFIX}{notification.RecipientId}";

//        var existingValue = await db.StringGetAsync(cacheKey);
//        var existingNotifications = existingValue.HasValue
//            ? JsonSerializer.Deserialize<List<Notifications>>(existingValue!)
//            : new List<Notifications>();

//        if (notification.Id == Guid.Empty)
//        {
//            notification.Id = Guid.NewGuid();
//        }

//        existingNotifications.Add(notification);

//        await db.StringSetAsync(
//            cacheKey,
//            JsonSerializer.Serialize(existingNotifications),
//            TimeSpan.FromDays(7)
//        );
//    }

//    public async Task UpdateNotificationStatus(Notifications notification)
//    {
//        await repository.UpdateAsync(notification);

//        var db = redis.GetDatabase();
//        var cacheKey = $"{CACHE_KEY_PREFIX}{notification.RecipientId}";

//        var cachedValue = await db.StringGetAsync(cacheKey);
//        if (cachedValue.HasValue)
//        {
//            var notifications = JsonSerializer.Deserialize<List<Notifications>>(cachedValue!);
//            var existingNotification = notifications.FirstOrDefault(n => n.Id == notification.Id);
//            if (existingNotification != null)
//            {
//                existingNotification.Status = notification.Status;
//                await db.StringSetAsync(
//                    cacheKey,
//                    JsonSerializer.Serialize(notifications),
//                    TimeSpan.FromDays(7)
//                );
//            }
//        }
//    }
//    private async Task InvalidateUserCache(Guid userId)
//    {
//        var db = redis.GetDatabase();
//        var cacheKey = $"{CACHE_KEY_PREFIX}{userId}";
//        await db.KeyDeleteAsync(cacheKey);
//    }
//}
