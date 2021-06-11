using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.DataAccess
{

    public class MySQLDataAccess
    {
        public static string GetConnectionString(string connectionName = "mycon")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static void SaveCoach(string userName, string password)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            using (con)
            {
                con.Open();
                MySqlCommand cmd;
                cmd = new MySqlCommand("sp_add_coach", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@un", userName.Trim());
                cmd.Parameters.AddWithValue("@pw", password.Trim());
                cmd.ExecuteNonQuery();
            }
        }

        public static Coach Login(string userName, string password)
        {           
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            using (con)
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * FROM coach Where coachName = '" + userName.Trim() + "'";
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (userName.ToUpper().Equals(dr["coachName"].ToString().ToUpper()) && password.Equals(dr["password"]))
                    {
                        Coach coach = new Coach();
                        coach.CoachID = Convert.ToInt32(dr["coachID"]);
                        coach.UserName = dr["coachName"].ToString();
                        return coach;
                    }                    
                    else
                    {
                        return null;
                    }
                }
                return null;
            }

        }

        public static DataTable GetData(string sp, int coachID, string StartDate, string ToDate)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataAdapter da;
            DataTable dt;

            using (con)
            {
                con.Open();
                cmd = new MySqlCommand(sp, con);
                da = new MySqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CoachID", coachID);
                cmd.Parameters.AddWithValue("@startDate", StartDate);
                cmd.Parameters.AddWithValue("@endDate", ToDate);
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public static DataTable LoadAppointmentsDatabyCoach(string sp, int coachID)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataAdapter da;
            DataTable dt;
            using (con)
            {
                con.Open();
                cmd = new MySqlCommand(sp, con);
                da = new MySqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CoachID", coachID);
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                da.Fill(dt);
                //da.Update(dt);

                return dt;
            }
        }

        public static Appointment LoadUpcomingAppointmentData(string sp, int coachID)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            using (con)
            {
                con.Open();

                string startDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local).ToString("yyyy-MM-dd HH:mm");
                string endDate = Convert.ToDateTime(startDate).AddHours(1).ToString("yyyy-MM-dd HH:mm");

                cmd = new MySqlCommand(sp);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@CoachID", coachID);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                MySqlDataReader dr = cmd.ExecuteReader();

                // Show if there is an appointment within 15 minutes of current time. 
                if (dr.Read())
                {
                    Appointment apt = new Appointment();
                    return apt;
                    //Todo: fix
                }
                dr.Close();
                return null;
            }
        }
        public static List<string> LoadAthleteNameData(string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataReader dr;
            
            using (con)
            {
                List<string> athleteNames = new List<string>();
                cmd = new MySqlCommand(sp);
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    athleteNames.Add(dr["athleteName"].ToString());
                }
                dr.Close();
                return athleteNames;
            }

        }

        public static List<string> LoadCoachNamesData(string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataReader dr;

            // Fill appointment consultant combobox
            using (con)
            {
                List<string> coachNames = new List<string>();
                cmd = new MySqlCommand(sp);
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    coachNames.Add(dr["coachName"].ToString());
                }
                dr.Close();
                return coachNames;
            }
        }


        public static List<string> LoadAppointmentsTypeData(string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataReader dr;

            using (con)
            {
                List<string> appointmentTypes = new List<string>();
                //Fill appointment type combobox
                cmd = new MySqlCommand(sp);
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    appointmentTypes.Add(dr["type"].ToString());
                }
                dr.Close();
                return appointmentTypes;
            }
        }
        
        public static Appointment LoadAppointmentDataByID(int appointmentID, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataReader dr;
            using (con)
            {
                // Get appointment by appointmentID
                cmd = new MySqlCommand(sp);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@apptID", appointmentID);
                dr = cmd.ExecuteReader();

                Appointment apt = new Appointment();
                if (dr.Read())
                {
                    apt.AppointmentID = Convert.ToInt32(dr["appointmentID"]);
                    apt.AthleteName = dr["athleteName"].ToString();
                    apt.AthleteID = Convert.ToInt32(dr["athleteId"]);
                    apt.Type = dr["type"].ToString();
                    apt.StartDate = Convert.ToDateTime((dr["start"]));
                    apt.EndDate = Convert.ToDateTime(dr["end"]);
                    apt.Updated = Convert.ToDateTime(dr["lastUpdate"]).ToLocalTime();
                }
                dr.Close();
                return apt;
            }
        }

        public static void DeleteAppointmenData(int appointmentID, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            using (con)
            {
                MySqlCommand cmd;
                con.Open();
                cmd = new MySqlCommand(sp);
                cmd.Connection = con;
                cmd.CommandText = "sp_apptDeletebyID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@apptID", appointmentID);
                cmd.ExecuteNonQuery();
            }

        }


    }
}
