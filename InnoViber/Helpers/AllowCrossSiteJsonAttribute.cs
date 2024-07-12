using Microsoft.AspNetCore.Mvc.Filters;

namespace InnoViber.API.Helpers;

public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.HttpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        base.OnActionExecuting(filterContext);
    }
}