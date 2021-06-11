using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public class AppointmentProcesser
    {
        public static DataTable GetAppointmentByCoach(int coachID)
        {
            string sp = "sp_getApptsbyCoachID";
            DataTable dt = new DataTable();
            return dt = MySQLDataAccess.LoadAppointmentsDatabyCoach(sp, coachID);
        }

        public static DateTime GetUpcomingAppointment(int coachID)
        {
            string sp = "sp_getApptsbyCoachID";
            Appointment apt = new Appointment();
            apt = MySQLDataAccess.LoadUpcomingAppointmentData(sp, coachID);

            return apt.StartDate;
        }

        public static List<string> GetAppointmentTypes()
        {
            string sp = "sp_allAppTypes";
            List<string> appointmentTypes = new List<string>();

            appointmentTypes = MySQLDataAccess.LoadAppointmentsTypeData(sp);
            return appointmentTypes;
        }

        public static Appointment GetAppointmentByID(int appointmentID)
        {
            Appointment apt = new Appointment();
            string sp = "sp_viewAppts";

            apt = MySQLDataAccess.LoadAppointmentDataByID(appointmentID, sp);

            return apt;
        }

        public static void DeleteAppointment(int appointmentID)
        {
            string sp = "sp_apptDeletebyID";
            MySQLDataAccess.DeleteAppointmenData(appointmentID, sp);
        }
            
    }
}
