using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace CoverZeroScheduling
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        string conString = @"server=3.227.166.251;user id=U04cRO;password=53688204070;persistsecurityinfo=True;database=U04cRO";

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text.Equals("Username"))
            {
                txtUsername.Text = ("");
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text.Equals(@""))
            {
                txtUsername.Text = ("Username");
            }
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(@"********"))
            {
                txtPassword.Text = ("");
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(""))
            {
                txtPassword.Text = (@"********");
            }
        }
        private void btnSignup_Click(object sender, EventArgs e)
        {
            if ((txtUsername.Text != "" && txtUsername.Text != "Username") && txtPassword.Text != "")
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    try
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("sp_add_coach", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@un", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@pw", txtPassword.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registrations Complete");
                        LogIn logIn = new LogIn();
                        this.Hide();
                        logIn.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Coach Already Exists \n {ex}");
                    }

                }
            }
            else
            {
                MessageBox.Show("Please fill in manditory fields");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            LogIn logIn = new LogIn();
            logIn.Show();
        }
    }
}
