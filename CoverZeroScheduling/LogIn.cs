using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using DataLibrary.BusinessLogic;
using DataLibrary.Models;

namespace DataLibrary
{
    public partial class LogIn : Form
    {
        public static string CurrentCoachName { get; set; }
        public static int CurrentCoachID { get; set; }
        private static string currentCoachName;
        private static int currentCoachID;


        public LogIn()
        {
            InitializeComponent();
            CultureInfo ci = CultureInfo.CurrentUICulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{CultureInfo.CurrentCulture.ToString()}");
            SetCulture();
            txtPassword.Text = "********";
        }


        private void txtUserEnter(object sender, EventArgs e)
        {
            // Set user textbox to blank
            LogIn.ActiveForm.Text = Properties.Resources.LogIn;
            if (txtUsername.Text.Equals($"{Properties.Resources.txtUsername}"))
            {
                txtUsername.Text = ("");
            }
        }

        private void txtPasswordEnter(object sender, EventArgs e)
        {
            // Set password textbox to blank
            if (txtPassword.Text.Equals(@"********"))
            {
                txtPassword.Text = ("");
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate 
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    Coach CurrentCoach = CoachProcessor.Login(txtUsername.Text.ToUpper(), txtPassword.Text);
                    if (CurrentCoach != null)
                    {                        
                        CurrentCoachID = CurrentCoach.CoachID;
                        CurrentCoachName = CurrentCoach.UserName;

                        // Open Scheduling screen
                        Schedule scheduling = new Schedule();
                        scheduling.coachlbl.Text = CurrentCoachName;
                        scheduling.currentCoachID = CurrentCoach.CoachID;
                        this.Hide();
                        scheduling.Show();

                        // Log successful login
                        using (StreamWriter w = File.AppendText("log.txt"))
                        {
                            LogEntry($"{CurrentCoachName} Logged in successfully", w);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login Credentials");
                    }

                }
                else// Throw exception
                {
                    throw (new UserPasswordException(string.Format($"{Properties.Resources.loginError}")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        // Set labels, buttons, and text boxes based on culture
        private void SetCulture()
        {
            txtUsername.Text = Properties.Resources.txtUsername;
            lblScheduling.Text = Properties.Resources.lblScheduling;
            lblLogIn.Text = Properties.Resources.LogIn;
            btnLogIn.Text = Properties.Resources.LogIn;
            btnCancel.Text = Properties.Resources.Cancel;
            lbl_newUser.Text = Properties.Resources.newUser;
            linklbl_register.Text = Properties.Resources.register;
        }

        // Paint panel
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            LogIn.ActiveForm.Text = Properties.Resources.LogIn;
        }

        // Open Register screen
        private void linklbl_register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register userScreen = new Register();
            userScreen.Show();
        }

        // Close app with cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        // Set Current user ID
        internal static int GetCoachID()
        {
            int coachID = CurrentCoachID;
            return coachID;
        }

        // Set Current user Name
        internal static string GetCoachName()
        {
            string userName = CurrentCoachName;
            return userName;
        }

        // Validate user name textbox is not empty
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                e.Cancel = true;
                //txtUsername.Focus();
                errorProvider1.SetError(txtUsername, "Please enter your Username!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUsername, null);
            }

        }

        // Log when user logs in - in log.txt
        public static void LogEntry(string entry, TextWriter log)
        {
            log.Write("\r\nLog Entry : ");
            log.WriteLine($"");
            log.WriteLine($"  : Coach {entry} on {DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}.");
            log.WriteLine("*************************************************");
        }

        // Enable or disable login button
        private bool AllowLogin()
        {
            if (((string.IsNullOrEmpty(txtUsername.Text)) || (string.IsNullOrEmpty(txtPassword.Text))) ||
                (txtUsername.Text == $"{Properties.Resources.txtUsername}"))
            {
                btnLogIn.BackColor = Color.SlateGray;
                return false;
            }
            btnLogIn.BackColor = Color.Blue;
            return true;

        }

        // User defined exception for unmatched username and password
        public class UserPasswordException : Exception
        {
            public UserPasswordException(string message) : base(message) { }
        }

        // Handle password textbox text change
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, $"{Properties.Resources.noPassword}");
            }
            else
            {

                errorProvider1.SetError(txtPassword, null);
            }
            btnLogIn.Enabled = AllowLogin();
        }

        // handle username textbox text change
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, $"{Properties.Resources.noUser}");
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            }
            btnLogIn.Enabled = AllowLogin();
            txtPassword.Text = ("");
        }

    }
}
