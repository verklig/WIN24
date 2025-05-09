using Data.Contexts;
using Data.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface INotificationRepository : IBaseRepository<UserEntity, User>
{
  Task AddNotificationAsync(NotificationEntity notificationEntity);
  Task DismissedNotificationAsync(string notificationId, string UserId);
  Task<IEnumerable<NotificationEntity>> GetAllUserNotificationsAsync(string userId, int take = 10);
}

public class NotificationRepository(AppDbContext context) : BaseRepository<UserEntity, User>(context), INotificationRepository
{
  public async Task AddNotificationAsync(NotificationEntity notificationEntity)
  {
    if (string.IsNullOrEmpty(notificationEntity.Image))
    {
      switch (notificationEntity.NotificationTypeId)
      {
        case 1:
          notificationEntity.Image = "/images/profile-picture-placeholder.svg";
          break;
        case 2:
          notificationEntity.Image = "/images/project-image-placeholder.svg";
          break;
      }
    }

    _context.Add(notificationEntity);
    await _context.SaveChangesAsync();
  }

  public async Task DismissedNotificationAsync(string notificationId, string UserId)
  {
    var alreadyDismissed = await _context.NotificationDismissed.AnyAsync(n => n.NotificationId == notificationId && n.UserId == UserId);
    if (!alreadyDismissed)
    {
      var dismissed = new NotificationDismissedEntity
      {
        NotificationId = notificationId,
        UserId = UserId
      };

      _context.Add(dismissed);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<IEnumerable<NotificationEntity>> GetAllUserNotificationsAsync(string userId, int take)
  {
    var dismissedIds = await _context.NotificationDismissed
      .Where(n => n.UserId == userId)
      .Select(n => n.NotificationId)
      .ToListAsync();

    var notifications = await _context.Notifications
      .Where(n => !dismissedIds.Contains(n.Id))
      .OrderByDescending(n => n.Created)
      .Take(take)
      .ToListAsync();

    return notifications;
  }
}