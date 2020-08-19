using DL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class ReservationBL
    {
        public static int Book(int id,
            string empId,
            string pickUp,
            string dropOff,
            DateTime scheduledTrip,
            string reason,
            string status,
            int seat,
            int schedId,
            int SSDId,
            string resType,
            bool vip)
        {
            IReservation reserve = new Reservation()
            {
                Id = id,
                EmpId = empId,
                PickUp = pickUp,
                DropOff = dropOff,
                ScheduledTrip = scheduledTrip,
                Reason = reason,
                Status = status,
                SchedId = schedId,
                SsdId = SSDId,
                Seat = seat,
                ResType = resType,
                VIP = vip
            };

            if (id == -1)
                return ReservationFacade.Add(reserve);
            else
                return ReservationFacade.Update(reserve);
        }

        public static List<Reservation> GetAllReservationsByScheduleId(int schedId)
        {
            return ReservationFacade.GetAllReservationsByScheduleId(schedId);
        }

        public static List<Reservation> GetPassengerByDriverId(int driverId)
        {
            return ReservationFacade.GetPassengerByDriverId(driverId);
        }

        public static List<Reservation> GetPassengerTodayByDriverId(int driverId)
        {
            return ReservationFacade.GetPassengerTodayByDriverId(driverId);
        }

        public static List<Reservation> GetReservationById(int id)
        {
            return ReservationFacade.GetReservationById(id);
        }

        public static List<Reservation> GetReservationCountByScheduleTrip(DateTime scheduledTrip)
        {
            return ReservationFacade.GetReservationCountByScheduleTrip(scheduledTrip);
        }

        public static List<Reservation> GetBusinessReservationCountByScheduleTrip(DateTime scheduledTrip)
        {
            return ReservationFacade.GetBusinessReservationCountByScheduleTrip(scheduledTrip);
        }
        public static List<Reservation> GetReservationByEmpIdAndResDate(string empId, DateTime reservationDate)
        {
            return ReservationFacade.GetReservationByEmpIdAndResDate(empId, reservationDate);
        }

        public static List<Reservation> GetReservationByEmpIdToday(string empId, DateTime reservationDate)
        {
            return ReservationFacade.GetReservationByEmpIdToday(empId, reservationDate);
        }

        public static List<Reservation> GetAllReservationsByScheduledTripPickUpDropOffAndStatus(DateTime schedTrip,
            int pickUpId,
            string dropOff,
            string status)
        {
            return ReservationFacade.GetAllReservationsByScheduledTripPickUpDropOffAndStatus(schedTrip,
                pickUpId,
                dropOff,
                status);
        }

        public static List<Reservation> GetBusinessReservationsByScheduledTripPickUpDropOffAndStatus(DateTime schedTrip,
            int pickUpId,
            string dropOff,
            string status)
        {
            return ReservationFacade.GetBusinessReservationsByScheduledTripPickUpDropOffAndStatus(schedTrip, pickUpId, dropOff, status);
        }

        public static int Cancel(int id)
        {
            return ReservationFacade.Cancel(id);
        }
        public static int OnBoard(int id)
        {
            return ReservationFacade.OnBoard(id);
        }

        public static int OffBoard(int id)
        {
            return ReservationFacade.OffBoard(id);
        }

        public static int NoShow(int id)
        {
            return ReservationFacade.NoShow(id);
        }

        public static int UpdateStatusReservationById(int id, string status, string message)
        {
            return ReservationFacade.UpdateStatusReservationById(id, status, message);
        }

        public static int UpdateRundTripById(int id, int roundTripId)
        {
            return ReservationFacade.UpdateRundTripById(id, roundTripId);
        }

        public static List<Reservation> GetReservationBySchedDatePickUpAndDropOff(DateTime schedDate,
        int pickUp,
        int dropOff)
        {
            return ReservationFacade.GetOTReservationBySchedDatePickUpAndDropOff(schedDate, pickUp, dropOff);
        }

        public static List<Reservation> GetBusinessReservationBySchedDatePickUpAndDropOff(DateTime schedDate,
            int pickUp,
            int dropOff)
        {
            return ReservationFacade.GetBusinessReservationBySchedDatePickUpAndDropOff(schedDate, pickUp, dropOff);
        }

        public static List<Reservation> GetReservationByEmpId(string empId)
        {
            return ReservationFacade.GetReservationByEmpId(empId);
        }

        public static List<Reservation> GetReservationHistoryByEmpId(string empId)
        {
            return ReservationFacade.GetReservationHistoryByEmpId(empId);
        }

        public static List<Reservation> Read()
        {
            return ReservationFacade.Read();
        }
    }
}
