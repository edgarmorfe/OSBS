using System;

namespace DomainObject
{
    public interface IReservation
    {
        int? CreatedBy { get; set; }
        int? DriverId { get; set; }
        string DropOff { get; set; }
        string EmpId { get; set; }
        int Id { get; set; }
        string PickUp { get; set; }
        DateTime ScheduledTrip { get; set; }
        int? Seat { get; set; }
        int? ShuttleId { get; set; }
        string Reason { get; set; }
        string Status { get; set; }
        int SchedId { get; set; }
        int PickUpId { get; set; }
        int DropOffId { get; set; }
        int SsdId { get; set; }
        string SSDStatus { get; set; }
        string ResType { get; set; }
        string DeclineComment { get; set; }
        bool VIP { get; set; }
        int RoundTripId { get; set; }
    }
}