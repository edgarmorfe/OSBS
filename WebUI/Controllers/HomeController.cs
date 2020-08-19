using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Attributes;

namespace WebUI.Controllers
{
    [SessionExpire]
    [AuthorizeUserAttribute]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //var userRoles = UserAccountBL.GetUserRoleByUserId(UserInfo.UserId);
            //if(userRoles.Where(r => r.RoleName.ToLower().Equals("driver")).Count() > 0)
            //    return RedirectToAction("ViewPassengers", "Reservation", new { id = UserInfo.UserId });
            //else
            return RedirectToAction("all", "tenantrenewal", new { filter = "all"});
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}