using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class OTReservationViewModel
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string PickUp { get; set; }
        public string DropOff { get; set; }
        public DateTime ScheduledTrip { get; set; }
        public int? Seat { get; set; }
        public int? ShuttleId { get; set; }
        public int? DriverId { get; set; }
        public int SSDID { get; set; }
        public string SSDStatus { get; set; }
    }
}