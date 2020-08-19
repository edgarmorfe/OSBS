using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Models
{
    public static class Configuration
    {
        public static string BaseURL => string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, new UrlHelper(HttpContext.Current.Request.RequestContext).Content("~"));

        public static string SSOURL => ConfigurationManager.AppSettings["SSOURL"];

        public static string SSOPublicKey => ConfigurationManager.AppSettings["SSOPublicKey"];

        public static string SSOPrivateKey => ConfigurationManager.AppSettings["SSOPrivateKey"];
    }
}