using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Attributes;

namespace WebUI.Controllers
{
    [SessionExpire]
    public class ErrorHandlerController : Controller
    {
        // GET: ErroHandler
        [HandleError]
        public ActionResult Index()
        {
            return View();
        }

        [HandleError]
        public ActionResult ServerError()
        {
            return View();
        }

        [HandleError]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}