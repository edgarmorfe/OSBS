using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Helpers
{
    public class ConstantStringValue
    {
        public static string ScheduleShuttleDriverAssigned = "Assigned";
        public static string ScheduleShuttleDriverInRoute = "InRoute";
        public static string ScheduleShuttleDriverDropOff = "DropOff";
        public static string ScheduleShuttleDriverArrived = "Arrived";

        public static string ReservationStatusActive = "Active";
        public static string ReservationStatusNotAssigned = "NotAssigned";
        public static string ReservationStatusOnBoard = "OnBoard";
        public static string ReservationStatusDropOff = "DropOff";
    }
}