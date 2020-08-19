using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class OTReservationNewModel
    {
        public int Id { get; set; }
        [Display(Name = "Reservation Date")]
        [Required(ErrorMessage = "You must provide a reservation date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime ReservationDate { get; set; }
        [Required(ErrorMessage = "You must provide a time slot")]
        public string TimeSlotSelectedId { get; set; }
        [Display(Name = "Time Slot")]
        public List<TimeSlot> TimeSlots { get; set; }
        [Required(ErrorMessage = "You must provide a pick up")]
        public string PickUpSelectedId { get; set; }
        [Display(Name = "Pick Up Location")]
        public List<Location> PickUps { get; set; }

        [Required(ErrorMessage = "You must provide a drop off")]
        public string DropOffSelectedId { get; set; }

        [Display(Name = "Drop Off Location")]
        public List<Location> DropOffs { get; set; }

        [Display(Name = "Remarks to Driver")]
        public string Comment { get; set; }
        public string Status { get; set; }
        public List<BookingModel> OTBooking {get; set;}
        public List<BookingModel> BusinessBooking { get; set; }
        public BusinessBookModel BusinessBook { get; set; }
        public string BookingType{ get; set; }
    }

    public class BookingModel
    {
        public int ReserveId { get; set; }
        public string TimeSlot { get; set; }
        public string Route { get; set; }
        public string PlateNo { get; set; }
        public string DriverName { get; set; }
        public string Status { get; set; }
    }

    public class BusinessBookModel
    {
        public int Id { get; set; }

        [Display(Name ="Round-Trip")]
        public string TripType { get; set; }

        [Display (Name ="VIP")]
        public bool VIP { get; set; }

        [Display(Name = "Pick-up Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime PickUpDate { get; set; }

        [Display(Name = "Pick-up Time")]
        public string PickUpTime { get; set; }

        [Display(Name = "Return Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime ReturnPickUpDate { get; set; }

        [Display(Name = "Return Time")]
        public string ReturnPickUpTime { get; set; }

        public string PickUpSelectedId { get; set; }
        [Display(Name = "Pick Up Location")]
        public string PickUp { get; set; }

        [Display(Name = "Drop Off Location")]
        public string DropOff { get; set; }

        [Display(Name ="Number of Passengers")]
        public string Passenger { get; set; }

        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        public string Status { get; set; }
    }

    public class TimeSlot
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}