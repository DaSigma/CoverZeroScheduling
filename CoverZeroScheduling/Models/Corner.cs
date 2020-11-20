using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverZeroScheduling.Models
{
    public class Corner : Athlete
    {
        public string CornerPosition { get; set; }

        public Corner(int athleteID, string athleteName, int addressID, string cornerPosition) : base(athleteID, athleteName, addressID)
        {
            CornerPosition = cornerPosition;
        }

        public static void Insert_UpdateCorner(int athleteID, string athleteName, string athletePostition, string AthleteDiscipline, int addressID)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString); 
            MySqlCommand cmd;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand("sp_addUpdateCorner", con);
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AthlID", athleteID);
                cmd.Parameters.AddWithValue("@AthlName", athleteName);
                cmd.Parameters.AddWithValue("@AthlPosition", athletePostition);
                cmd.Parameters.AddWithValue("@Corner_Discipline", AthleteDiscipline);
                cmd.Parameters.AddWithValue("@addID", addressID);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
