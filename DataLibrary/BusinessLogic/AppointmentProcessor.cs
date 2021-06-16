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
    public class AppointmentProcessor
    {
        public static DataTable GetAppointmentByCoach(int coachID)
        {
            string sp = "sp_getApptsbyCoachID";
            DataTable dt = new DataTable();
            return dt = MySQLConnector.LoadAppointmentsDatabyCoach(sp, coachID);
        }

        public static DateTime GetUpcomingAppointment(int coachID)
        {
            string sp = "sp_getApptsbyCoachID";
            Appointment apt = new Appointment();
            apt = MySQLConnector.LoadUpcomingAppointmentData(sp, coachID);

            return apt.StartDate;
        }

        public static List<string> GetAppointmentTypes()
        {
            string sp = "sp_allAppTypes";
            List<string> appointmentTypes = new List<string>();

            appointmentTypes = MySQLConnector.LoadAppointmentsTypeData(sp);
            return appointmentTypes;
        }

        public static Appointment GetAppointmentByID(int appointmentID)
        {
            Appointment apt = new Appointment();
            string sp = "sp_viewAppts";

            apt = MySQLConnector.LoadAppointmentDataByID(appointmentID, sp);

            return apt;
        }

        public static void DeleteAppointment(int appointmentID)
        {
            string sp = "sp_apptDeletebyID";
            MySQLConnector.DeleteAppointmentData(appointmentID, sp);
        }

        public static StringBuilder GetAppointmentCount(string currentMonth, string startDate, string endDate)
        {
            var pg = new StringBuilder();
            List<Appointment> apts = new List<Appointment>();
            string sp = "sp_appointmentTypebyMonth";

            pg = MySQLConnector.LoadAppointmentCountData(currentMonth, startDate, endDate, sp);
            return pg;
        }

            
    }
}
