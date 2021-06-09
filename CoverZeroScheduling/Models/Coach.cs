using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoverZeroScheduling
{
    public class Coach
    {
        [Display(Name ="CoachID")]
        public int CoachID { get; set; }
        public static void AddCoach(string userName, string password)
        {            
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            MySqlCommand cmd;

            using (con)
            {
                try
                {
                    con.Open();
                    cmd = new MySqlCommand("sp_add_coach", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@un", userName.Trim());
                    cmd.Parameters.AddWithValue("@pw", password.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registrations Complete");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Coach Already Exists \n {ex}");
                }
            }

        }
    }
}
