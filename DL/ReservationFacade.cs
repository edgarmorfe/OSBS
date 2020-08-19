using Dapper;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public static class ReservationFacade
    {
        public static List<Reservation> Read()
        {
            var reservations = DataAccessHelper.LoadData<Reservation>("spGetAllReservationToday");
            return new List<Reservation>(reservations);
        }

        public static int Add(IReservation reservation)
        {
            DynamicParameters dParams = GenerateDynamicParameters(reservation);
            return DataAccessHelper.SaveDataWithReturn("spInsertReservation", dParams);
        }

        public static List<Reservation> GetReservationCountByScheduleTrip(DateTime scheduledTrip)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@scheduledTrip", scheduledTrip);
            return DataAccessHelper.LoadData<Reservation>("spGetReservationCountByScheduleTrip", dParams);
        }

        public static List<Reservation> GetBusinessReservationCountByScheduleTrip(DateTime scheduledTrip)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@scheduledTrip", scheduledTrip);
            return DataAccessHelper.LoadData<Reservation>("spGetBusinessReservationCountByScheduleTrip", dParams);
        }

        public static List<Reservation> GetReservationByEmpIdAndResDate(string empId, DateTime reservationDate)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@empId", empId);
            dParams.Add("@resDate", reservationDate);
            return DataAccessHelper.LoadData<Reservation>("spGetReservationByEmpIdAndResDate", dParams);
        }

        public static List<Reservation> GetReservationByEmpIdToday(string empId, DateTime reservationDate)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@empId", empId);
            dParams.Add("@resDate", reservationDate);
            return DataAccessHelper.LoadData<Reservation>("spGetReservationByEmpIdToday", dParams);
        }

        public static int TotalReservationByShuttle(int shuttleId, DateTime resDate)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@shuttleId", shuttleId, dbType: System.Data.DbType.Int32);
            dParams.Add("@reservationdate", resDate, dbType: System.Data.DbType.DateTime);
            dParams.Add("@totalCount", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            return DataAccessHelper.ReturnInt("spCheckAllAvailableReservation", dParams).Get<int>("@totalCount");
        }

        public static List<Reservation> GetReservationByScheduledTrip(DateTime scheduledTrip)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@scheduledTrip", scheduledTrip);
            return DataAccessHelper.LoadData<Reservation>("spGetReservationByScheduledTrip", dParams);
        }

        public static List<Reservation> GetAllReservationsByScheduleId(int schedId)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@schedId", schedId);
            return DataAccessHelper.LoadData<Reservation>("spGetAllReservationsByScheduleId", dParam);
        }

        public static List<Reservation> GetPassengerByDriverId(int driverId)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@driverId", driverId);
            return DataAccessHelper.LoadData<Reservation>("spGetPassengerByDriverId", dParam);
        }

        public static List<Reservation> GetPassengerTodayByDriverId(int driverId)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@driverId", driverId);
            return DataAccessHelper.LoadData<Reservation>("spGetPassengerTodayByDriverId", dParam);
        }

        public static List<Reservation> GetReservationById(int id)
        {
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@id", id);
            return DataAccessHelper.LoadData<Reservation>("spGetReservationById", dParam);
        }

        public static List<Reservation> GetAllReservationsByScheduledTripPickUpDropOffAndStatus(DateTime schedTrip,
            int pickUpId,
            string dropOff,
            string status)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@scheduledTrip", schedTrip);
            dParams.Add("@pickUpId", pickUpId);
            dParams.Add("@dropOff", dropOff);
            dParams.Add("@status", status);
            return DataAccessHelper.LoadData<Reservation>("spGetAllReservationsByScheduledTripPickUpDropOffAndStatus", dParams);
        }

        public static List<Reservation> GetBusinessReservationsByScheduledTripPickUpDropOffAndStatus(DateTime schedTrip,
        int pickUpId,
        string dropOff,
        string status)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@scheduledTrip", schedTrip);
            dParams.Add("@pickUpId", pickUpId);
            dParams.Add("@dropOff", dropOff);
            dParams.Add("@status", status);
            return DataAccessHelper.LoadData<Reservation>("spGetBusinessReservationsByScheduledTripPickUpDropOffAndStatus", dParams);
        }

        public static int Update(IReservation reservation)
        {
            DynamicParameters dParams = GenerateUpdateParameters(reservation);
            dParams.Add("@id", reservation.Id);
            return DataAccessHelper.SaveData<IDriver>("spUpdateReservation", dParams);
        }

        public static int Cancel(int id)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@id", id);
            dParams.Add("@status", "Cancel");
            return DataAccessHelper.SaveData("spCancelReservationById", dParams);
        }

        public static int OnBoard(int id)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@id", id);
            dParams.Add("@status", "OnBoard");
            return DataAccessHelper.SaveData("spCancelReservationById", dParams);
        }

        public static int OffBoard(int id)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@id", id);
            dParams.Add("@status", "Assigned");
            return DataAccessHelper.SaveData("spCancelReservationById", dParams);
        }

        public static int NoShow(int id)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@id", id);
            dParams.Add("@status", "NoShow");
            return DataAccessHelper.SaveData("spCancelReservationById", dParams);
        }

        public static int UpdateStatusReservationById(int id, string status, string message)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@id", id);
            dParams.Add("@status", status);
            dParams.Add("@declineComment", message);
            return DataAccessHelper.SaveData("spUpdateStatusReservationById", dParams);
        }

        public static int UpdateRundTripById(int id, int roundTripId)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@id", id);
            dParams.Add("@roundTripId", roundTripId);
            return DataAccessHelper.SaveData("spUpdateRoundTripById", dParams);
        }

        public static List<Reservation> GetOTReservationBySchedDatePickUpAndDropOff(DateTime schedDate,
            int pickUp,
            int dropOff)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@scheduledTrip", schedDate);
            dParams.Add("@pickUp", pickUp);
            dParams.Add("@dropOff", dropOff);
            return DataAccessHelper.LoadData<Reservation>("spGetReservationBySchedDatePickUpAndDropOff", dParams);
        }

        public static List<Reservation> GetBusinessReservationBySchedDatePickUpAndDropOff(DateTime schedDate,
            int pickUp,
            int dropOff)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@scheduledTrip", schedDate);
            dParams.Add("@pickUp", pickUp);
            dParams.Add("@dropOff", dropOff);
            return DataAccessHelper.LoadData<Reservation>("spGetBusinessReservationBySchedDatePickUpAndDropOff", dParams);
        }

        public static List<Reservation> GetReservationByEmpId(string empId)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@empId", empId);
            return DataAccessHelper.LoadData<Reservation>("spGetReservationByEmpId", dParam);
        }

        public static List<Reservation> GetReservationHistoryByEmpId(string empId)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@empId", empId);
            return DataAccessHelper.LoadData<Reservation>("spGetReservationHistoryByEmpId", dParam);
        }


        private static DynamicParameters GenerateUpdateParameters(IReservation reserve)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@empId", reserve.EmpId);
            dParams.Add("@pickUp", reserve.PickUp);
            dParams.Add("@dropOff", reserve.DropOff);
            dParams.Add("@reason", reserve.Reason);
            dParams.Add("@scheduledTrip", reserve.ScheduledTrip);
            return dParams;
        }

        private static DynamicParameters GenerateDynamicParameters(IReservation reserve)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@empId", reserve.EmpId);
            dParams.Add("@pickUp", reserve.PickUp);
            dParams.Add("@dropOff", reserve.DropOff);
            dParams.Add("@reason", reserve.Reason);
            dParams.Add("@scheduledTrip", reserve.ScheduledTrip);
            dParams.Add("@status", reserve.Status);
            dParams.Add("@SchedId", reserve.SchedId);
            dParams.Add("@seat", reserve.Seat);
            dParams.Add("@resType", reserve.ResType);
            if (reserve.SsdId != 0)
                dParams.Add("@SsdId", reserve.SsdId);
            dParams.Add("@vip", reserve.VIP);
            return dParams;
        }
    }
}
