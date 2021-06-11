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
using System.Configuration;
using DataLibrary.BusinessLogic;

namespace DataLibrary
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

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
            if ((txtUsername.Text != "" && txtUsername.Text != "Username") &&
                (txtPassword.Text != "" && txtPassword.Text != "********"))
            {
                try
                {
                    string coachUserName = txtUsername.Text;
                    string coachPassword = txtPassword.Text;

                    CoachProcesser.AddCoach(coachUserName, coachPassword);
                    LogIn logIn = new LogIn();
                    MessageBox.Show($"Registrations Complete, Welcom2 to CoverZero {coachPassword}!");
                    this.Hide();
                    logIn.Show();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Coach Already Exists \n {ex}");
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
