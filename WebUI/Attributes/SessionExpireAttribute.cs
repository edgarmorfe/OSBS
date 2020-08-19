using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Attributes
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["UserContext_UserId"] == null)
            {
                filterContext.Result = new RedirectResult(Configuration.SSOURL);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}