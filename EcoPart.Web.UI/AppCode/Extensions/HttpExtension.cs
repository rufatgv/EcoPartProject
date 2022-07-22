using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EcoPart.Web.UI.AppCode.Extensions
{
    public static partial class HttpExtension
    {
        public static string GetAppLink(this IActionContextAccessor ctx )
        {
            string scheme = ctx.ActionContext.HttpContext.Request.Scheme;
            string host = ctx.ActionContext.HttpContext.Request.Host.ToString();

            return $"{scheme}://{host}";
        }


    }
}
