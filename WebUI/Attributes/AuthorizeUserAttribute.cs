using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Attributes
{
    public static class UserInfo
    {
        public static string UserId => HttpContext.Current?.Session["UserContext_UserId"]?.ToString();
        public static string Token => HttpContext.Current?.Session["UserContext_Token"]?.ToString();
        public static string Username => HttpContext.Current?.Session["UserContext_Username"]?.ToString();
        public static string FirstName => HttpContext.Current?.Session["UserContext_FirstName"]?.ToString();
        public static string LastName => HttpContext.Current?.Session["UserContext_LastName"]?.ToString();
        public static string FullName => FirstName + " " + LastName;
        public static string EmailAddress => HttpContext.Current?.Session["UserContext_EmailAddress"]?.ToString();
        public static string MobileNo => HttpContext.Current?.Session["UserContext_MobileNo"]?.ToString();
        public static string ProfilePic => HttpContext.Current?.Session["UserContext_ProfilePic"]?.ToString();
    }

    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // need to end every session for every once the referer is sso
            if (httpContext.Request.UrlReferrer == null)
                return false;

            if (httpContext.Request.UrlReferrer.ToString().ToLower().StartsWith(Configuration.SSOURL.ToLower()))
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
            }
            if (!string.IsNullOrEmpty(UserInfo.UserId?.Trim()))
            {
                if (this.Roles.Count() > 0)
                {
                    var userRoles = httpContext.Session["UserContext_UserRoles"]?.ToString().Split(',').Select(o => int.TryParse(o ?? "", out var parsed) ? parsed : 0) ?? new int[0];
                    if (this.Roles.Count(o => userRoles.Contains((int)o)) > 0) return true;
                }
                else return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(UserInfo.UserId?.Trim()))
            {
                filterContext.HttpContext.Response.Redirect($"{ Configuration.SSOURL }token?app={ Configuration.SSOPublicKey }");
            }
            else base.HandleUnauthorizedRequest(filterContext);
        }
    }
}