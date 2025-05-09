using System.Security.Claims;
using Data.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;

namespace Webapp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController(IHubContext<NotificationHub> notificationHub, INotificationService notificationService) : ControllerBase
{
  private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;
  private readonly INotificationService _notificationService = notificationService;

  [HttpPost]
  public async Task<IActionResult> CreateNotification(NotificationEntity notificationEntity)
  {
    await _notificationService.GetAllUserNotificationsAsync(notificationEntity.Id);
    return Ok(new { success = true });
  }

  [HttpPost("dismiss/{notificationId}")]
  public async Task<IActionResult> DismissNotification(string notificationId)
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
    if (string.IsNullOrEmpty(userId))
    {
      return Unauthorized();
    }

    await _notificationService.DismissNotificationAsync(notificationId, userId);
    await _notificationHub.Clients.User(userId).SendAsync("NotificationDismissed", notificationId);

    return Ok(new { success = true });
  }

  [HttpGet]
  public async Task<IActionResult> GetAllNotifications()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
    if (string.IsNullOrEmpty(userId))
    {
      return Unauthorized();
    }

    var notifications = await _notificationService.GetAllUserNotificationsAsync(userId);

    return Ok(notifications);
  }
}