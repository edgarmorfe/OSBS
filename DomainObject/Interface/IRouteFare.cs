using System;

namespace DomainObject
{
    public interface IRouteFare
    {
        decimal Amount { get; set; }
        int DropOffId { get; set; }
        int Id { get; set; }
        int PickUpId { get; set; }
        DateTime Time { get; set; }
    }
}