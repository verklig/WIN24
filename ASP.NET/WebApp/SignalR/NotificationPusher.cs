using Data.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;

namespace WebApp.SignalR;

public class NotificationPusher(IHubContext<NotificationHub> hubContext) : INotificationPusher
{
  public async Task PushToAllAsync(NotificationEntity notification)
  {
    await hubContext.Clients.All.SendAsync("AllReceiveNotification", notification);
  }

  public async Task PushToAdminsAsync(NotificationEntity notification)
  {
    await hubContext.Clients.All.SendAsync("AdminsReceiveNotification", notification);
  }

  public async Task PushToUserAsync(string userId, string notificationId)
  {
    await hubContext.Clients.User(userId).SendAsync("NotificationDismissed", notificationId);
  }
}
