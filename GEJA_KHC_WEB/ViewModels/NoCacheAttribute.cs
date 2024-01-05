using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GEJA_KHC_WEB.ViewModels
{
    /// <summary>
    /// Prevent a controller or specific action from being cached in the web browser.
    /// For example - sign in, go to a secure page, sign out, click the back button.
    /// <see also cref="https://stackoverflow.com/questions/6656476/mvc-back-button-issue/6656539#6656539"/>
    /// </summary>
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            response.Cache.SetValidUntilExpires(false);
            response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.Cache.SetNoStore();
        }
    }
}