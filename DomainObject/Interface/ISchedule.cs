using System;

namespace DomainObject
{
    public interface ISchedule
    {
        int DriverId { get; set; }
        int DropOff { get; set; }
        int Id { get; set; }
        int PickUp { get; set; }
        DateTime ScheduledTrip { get; set; }
        int ShuttleId { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        string ShuStatus { get; set; }
    }
}