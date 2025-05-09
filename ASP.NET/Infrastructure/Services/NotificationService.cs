using Data.Entities;
using Data.Repositories;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public interface INotificationService
{
  Task CreateNotificationAsync(NotificationEntity notification);
  Task DismissNotificationAsync(string notificationId, string userId);
  Task<IEnumerable<NotificationEntity>> GetAllUserNotificationsAsync(string userId, int take = 99);
}

public class NotificationService(INotificationRepository notificationRepository, INotificationPusher notificationPusher) : INotificationService
{
  private readonly INotificationRepository _notificationRepository = notificationRepository;
  private readonly INotificationPusher _notificationPusher = notificationPusher;

  public async Task CreateNotificationAsync(NotificationEntity notification)
  {
    await _notificationRepository.AddNotificationAsync(notification);

    var target = notification.NotificationTargetGroup?.NotificationTargetGroup ?? "allUsers";
    if (target == "allUsers")
    {
      await _notificationPusher.PushToAllAsync(notification);
    }
    else
    {
      await _notificationPusher.PushToAdminsAsync(notification);
    }
  }

  public async Task DismissNotificationAsync(string notificationId, string userId)
  {
    await _notificationRepository.DismissedNotificationAsync(notificationId, userId);
    await _notificationPusher.PushToUserAsync(userId, notificationId);
  }

  public async Task<IEnumerable<NotificationEntity>> GetAllUserNotificationsAsync(string userId, int take = 10)
  {
    return await _notificationRepository.GetAllUserNotificationsAsync(userId, take);
  }
}