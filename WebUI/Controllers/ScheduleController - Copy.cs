using BL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Attributes;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Controllers
{
    [SessionExpire]
    [Authorize(Roles = "Admin")]
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        //public ActionResult Add()
        //{
        //    ScheduleModel model = new ScheduleModel();
        //    model = GenerateDefaultValue(model);

        //    return View(model);
        //}


        [Authorize(Roles = "Admin")]
        public ActionResult Add(DateTime selectedTime, int pickUpId, string dropOff, int resId, string resType)
        {
            ScheduleModel model = new ScheduleModel();
            model.ResType = resType;
            if (resType.Equals("ot"))
            {
                model = GenerateOTDefaultValue(model, selectedTime, resType);
                if (pickUpId != 0)
                {
                    model.ResType = "ot";
                    model.PickUpSelectedId = pickUpId;
                    model.DropOffSelectedId = LocationCodeBL.Read().Where(p => p.Name.ToLower() == dropOff.ToLower()).FirstOrDefault().Id;
                }
            }
            else
            {
                var res = ReservationBL.GetReservationById(resId).FirstOrDefault();
                model = GenerateBusinessDefaultValue(model, selectedTime, resType);
                model.TimeSlots = new List<TimeSlot>()
                {
                    new TimeSlot() {Id = res.ScheduledTrip.ToShortTimeString(), Name = res.ScheduledTrip.ToShortTimeString()}
                };
                //model.TimeSlotSelectedId = $"{res.ScheduledTrip.ToShortTimeString()}";
                model.TimeSlotSelectedId = res.ScheduledTrip.TimeOfDay.ToString();
                model.ScheduleTrip = res.ScheduledTrip;
                model.DropOffSelectedId = Convert.ToInt32(res.DropOff);
                model.PickUpSelectedId = Convert.ToInt32(res.PickUp);
                //model.VIP = res.V
                model.IsReadOnly = true;
                model.Id = res.Id;
                model.ResType = "bs";
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Add(ScheduleModel model)
        {
            if (!IsValidScheduleDateField(model) || !IsValidShuttleOrDriver(model))
            {
                model = GenerateOTDefaultValue(model, model.ScheduleTrip, model.ResType);
                return View(model);
            }

            // for sending sMs, already book but dont assigned shuttle
            // sending sMS for their assigned shuttle and driver
            var shu = ShuttleBL.GetShuttleById(model.ShuttleSelectedId);

            var reservations = new List<Reservation>();
            var dropOffName = string.Empty;
            if (model.ResType.Equals("ot"))
            {
                reservations = ReservationBL.GetReservationBySchedDatePickUpAndDropOff(model.ScheduleTrip,
                    model.PickUpSelectedId,
                    model.DropOffSelectedId).Take(shu.SeatCount).ToList();
                ScheduleBL.Save(-1,
                              model.ScheduleTrip,
                              model.PickUpSelectedId,
                              model.DropOffSelectedId,
                              model.ShuttleSelectedId,
                              model.DriverSelectedId,
                              Session["UserContext_UserId"].ToString(),
                              Session["UserContext_UserId"].ToString());
                dropOffName = LocationCodeBL.Read().Where(l => l.Id == model.DropOffSelectedId).FirstOrDefault().Name;

            }
            else
            {
                reservations = ReservationBL.GetBusinessReservationBySchedDatePickUpAndDropOff(model.ScheduleTrip,
                  model.PickUpSelectedId,
                  model.DropOffSelectedId);
                ScheduleBL.SaveBusiness(-1,
                         model.ScheduleTrip,
                         model.PickUpSelectedId,
                         model.DropOffSelectedId,
                         model.ShuttleSelectedId,
                         model.DriverSelectedId,
                         Session["UserContext_UserId"].ToString(),
                         Session["UserContext_UserId"].ToString());
                dropOffName = PlaceBL.GetPlaceById(model.DropOffSelectedId).FirstOrDefault().Name;
            }

            // for sms
            string pickUp = PlaceBL.GetPlaceById(Convert.ToInt32(model.PickUpSelectedId)).FirstOrDefault().Name;
            var driver = EmployeeBL.GetEmployeeMasterInfoByLoginId(DriverBL.Read().Where(d => d.Id == model.DriverSelectedId).FirstOrDefault().UserName).FirstOrDefault();

            foreach (Reservation res in reservations)
            {
                var employee = EmployeeBL.GetEmployeeMasterInfoByLoginId(res.EmpId).FirstOrDefault();
                var dropOffLocation = PlaceBL.GetPlaceById(Convert.ToInt32(res.DropOff)).FirstOrDefault().Name;
                if (shu.PlateNo.ToLower().Equals("grab"))
                    SMSHelper.SendSMS(employee.empMobile, $"Hi {employee.empFName}, No available shuttle, kindly book a grab and charge it against your grab business.");
                else
                    SMSHelper.SendSMS(employee.empMobile, $"Yay! We found you a ride!\\n\\nUpdated Booking Details\\nEmployee's Name: {employee.empFName} {employee.empLName}\\nDate: {DateHelper.SMSDateFormat(res.ScheduledTrip)}; " +
                       $"{res.ScheduledTrip.ToShortTimeString()} \\nRoute: {pickUp} - {dropOffLocation}; {dropOffName}" +
                       $"\\nPlate Number: {shu.PlateNo} \\nDriver: {driver.empFName} \\nBooking Ref. No. {res.Id} \\n\\nYou may opt to cancel your booking at least 1 hour before departure" +
                       $" by logging into your account. Your booking fee will be returned to your e-wallet after you cancel your booking. For all other concerns, please send an email to rlcso@rockwell.com.ph");
            }

            // send driver an assignment
            if (model.ResType.Equals("ot"))
                SMSHelper.SendSMS(driver.empMobile, $"Hi {driver.empFName}, you are assigned to drive the {model.ScheduleTrip.ToShortTimeString()} {dropOffName} ROCK Employee Shuttle!" +
                $"\\n\\nTrip Details \\nDate: {DateHelper.SMSDateFormat(model.ScheduleTrip)}; {model.ScheduleTrip.ToShortTimeString()} " +
                $"\\nPlate Number: {shu.PlateNo} \\nRoute: {pickUp} - {dropOffName}");
            else if (model.ResType.Equals("bs") && !shu.PlateNo.ToLower().Equals("grab"))
            {
                var VIP = reservations[0].VIP.Equals(true) ? "Yes" : "No";
                SMSHelper.SendSMS(driver.empMobile, $"You are assigned in Shuttle {shu.PlateNo} bound {pickUp} to {dropOffName}." +
                $"\\n\\nTrip Details \\nDate: {DateHelper.SMSDateFormat(model.ScheduleTrip)}; {model.ScheduleTrip.ToShortTimeString()} " +
                $"\\nPlate Number: {shu.PlateNo} \\nDestination: {pickUp} - {dropOffName} \\nSeat: {reservations[0].Seat} " +
                $"\\nVIP: {VIP} \\nNotes: Business Trip");
            }

            TempData["message"] = "Schedule Saved!";
            return RedirectToAction("Add", new { selectedTime = model.ScheduleTrip, pickUpId = 0, dropOff = "", resId = model.Id, resType = model.ResType });
        }

        public ActionResult ViewAll()
        {
            var schedules = ScheduleBL.GetAllScheduleToday();
            StringBuilder schedStr = GenerateList(schedules);
            TempData["reseveList"] = schedStr;
            return View();
        }

        public ActionResult ViewBySchedule(int id)
        {
            ScheduleModel model = new ScheduleModel();
            model = GenerateOTDefaultValue(model, model.ScheduleTrip, "ot");

            var schedules = ScheduleBL.GetScheduleByScheduleId(id);
            StringBuilder schedStr = GenerateList(schedules);
            TempData["reseveList"] = schedStr;
            return View(model);
        }

        public ActionResult ViewPassengers(DateTime schedTrip, int pickUpId, int dropOffId, string resType)
        {
            ScheduleModel model = new ScheduleModel();
            // model.Schedules.Where(s => s.Time == schedTrip.ToShortTimeString()).ToList().ForEach(s => s.IsActivecollapsible = "active");

            var passengers = new List<Reservation>();
            model.ResType = resType;
            if (resType.Equals("ot"))
            {
                model = GenerateOTDefaultValue(model, schedTrip, resType);
                passengers = ReservationBL.GetAllReservationsByScheduledTripPickUpDropOffAndStatus(schedTrip, pickUpId, dropOffId.ToString(), "Active");
            }
            else
            {
                model = GenerateBusinessDefaultValue(model, schedTrip, resType);
                passengers = ReservationBL.GetBusinessReservationsByScheduledTripPickUpDropOffAndStatus(schedTrip, pickUpId, dropOffId.ToString(), "Active").Where(r => r.ScheduledTrip == schedTrip).ToList();
            }

            var schedule = ScheduleBL.GetScheduleBySchedTripPickUpAndDropOff(schedTrip, pickUpId, dropOffId).FirstOrDefault();

            model.Passengers = new List<ShuttleDriverPassenger>();
            if (schedule != null)
            {
                var shuttles = ScheduleShuttleDriverBL.GetSchedShuttleDriverBySchedId(schedule.Id);
                foreach (ScheduleShuttleDriver shu in shuttles)
                {
                    var passengerList = passengers.Where(p => p.ShuttleId == shu.ShuttleId).ToList();
                    ShuttleDriverPassenger shuDriPas = new ShuttleDriverPassenger();
                    shuDriPas.Passengers = new List<Passenger>();

                    string shuttlePlateNo = ShuttleBL.GetShuttleById(shu.ShuttleId).PlateNo;
                    string driverName = DriverBL.GetDriverById(shu.DriverId).FirstName;
                    string pickUp = PlaceBL.GetPlaceById(Convert.ToInt32(schedule.PickUp)).FirstOrDefault().Name;
                    var dropOffLocationCodeName = string.Empty;
                    if (resType.Equals("ot"))
                        dropOffLocationCodeName = LocationCodeBL.Read().Where(l => l.Id == Convert.ToInt32(schedule.DropOff)).FirstOrDefault().Name;
                    else
                        dropOffLocationCodeName = PlaceBL.GetPlaceById(Convert.ToInt32(schedule.DropOff)).FirstOrDefault().Name;

                    shuDriPas.Id = shu.Id;
                    shuDriPas.ShuttleId = shu.ShuttleId;
                    shuDriPas.ShuttlePlateNo = shuttlePlateNo;
                    shuDriPas.RouteName = $"{pickUp} - {dropOffLocationCodeName}";
                    shuDriPas.DriverId = shu.DriverId;
                    shuDriPas.DriverName = driverName;
                    shuDriPas.Status = shu.Status;
                    shuDriPas.SchedDate = schedTrip;

                    foreach (Reservation pas in passengerList)
                    {
                        var empName = EmployeeBL.GetEmployeeMasterInfoByLoginId(pas.EmpId).FirstOrDefault();
                        string dropOffName = PlaceBL.GetPlaceById(Convert.ToInt32(pas.DropOff)).FirstOrDefault().Name;
                        string profilePicPath = EmployeeHelper.GetEmployeeProfilePicPath(empName);

                        Passenger passenger = new Passenger()
                        {
                            EmpId = pas.EmpId,
                            FullName = $"{empName.empFName} {empName.empLName}",
                            ProfilePicPath = profilePicPath,
                            Status = pas.Status,
                            DropOff = dropOffName,
                            VIP = pas.VIP.Equals(true) ? "Yes" : "No",
                            ResType = pas.ResType,
                            PassCount = Convert.ToInt32(pas.Seat)
                        };
                        shuDriPas.Passengers.Add(passenger);
                    }
                    model.Passengers.Add(shuDriPas);
                }
            }
            else if (!passengers.Count().Equals(0))
            {
                ShuttleDriverPassenger shuDriPas = new ShuttleDriverPassenger();
                shuDriPas.Passengers = new List<Passenger>();

                string shuttlePlateNo = "N/A";
                string driverName = "N/A";
                string pickUp = PlaceBL.GetPlaceById(Convert.ToInt32(passengers[0].PickUp)).FirstOrDefault().Name;
                int placeCodeId = PlaceBL.GetPlaceById(Convert.ToInt32(passengers[0].DropOff)).FirstOrDefault().PlaceCode;
                var dropOffLocationCodeName = string.Empty;
                if (resType.Equals("ot"))
                    dropOffLocationCodeName = LocationCodeBL.Read().Where(l => l.Id == Convert.ToInt32(placeCodeId)).FirstOrDefault().Name;
                else
                    dropOffLocationCodeName = PlaceBL.GetPlaceById(Convert.ToInt32(passengers[0].DropOff)).FirstOrDefault().Name;

                shuDriPas.Id = passengers[0].SsdId;
                shuDriPas.ShuttleId = 0;
                shuDriPas.ShuttlePlateNo = shuttlePlateNo;
                shuDriPas.RouteName = $"{pickUp} - {dropOffLocationCodeName}";
                shuDriPas.DriverId = 0;
                shuDriPas.DriverName = driverName;
                shuDriPas.Status = "";
                shuDriPas.SchedDate = schedTrip;

                foreach (Reservation pas in passengers)
                {
                    var empName = EmployeeBL.GetEmployeeMasterInfoByLoginId(pas.EmpId).FirstOrDefault();
                    string dropOffName = PlaceBL.GetPlaceById(Convert.ToInt32(pas.DropOff)).FirstOrDefault().Name;
                    string profilePicPath = EmployeeHelper.GetEmployeeProfilePicPath(empName);

                    Passenger passenger = new Passenger()
                    {
                        EmpId = pas.EmpId,
                        FullName = $"{empName.empFName} {empName.empLName}",
                        ProfilePicPath = profilePicPath,
                        Status = pas.Status,
                        DropOff = dropOffName,
                        VIP = pas.VIP.Equals(true) ? "Yes" : "No",
                        ResType = pas.ResType,
                        PassCount = Convert.ToInt32(pas.Seat)
                    };
                    shuDriPas.Passengers.Add(passenger);
                }
                model.Passengers.Add(shuDriPas);
            }

            return View(model);
        }

        public ActionResult Edit(int id, string bookingType)
        {
            ScheduleModel model = new ScheduleModel();
            var sched = ScheduleBL.GetAllScheduleToday().Where(s => s.SchedShuttleDriverId == id).FirstOrDefault();

            model.ResType = bookingType;
            if (bookingType.Equals("ot"))
                model = GenerateOTDefaultValue(model, sched.ScheduledTrip, bookingType);
            else
            {
                model = GenerateBusinessDefaultValue(model, sched.ScheduledTrip, bookingType);
                model.TimeSlots = new List<TimeSlot>()
                {
                    new TimeSlot() {Id = sched.ScheduledTrip.ToShortTimeString(), Name = sched.ScheduledTrip.ToShortTimeString()}
                };
                model.TimeSlotSelectedId = sched.ScheduledTrip.TimeOfDay.ToString();
            }

            model.Id = id;
            model.IsReadOnly = true;
            model.ScheduleTrip = sched.ScheduledTrip;
            model.PickUpSelectedId = sched.PickUp;
            model.DropOffSelectedId = sched.DropOff;
            model.ShuttleSelectedId = sched.ShuttleId;
            model.DriverSelectedId = sched.DriverId;
            model.OldDriverId = sched.DriverId;
            model.OldShuttleId = sched.ShuttleId;
            //model.TimeSlotSelectedId = TimeSlotHelper.ConvertHour(sched.ScheduledTrip.ToShortTimeString());

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ScheduleModel model)
        {
            if (!IsValidScheduleDateField(model) || !IsValidShuttleOrDriverUpdate(model))
            {
                model = GenerateOTDefaultValue(model, model.ScheduleTrip, "ot");
                return View(model);
            }

            ScheduleBL.Save(model.Id,
                    model.ScheduleTrip,
                    model.PickUpSelectedId,
                    model.DropOffSelectedId,
                    model.ShuttleSelectedId,
                    model.DriverSelectedId,
                    Session["UserContext_UserId"].ToString(),
                    Session["UserContext_UserId"].ToString());

            var shu = ShuttleBL.GetShuttleById(model.ShuttleSelectedId);
            var reservations = new List<Reservation>();
            var dropOffName = string.Empty;
            if (model.ResType.Equals("ot"))
            {
                reservations = ReservationBL.GetReservationBySchedDatePickUpAndDropOff(model.ScheduleTrip,
                    model.PickUpSelectedId,
                    model.DropOffSelectedId).Take(shu.SeatCount).ToList();
                dropOffName = LocationCodeBL.Read().Where(l => l.Id == model.DropOffSelectedId).FirstOrDefault().Name;

            }
            else
            {
                reservations = ReservationBL.GetBusinessReservationBySchedDatePickUpAndDropOff(model.ScheduleTrip,
                  model.PickUpSelectedId,
                  model.DropOffSelectedId);
                dropOffName = PlaceBL.GetPlaceById(model.DropOffSelectedId).FirstOrDefault().Name;
            }
            // for sms
            string pickUp = PlaceBL.GetPlaceById(Convert.ToInt32(model.PickUpSelectedId)).FirstOrDefault().Name;
            var driver = EmployeeBL.GetEmployeeMasterInfoByLoginId(DriverBL.Read().Where(d => d.Id == model.DriverSelectedId).FirstOrDefault().UserName).FirstOrDefault();

            foreach (Reservation res in reservations)
            {
                var employee = EmployeeBL.GetEmployeeMasterInfoByLoginId(res.EmpId).FirstOrDefault();
                var dropOffLocation = PlaceBL.GetPlaceById(Convert.ToInt32(res.DropOff)).FirstOrDefault().Name;
                if (shu.PlateNo.ToLower().Equals("grab"))
                    SMSHelper.SendSMS(employee.empMobile, $"Updated Booking Details\\nHi {employee.empFName}, No available shuttle, kindly book a grab and charge it against your grab business.");
                else
                    SMSHelper.SendSMS(employee.empMobile, $"Updated Booking Details\\nEmployee's Name: {employee.empFName} {employee.empLName}\\nDate: {DateHelper.SMSDateFormat(res.ScheduledTrip)}; " +
                       $"{res.ScheduledTrip.ToShortTimeString()} \\nRoute: {pickUp} - {dropOffLocation}; {dropOffName}" +
                       $"\\nPlate Number: {shu.PlateNo} \\nDriver: {driver.empFName} \\nBooking Ref. No. {res.Id} \\n\\nYou may opt to cancel your booking at least 1 hour before departure" +
                       $" by logging into your account. Your booking fee will be returned to your e-wallet after you cancel your booking. For all other concerns, please send an email to rlcso@rockwell.com.ph");
            }

            // send driver an assignment
            if (model.ResType.Equals("ot"))
                SMSHelper.SendSMS(driver.empMobile, $"Updated Assignement:\\nHi {driver.empFName}, you are assigned to drive the {model.ScheduleTrip.ToShortTimeString()} {dropOffName} ROCK Employee Shuttle!" +
                $"\\n\\nTrip Details \\nDate: {DateHelper.SMSDateFormat(model.ScheduleTrip)}; {model.ScheduleTrip.ToShortTimeString()} " +
                $"\\nPlate Number: {shu.PlateNo} \\nRoute: {pickUp} - {dropOffName}");
            else if (model.ResType.Equals("bs") && !shu.PlateNo.ToLower().Equals("grab"))
            {
                var VIP = reservations[0].VIP.Equals(true) ? "Yes" : "No";
                SMSHelper.SendSMS(driver.empMobile, $"Updated Assignment:\\nYou are assigned in Shuttle {shu.PlateNo} bound {pickUp} to {dropOffName}." +
                $"\\n\\nTrip Details \\nDate: {DateHelper.SMSDateFormat(model.ScheduleTrip)}; {model.ScheduleTrip.ToShortTimeString()} " +
                $"\\nPlate Number: {shu.PlateNo} \\nDestination: {pickUp} - {dropOffName} \\nSeat: {reservations[0].Seat} " +
                $"\\nVIP: {VIP} \\nNotes: Business Trip");
            }
            TempData["message"] = "Schedule Saved!";
            return RedirectToAction("Edit", new { id = model.Id, bookingType = model.ResType });
        }

        private static StringBuilder GenerateList(List<Schedule> schedules)
        {
            StringBuilder schedStr = new StringBuilder();
            foreach (Schedule sched in schedules)
            {
                //var pickUpName = PlaceBL.Read().Where(p => p.Id == sched.PickUp).FirstOrDefault().Name;
                //var dropOffName = LocationCodeBL.Read().Where(l => l.Id == sched.DropOff).FirstOrDefault().Name;
                var shuttleName = ShuttleBL.GetShuttleById(sched.ShuttleId).PlateNo;
                var driverName = DriverBL.GetDriverById(sched.DriverId).FirstName;
                string badgeColor = "green";//sched.ShuStatus == "Assigned" || sched.ShuStatus == "InRoute" ? "green" : "red";

                schedStr.Append($"[\"{shuttleName}\", \"{driverName}\", " +
                    $"'<span class=\"new badge {badgeColor}\" data-badge-caption=\"\">{sched.ShuStatus}</span>', " +
                    $"'<a href=\"/Schedule/Edit/{sched.SchedShuttleDriverId}\"><i class=\"material-icons\">edit</i></a>'],");
            }
            if (schedStr.Length > 0)
                schedStr.Remove(schedStr.Length - 1, 1);
            return schedStr;
        }

        private bool IsValidScheduleDateField(ScheduleModel model)
        {
            TimeSpan t;
            TimeSpan.TryParse(model.TimeSlotSelectedId, out t);
            model.ScheduleTrip = model.ScheduleTrip.Add(t);

            // check if the selected date and time is more than the current datetime
            if (model.ScheduleTrip <= DateTime.Now && model.Id == -1)
            {
                TempData["error"] = "Schedule Date and Time is not valid";
                return false;
            }
            return true;
        }

        protected bool IsValidShuttleOrDriver(ScheduleModel model)
        {
            //var pickUpName = PlaceBL.Read().Where(p => p.Id == model.PickUpSelectedId).FirstOrDefault().Name;
            //var dropOffName = LocationCodeBL.Read().Where(l => l.Id == model.DropOffSelectedId).FirstOrDefault().Name;

            // check if for duplicate shuttle
            var schedule = ScheduleBL.GetScheduleByScheduledTripAndStatus(model.ScheduleTrip);
            if (schedule.Where(s => s.ShuttleId == model.ShuttleSelectedId).ToList().Count > 0)
            {
                TempData["error"] = "Shuttle is already scheduled for the same schedule";
                return false;
            }

            // check if for duplicate driver
            if (schedule.Where(s => s.DriverId == model.DriverSelectedId).ToList().Count > 0)
            {
                TempData["error"] = "Driver is already scheduled for the same schedule";
                return false;
            }
            return true;
        }

        protected bool IsValidShuttleOrDriverUpdate(ScheduleModel model)
        {
            //var pickUpName = PlaceBL.Read().Where(p => p.Id == model.PickUpSelectedId).FirstOrDefault().Name;
            //var dropOffName = LocationCodeBL.Read().Where(l => l.Id == model.DropOffSelectedId).FirstOrDefault().Name;

            // check if for duplicate shuttle and driver
            var shedules = ScheduleBL.GetScheduleBoxes();
            if (shedules.Where(s => s.ScheduledTrip == model.ScheduleTrip && s.ShuttleCount == model.ShuttleSelectedId).ToList().Count > 0 &&
              model.OldShuttleId != model.ShuttleSelectedId)
            {
                TempData["error"] = "Shuttle is already scheduled for the same schedule";
                return false;
            }

            // check if for duplicate driver
            if (shedules.Where(s => s.ScheduledTrip == model.ScheduleTrip && s.DriverId == model.DriverSelectedId).ToList().Count > 0 &&
              model.OldDriverId != model.DriverSelectedId)
            {
                TempData["error"] = "Driver is already scheduled for the same schedule";
                return false;
            }

            return true;
        }

        protected ScheduleModel GenerateOTDefaultValue(ScheduleModel model, DateTime selectedTime, string resType)
        {
            model.TimeSlots = new List<TimeSlot>();
            var routeTimes = RouteFareBL.GetRouteTime();
            foreach (RouteFare time in routeTimes)
            {
                model.TimeSlots.Add(new TimeSlot()
                {
                    Id = time.Time.ToString("HH:mm"),
                    Name = time.Time.ToShortTimeString()
                });
            }
            model.PickUps = LocationHelper.GenerateList(PlaceBL.Read().Where(p => p.PlaceCode == 5).ToList());
            model.DropOffs = LocationHelper.GenerateList(LocationCodeBL.Read());
            model.Shuttles = ShuttleHelper.GenerateList(ShuttleBL.Read().Where(s => Convert.ToInt32(s.Coding) != ((int)DateTime.Now.DayOfWeek + 1)).Where(s => s.Status == "1").ToList());
            model.Drivers = DriverHelper.GenerateList(DriverBL.Read().Where(d => d.Status == true).ToList());
            model.TimeSlotSelectedId = $"{selectedTime.Hour}:00";
            model.Schedules = GenerateScheduleBoxes(selectedTime, resType);

            model.ScheduleTrip = selectedTime;
            model.Id = -1;

            return model;
        }

        protected ScheduleModel GenerateBusinessDefaultValue(ScheduleModel model, DateTime selectedTime, string resType)
        {
            model.PickUps = LocationHelper.GenerateList(PlaceBL.Read().Where(p => p.PlaceCode == 7).ToList());
            model.DropOffs = model.PickUps;
            model.Shuttles = ShuttleHelper.GenerateList(ShuttleBL.Read().Where(s => Convert.ToInt32(s.Coding) != ((int)DateTime.Now.DayOfWeek + 1)).Where(s => s.Status == "1").ToList());
            model.Drivers = DriverHelper.GenerateList(DriverBL.Read().Where(d => d.Status == true).ToList());
            model.Schedules = GenerateScheduleBoxes(selectedTime, resType);

            model.ScheduleTrip = selectedTime;
            model.Id = -1;

            return model;
        }

        protected List<ScheduleBoxModel> GenerateScheduleBoxes(DateTime selectedTime, string resType)
        {
            List<ScheduleBoxModel> list = new List<ScheduleBoxModel>();

            var schedules = ScheduleBL.GetScheduleBoxes();

            List<TimeSlot> timeSlots = new List<TimeSlot>() {
                 new TimeSlot() { Id = "BUSINESS", Name = "BUSINESS" }
            };
            var routeTimes = RouteFareBL.GetRouteTime();
            foreach (RouteFare time in routeTimes)
            {
                timeSlots.Add(new TimeSlot()
                {
                    Id = time.Time.ToString("HH:mm"),
                    Name = time.Time.ToShortTimeString()
                });
            }

            GenerateScheduleBox(list, schedules, timeSlots, selectedTime, resType);
            return list;
        }

        private static void GenerateScheduleBox(List<ScheduleBoxModel> list, List<ScheduleBox> schedules,
            List<TimeSlot> timeSlots, DateTime selectedTime, string resType)
        {
            for (int i = 0; i < timeSlots.Count(); i++)
            {
                ScheduleBoxModel schedBoxMod = new ScheduleBoxModel();
                var schedFilter = schedules.Where(s => s.ScheduledTrip.ToShortTimeString() == timeSlots[i].Name);

                if (timeSlots[i].Name.ToLower() == "business")
                {
                    schedules = ScheduleBL.GetAllBusinessScheduleBox();
                    schedBoxMod.IsActivecollapsible = resType.Equals("bs") ? "active" : "";
                    GenerateBusinessScheduleBox(list, timeSlots, i, schedBoxMod, schedules);
                }
                else if (timeSlots[i].Name.Contains("AM"))
                {
                    schedBoxMod.IsActivecollapsible = selectedTime.ToShortTimeString() == timeSlots[i].Name && resType.Equals("ot") ? "active" : "";
                    GenerateAMScheduleBox(list, timeSlots, i, schedBoxMod, schedFilter);
                }
                else
                {
                    schedBoxMod.IsActivecollapsible = selectedTime.ToShortTimeString() == timeSlots[i].Name && resType.Equals("ot") ? "active" : "";
                    GeneratePMScheduleBox(list, timeSlots, i, schedBoxMod, schedFilter);
                }
            }
        }

        private static void GenerateBusinessScheduleBox(List<ScheduleBoxModel> list, List<TimeSlot> timeSlots, int i, ScheduleBoxModel schedBoxMod, IEnumerable<ScheduleBox> schedFilter)
        {
            List<ScheduleBoxChildModel> listChild = new List<ScheduleBoxChildModel>();

            schedBoxMod.Time = timeSlots[i].Name;
            schedBoxMod.Child = new List<ScheduleBoxChildModel>();

            TimeSpan t;
            TimeSpan.TryParse(timeSlots[i].Id, out t);
            var schedTrip = DateTime.Now.Date.Add(t);

            var reservations = ReservationBL.GetBusinessReservationCountByScheduleTrip(schedTrip).Where(r => r.ResType.ToLower() == "bs").ToList();

            string[] routes = new string[reservations.Count()];
            string[] shortRoute = new string[reservations.Count()];

            for (int a = 0; a < reservations.Count; a++)
            {
                var pickUp = PlaceBL.GetPlaceById(reservations[a].PickUpId).FirstOrDefault();
                var dropOff = PlaceBL.GetPlaceById(reservations[a].DropOffId).FirstOrDefault();
                routes[a] = $"{pickUp.Name} - {dropOff}";
                shortRoute[a] = $"{pickUp.Name.Substring(0, 1)} - {dropOff.Name.Substring(0, 1)}";
            }

            GenerateScheduleBusinessChildBox(schedTrip, reservations, schedBoxMod, schedFilter, routes, shortRoute);
            list.Add(schedBoxMod);
        }

        private static void GenerateScheduleBusinessChildBox(DateTime schedTrip,
           List<Reservation> reservations,
           ScheduleBoxModel schedBoxMod,
           IEnumerable<ScheduleBox> schedFilter,
           string[] routes,
           string[] shortRoute)
        {
            int a = 0;
            foreach (Reservation res in reservations)
            {
                ScheduleBoxChildModel child = new ScheduleBoxChildModel();
                child.Id = res.Id;
                child.RouteName = shortRoute[a];
                child.SchedTime = res.ScheduledTrip;
                child.OccupiedSeat = 0;
                child.ShuttleAssigned = 0;
                child.TotalSeat = 0;
                child.ResType = "bs";
                child.PickUpId = res.PickUpId;
                child.DropOffId = res.DropOffId;
                child.DropOff = res.DropOff;
                child.OccupiedSeat = Convert.ToInt32(res.Seat);
                //child.Color = "gold";

                var sched = schedFilter.Where(s => s.PickUp == res.PickUp && s.DropOff == res.DropOff && s.ScheduledTrip == res.ScheduledTrip).ToList();
                if (sched.Count() > 0)
                {
                    child.ShuttleAssigned = sched.Select(s => s.Id).Count();
                    child.TotalSeat = sched.Select(s => s.SeatCount).Sum();
                }

                if (child.OccupiedSeat > child.TotalSeat)
                {
                    child.Color = "red";
                    schedBoxMod.Color = "red";
                }
                a++;
                schedBoxMod.Child.Add(child);
            }
        }

        private static void GenerateAMScheduleBox(List<ScheduleBoxModel> list, List<TimeSlot> timeSlots, int i, ScheduleBoxModel schedBoxMod, IEnumerable<ScheduleBox> schedFilter)
        {
            List<ScheduleBoxChildModel> listChild = new List<ScheduleBoxChildModel>();

            schedBoxMod.Time = timeSlots[i].Name;
            schedBoxMod.Child = new List<ScheduleBoxChildModel>();

            string[] routes = new string[1];
            routes[0] = "SM North - Joya";

            string[] shortRoute = new string[1];
            shortRoute[0] = "S - J";

            TimeSpan t;
            TimeSpan.TryParse(timeSlots[i].Id, out t);
            var schedTrip = DateTime.Now.Date.AddDays(1).Add(t);
            var reservations = ReservationBL.GetReservationCountByScheduleTrip(schedTrip);

            GenerateScheduleChildBox(schedTrip, reservations, schedBoxMod, schedFilter, routes, shortRoute);

            list.Add(schedBoxMod);
        }

        private static void GeneratePMScheduleBox(List<ScheduleBoxModel> list, List<TimeSlot> timeSlots, int i, ScheduleBoxModel schedBoxMod, IEnumerable<ScheduleBox> schedFilter)
        {
            List<ScheduleBoxChildModel> listChild = new List<ScheduleBoxChildModel>();

            schedBoxMod.Time = timeSlots[i].Name;
            schedBoxMod.Child = new List<ScheduleBoxChildModel>();

            string[] routes = new string[4];
            routes[0] = "Joya - North";
            routes[1] = "Joya - South";
            routes[2] = "Joya - East";
            routes[3] = "Joya - West";

            string[] shortRoute = new string[4];
            shortRoute[0] = "J - N";
            shortRoute[1] = "J - S";
            shortRoute[2] = "J - E";
            shortRoute[3] = "J - W";

            TimeSpan t;
            TimeSpan.TryParse(timeSlots[i].Id, out t);
            var schedTrip = DateTime.Now.Date.Add(t);
            var reservations = ReservationBL.GetReservationCountByScheduleTrip(schedTrip);

            GenerateScheduleChildBox(schedTrip, reservations, schedBoxMod, schedFilter, routes, shortRoute);

            list.Add(schedBoxMod);
        }

        private static void GenerateScheduleChildBox(DateTime schedTrip,
            List<Reservation> reservations,
            ScheduleBoxModel schedBoxMod,
            IEnumerable<ScheduleBox> schedFilter,
            string[] routes,
            string[] shortRoute)
        {

            for (int c = 0; c < routes.Count(); c++)
            {
                ScheduleBoxChildModel child = new ScheduleBoxChildModel();
                child.RouteName = shortRoute[c];
                child.SchedTime = schedTrip;
                child.OccupiedSeat = 0;
                child.ShuttleAssigned = 0;
                child.TotalSeat = 0;
                child.ResType = "ot";
                var locations = routes[c].Split('-');
                child.PickUpId = PlaceBL.Read().Where(p => p.Name.ToLower() == locations[0].TrimEnd(' ').ToLower()).FirstOrDefault().Id;
                child.DropOff = locations[1].TrimStart(' ').ToLower();
                child.DropOffId = LocationCodeBL.Read().Where(lc => lc.Name.ToLower().Equals(child.DropOff.ToLower())).FirstOrDefault().Id;

                var res = reservations.Where(r => r.PickUp + " - " + r.DropOff == routes[c]).ToList();
                var sched = schedFilter.Where(s => s.PickUp + " - " + s.DropOff == routes[c]).ToList();
                child.OccupiedSeat = (int)res.Sum(s => s.Seat);
                //child.Color = "gold";

                if (sched.Count() > 0)
                {
                    child.Id = sched.FirstOrDefault().Id;
                    child.ShuttleAssigned = sched.Select(s => s.Id).Count();
                    child.TotalSeat = sched.Select(s => s.SeatCount).Sum();
                }

                if (child.OccupiedSeat > child.TotalSeat)
                {
                    child.Color = "red";
                    schedBoxMod.Color = "red";
                }
                schedBoxMod.Child.Add(child);
            }
        }
    }
}