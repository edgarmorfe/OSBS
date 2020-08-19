using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Reservation : IReservation
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string PickUp { get; set; }
        public int PickUpId { get; set; }
        public string DropOff { get; set; }
        public int DropOffId { get; set; }
        public DateTime ScheduledTrip { get; set; }
        public int? Seat{ get; set; }
        public int? ShuttleId { get; set; }
        public int? DriverId { get; set; }
        public int? CreatedBy { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int SchedId { get; set; }
        public int SsdId { get; set; }
        public string SSDStatus { get; set; }

        public string ResType { get; set; }
        public string DeclineComment { get; set; }
        public bool VIP { get; set; }

        public int RoundTripId { get; set; }
    }
}
