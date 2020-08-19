using BL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Utility;
using WebUI.Attributes;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    [SessionExpire]
    [Authorize(Roles = "Employee,Driver")]
    public class ReservationController : Controller
    {
        // GET: OTReservation
        public ActionResult Index()
        {
            return RedirectToAction("Book", new { bookingType = "ot" });
        }

        public ActionResult Book(string bookingType)
        {
            OTReservationNewModel model = new OTReservationNewModel();
            
            TempData["header"] = "Reservation";
            return View(model);
        }

        private static void GenerateCurrentBooking(OTReservationNewModel model)
        {
            //var reserves = ReservationBL.GetReservationByEmpId(UserInfo.UserId).Where(r => r.Status.ToLower() == "notassigned" || r.Status.ToLower() == "assigned"
            //|| r.Status.ToLower() == "forapproval" || r.Status.ToLower() == "approved").ToList();

            var reserves = ReservationBL.GetReservationByEmpIdToday(UserInfo.UserId, DateTime.Now);
            var OTBooking = reserves.Where(b => b.ResType.Equals("ot")).ToList();
            var BusinessBooking = reserves.Where(b => b.ResType.Equals("bs")).ToList();

            // generate OT Booking
            model.OTBooking = new List<BookingModel>();

            // generate business booking
            model.BusinessBooking = new List<BookingModel>();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Edit")]
        public ActionResult Edit(OTReservationNewModel model)
        {
            TimeSpan t;
            TimeSpan.TryParse(model.TimeSlotSelectedId, out t);
            model.ReservationDate = model.ReservationDate.Add(t);

            //var rv = ReservationBL.Book(model.Id,
            //    Session["UserContext_UserId"].ToString(),
            //    model.PickUpSelectedId.ToString(),
            //    model.DropOffSelectedId.ToString(),
            //    model.ReservationDate,
            //    model.Comment,
            //    model.Status);

            TempData["message"] = "Update booking is successful";
            return RedirectToAction("Book", new { BookingType = model.BookingType });
        }

      

        #region View
        private OTReservationViewModel GenerateDriverModel(Reservation reserveModel)
        {
            OTReservationViewModel model = new OTReservationViewModel()
            {
                Id = reserveModel.Id,
                EmpId = reserveModel.EmpId,
                Seat = reserveModel.Seat,
                ScheduledTrip = reserveModel.ScheduledTrip,
                DriverId = reserveModel.DriverId,
                ShuttleId = reserveModel.ShuttleId,
                SSDID = reserveModel.SsdId,
                SSDStatus = reserveModel.SSDStatus
            };
            return model;
        }

        [HttpGet]
        public JsonResult GetApprovalCount()
        {
            var empNo = EmployeeBL.GetEmployeeMasterInfoByLoginId(UserInfo.UserId).FirstOrDefault().empNo;
            var emps = EmployeeBL.GetEmployeeMasterInfoBySupEmpNo(empNo);
            var approvalCount = 0;
            foreach (Employee emp in emps)
            {
                approvalCount += ReservationBL.GetReservationByEmpId(emp.empLoginID).Where(r => r.ResType.ToLower().Equals("bs") && r.Status.ToLower().Equals("forapproval")).Count();
            }
            return Json(approvalCount, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}