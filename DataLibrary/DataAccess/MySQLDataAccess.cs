using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.BusinessLogic;
using System.Windows.Forms;

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
                con.Close();
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
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                da.Fill(dt);
                da.Update(dt);

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
                con.Open();
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
                con.Open();
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
                con.Open();
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
                con.Open();
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

        public static void DeleteAppointmentData(int appointmentID, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            using (con)
            {
                MySqlCommand cmd;
                con.Open();
                cmd = new MySqlCommand(sp);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@apptID", appointmentID);
                cmd.ExecuteNonQuery();
            }

        }
        
        public static List<string> LoadDistinctAddressZipData(string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            List<string> distinctZips = new List<string>();

            using (con)
            {
                con.Open();
                MySqlCommand cmd;
                cmd = new MySqlCommand(sp);
                cmd.Connection = con;
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    distinctZips.Add(dr["postalCode"].ToString());
                }
                dr.Close();
                return distinctZips;
            }
        }

        public static Athlete LoadAthleteDataByID(bool IsCorner, int athleteID, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataReader dr;
            
            using (con)
            {
                Athlete currentAthlete = new Athlete();
                string athleteDiscipline = AthleteProcessor.SetAthleteDiscipline(IsCorner);
                con.Open();
                cmd = new MySqlCommand(sp);
                cmd.Connection = con;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@AthlID", athleteID);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    currentAthlete.AthleteID = Convert.ToInt32(dr["athleteId"]);
                    currentAthlete.AthleteName = dr["athleteName"].ToString();
                    currentAthlete.AthletePosition = dr["athletePosition"].ToString();
                    currentAthlete.AthleteDiscipline = dr[athleteDiscipline].ToString();
                    currentAthlete.Phone = dr["phone"].ToString();
                    currentAthlete.StreetAddress = dr["address"].ToString();
                    currentAthlete.City = dr["city"].ToString();
                    currentAthlete.Country = dr["country"].ToString();
                    currentAthlete.PostalCode = dr["postalCode"].ToString();
                    currentAthlete.AddressID = Convert.ToInt32(dr["addressId"]);
                    currentAthlete.Updated = Convert.ToDateTime(dr["lastUpdate"]);
                    currentAthlete.ImageURL = dr["imgURL"].ToString();
                }
                con.Close();
                return currentAthlete;
            }
        }

        public static void DeleteAthleteDataByID(int athleteID, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;

            // Delete Athlete
            using (con)
            {
                if (true)
                {
                    cmd = new MySqlCommand(sp);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Athlete_ID", athleteID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteAddressDataByID(int addressID, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            using (con)
            {
                if (true)
                {
                    cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Address_ID", addressID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool CheckAthleteAssociationData(int athleteID)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataReader dr;

            using (con)
            {
                con.Open();
                cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "sp_distinctApptAthleteID";
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    if (athleteID.Equals(Convert.ToInt32((dr["athleteId"]))))
                    {
                        return true;
                    }
                    
                }
                dr.Close();
                return false;
            }
        }

        public static DataTable LoadAllAthletesData(string sp)
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
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                da.Fill(dt);
                da.Update(dt);
            }
            return dt;
        }

        public static List<Appointment> LoadAppointmentCountData(string startDate, string endDate, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            Appointment apt = new Appointment();
            List<Appointment> newlist = new List<Appointment>();
            
            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    apt.Type = dr["type"].ToString();
                    apt.Count_Type = Convert.ToInt32(dr["Count(type)"]);

                    newlist.Add(apt);
                }
            }
            return newlist;
           
        }

        public static StringBuilder LoadCoachScheduleData(string coachName, string sp)
        {

            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CoachName", coachName);

                MySqlDataReader dr = cmd.ExecuteReader();
                var pg = new StringBuilder();
                pg.Append($"\t\t\t All appointments for Coach {coachName} \n\n");
                pg.Append(String.Format("{0,-15}\t{1,-15}\t{2,-25}\t\t{3,-25}\n", "Coach", "Type", "Start", "End"));
                pg.Append($"{string.Concat(Enumerable.Repeat("*", 100))} \n");
                while (dr.Read())
                {
                    pg.AppendFormat(String.Format("{0,-15}\t{1,-15}\t{2,-25}\t{3,-25} \n", dr["coachName"], dr["type"],
                       DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime(dr["start"])), DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime(dr["end"]))));
                }
                return pg;
            }
        }

        public static StringBuilder LoadAllCoachesAppointmentData(string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataReader dr = cmd.ExecuteReader();

                // Initilize stringbuilder
                var pg = new StringBuilder();
                pg.Append($"\t\t\t All appointments for all Coaches \n\n");// Report Title
                pg.Append(String.Format("{0,-15}\t{1,-15}\t{2,-25}\t\t{3,-25}\n", "Coach", "Type", "Start", "End"));// Report Header
                pg.Append($"{string.Concat(Enumerable.Repeat("*", 100))} \n");
                while (dr.Read())
                {
                    pg.AppendFormat(String.Format("{0,-15}\t{1,-15}\t{2,-25}\t{3,-25} \n", dr["coachName"], dr["type"],
                       DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime(dr["start"])), DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime(dr["end"]))));
                }
                return pg;
            }
        }

        public static StringBuilder LoadDBReportData(string sp, string athleteDiscipline)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataReader dr = cmd.ExecuteReader();

                // Initilize stringbuilder
                var pg = new StringBuilder();
                pg.Append($"\t\t\t Defensive Backs by Position \n\n");// Report Title
                pg.Append(String.Format("{0,-35}\t{1,-25}\t{2,-25}\t\n", "       DB       ", "Discipline", "Position"));// Report Header
                pg.Append($"{string.Concat(Enumerable.Repeat("*", 100))} \n");
                while (dr.Read())
                {
                    pg.AppendFormat(String.Format("{0,-30}\t{1,-25}\t{2,-25} \n", dr["athleteName"], dr[athleteDiscipline],
                        dr["athletePosition"]));
                }
                return pg;
            }
        }

        public static void Insert_UpdateAddressData(int addressID, string street, int cityID, string zip, string phoneNumber, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@address_ID", addressID);
                cmd.Parameters.AddWithValue("@address1", street);
                cmd.Parameters.AddWithValue("@city_ID", cityID);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                cmd.ExecuteNonQuery();
            }
        }

        // Get addressID with cityID and zip
        public static int GetAddressIDData(int cityID, string zip, string street, string phoneNumber, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataReader dr;
            int addressID;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@city_ID", cityID);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@AthleteAddress", street);
                cmd.Parameters.AddWithValue("@AthletePhone", phoneNumber);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    addressID = Convert.ToInt32(dr["addressId"]);
                    return addressID;
                }
                dr.Close();
            }
            return 0;
        }

        public static int GetCityIDData(string zip, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;
            MySqlDataReader dr;
            int cityID;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@zip", zip);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cityID = Convert.ToInt32(dr["cityId"]);
                    return cityID;
                }
                dr.Close();
            }
            return 0;
        }

        public static void Insert_UpdateCornerData(int athleteID, string athleteName, string athletePostition, string AthleteDiscipline, int addressID, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);
                //cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AthlID", athleteID);
                cmd.Parameters.AddWithValue("@AthlName", athleteName);
                cmd.Parameters.AddWithValue("@AthlPosition", athletePostition);
                cmd.Parameters.AddWithValue("@Corner_Discipline", AthleteDiscipline);
                cmd.Parameters.AddWithValue("@addID", addressID);
                cmd.ExecuteNonQuery();
            }
        }
        public static void Insert_UpdateSafetyData(int athleteID, string athleteName, string athletePostition, string AthleteDiscipline, int addressID, string sp)
        {
            MySqlConnection con = new MySqlConnection(GetConnectionString());
            MySqlCommand cmd;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand(sp, con);
                //cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AthlID", athleteID);
                cmd.Parameters.AddWithValue("@AthlName", athleteName);
                cmd.Parameters.AddWithValue("@AthlPosition", athletePostition);
                cmd.Parameters.AddWithValue("@Safety_Discipline", AthleteDiscipline);
                cmd.Parameters.AddWithValue("@addID", addressID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
