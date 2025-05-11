using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
  public class RequireCookieConsentFilter : ActionFilterAttribute
  {
    private const string ConsentCookieName = "cookie_consent";
    private const string LoginPath = "/login";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var hasConsent = context.HttpContext.Request.Cookies.TryGetValue(ConsentCookieName, out var value) && value == "accepted";

      if (!hasConsent)
      {
        context.Result = new RedirectResult(LoginPath + "?consentRequired=true");
      }

      base.OnActionExecuting(context);
    }
  }
}
