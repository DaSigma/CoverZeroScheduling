using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Safety:Athlete
    {
        public string SafetyPosition { get; set; }

        public Safety(int athleteID, string athleteName, int addressID, string safetyPosition) : base(athleteID, athleteName, addressID)
        {
            SafetyPosition = safetyPosition;
        }

        public static void Insert_UpdateSafety(int athleteID, string athleteName, string athletePostition, string AthleteDiscipline, int addressID)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            MySqlCommand cmd;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand("sp_addUpdateSafety", con);
                cmd.Parameters.Clear();
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
