using Data.Entities;

namespace Infrastructure.Interfaces;

public interface INotificationPusher
{
  Task PushToAllAsync(NotificationEntity notification);
  Task PushToAdminsAsync(NotificationEntity notification);
  Task PushToUserAsync(string userId, string notificationId);
}
