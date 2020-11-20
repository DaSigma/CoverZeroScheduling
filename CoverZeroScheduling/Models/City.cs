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
    class City
    {
        public static int GetCityID(string zip)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString); ;
            MySqlCommand cmd;
            MySqlDataReader dr;
            int cityID;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand("sp_getCityID", con);

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
    }
}
