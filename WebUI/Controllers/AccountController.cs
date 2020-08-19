using BL;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Attributes;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return Redirect(Configuration.SSOURL);
        }

        [Route("token")]
        [UnauthorizeUser]
        public ActionResult TokenHandler(string user, string token)
        {
            if (string.IsNullOrEmpty(user))
                return Redirect(Configuration.SSOURL);
            else
            {
                Session["UserContext_Token"] = token;
                var hash = (Configuration.SSOPrivateKey.ToLower() + user.Trim().ToLower() + token.ToLower()).GetHashString().ToLower().Trim();
                return Redirect($"{ Configuration.SSOURL }sso?app={ Configuration.SSOPublicKey }&key={ hash }");
            }
        }

        [Route("session")]
        [UnauthorizeUser]
        public ActionResult SessionHandler(string user, string key)
        {

            var hash = (Configuration.SSOPrivateKey.ToLower() + user.Trim().ToLower() + UserInfo.Token.ToLower()).GetHashString().ToLower().Trim();
            var employee = EmployeeBL.GetEmployeeMasterInfoByLoginId(user).FirstOrDefault();

            if (hash != key.ToLower().Trim() || string.IsNullOrEmpty(employee.empLoginID))
            {
                return Redirect(Configuration.SSOURL);
            }
            else
            {
                //FormsAuthentication.SetAuthCookie(user, true);
                // sometimes used to persist user roles
                string userData = string.Empty;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                  1,                                     // ticket version
                  user,                              // authenticated username
                  DateTime.Now,                          // issueDate
                  DateTime.Now.AddMinutes(30),           // expiryDate
                  true,                                 // true to persist across browser sessions
                  userData,                              // can be used to store additional user data
                  FormsAuthentication.FormsCookiePath);  // the path for the cookie

                // Encrypt the ticket using the machine key
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Add the cookie to the request to save it
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                Session["UserContext_UserId"] = user;
                Session["UserContext_Username"] = employee.empLoginID ?? "";
                Session["UserContext_EmailAddress"] = employee.empCorpEmail ?? "";
                Session["UserContext_FirstName"] = employee.empFName ?? "";
                Session["UserContext_LastName"] = employee.empLName ?? "";
                Session["UserContext_MobileNo"] = employee.empMobile ?? "";
                Session["UserContext_ProfilePic"] = EmployeeHelper.GetEmployeeProfilePicPath(employee);

                // giving new employee a an access rights
                if (UserAccountBL.GetUserRoleByUserId(employee.empLoginID).Count() == 0)
                    UserAccountBL.InsertUserRole(employee.empLoginID, 3);

                //var userRoles = UserAccountBL.GetUserRoleByUserId(UserInfo.UserId);
                //if (userRoles.Where(r => r.RoleName.ToLower().Equals("driver")).Count() > 0)
                //    return RedirectToAction("ViewPassengers", "Reservation", new { id = UserInfo.UserId });
                //else
                    return RedirectToAction("all", "tenantrenewal", new { filter = "all"});
            }
        }

    }
}