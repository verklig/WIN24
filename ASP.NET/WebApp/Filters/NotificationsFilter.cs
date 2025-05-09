using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebApp.Filters;

public class NotificationsFilter(INotificationService notificationService) : IAsyncActionFilter
{
  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    if (context.Controller is Controller controller)
    {
      var userId = controller.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
      var notifications = await notificationService.GetAllUserNotificationsAsync(userId);

      controller.ViewBag.Notifications = notifications.OrderByDescending(n => n.Created).ToList();
    }

    await next();
  }
}
