using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLibrary.BusinessLogic;
using DataLibrary.DataAccess;


namespace DataLibrary
{
    public partial class AppointmentForm : Form
    {
        static DateTime start;
        static DateTime end;
        static DateTime lowerLimit;
        static DateTime upperLimit;
        static DateTime apptStartTime;
        static DateTime apptEndTime;
        private int coachID;
        public int CoachID { get; set; }

        public AppointmentForm()
        {
            
            InitializeComponent();
            dtpEnd.Value = dTPStart.Value;
            dTPStartTime.Value = dTPStart.Value;
            dTPEndTime.Value = dTPStart.Value;
            this.btnDone.Click += (object sender, EventArgs e) =>
            {
                this.Hide();
                Schedule schedulingScreen = new Schedule();
                schedulingScreen.Show();
            }; // Lambda expression to create Done button click event
        }

        // Save appointment
        private void btnSaveApt_Click(object sender, EventArgs e)
        {
            // Convert times for validations
            string athleteName = cbCustName.Text.ToString();
            lowerLimit = Convert.ToDateTime((dTPStart.Value.ToString("yyyy-MM-dd") + (" 09:00")));
            upperLimit = Convert.ToDateTime((dTPStart.Value.ToString("yyyy-MM-dd") + (" 17:00")));
            apptStartTime = Convert.ToDateTime(dTPStart.Value.ToString("yyyy-MM-dd") + dTPStartTime.Value.ToString(" HH:mm"));
            apptEndTime = Convert.ToDateTime(dTPStart.Value.ToString("yyyy-MM-dd") + dTPEndTime.Value.ToString(" HH:mm"));

            try
            {
                //Validate times
                if ((BusinessHrsValidation()) && (FutureAppointmentValidation()) && (MeetingTimeValidation()))
                {
                    int AthleteID;
                    CoachID = coachID;

                    
                    apptStartTime = TimeZoneInfo.ConvertTimeToUtc(dTPStartTime.Value, TimeZoneInfo.Local);
                    apptEndTime = TimeZoneInfo.ConvertTimeToUtc(dTPEndTime.Value, TimeZoneInfo.Local);
                    string apptStart = dTPStart.Value.ToString("yyyy-MM-dd") + apptStartTime.ToString(" HH:mm");
                    string apptEnd = dTPStart.Value.ToString("yyyy-MM-dd") + apptEndTime.ToString(" HH:mm");
                    DateTime t1 = Convert.ToDateTime(apptStart);
                    DateTime t2 = Convert.ToDateTime(apptEnd);

                    string apptStartTimeTemp = dTPStartTime.Value.ToString(" HH:mm");
                    string apptEndTimeTemp = dTPEndTime.Value.ToString(" HH:mm");

                    // Get coachID by name
                    coachID =  CoachProcessor.GetCoachIDByName(cbUsr.Text);

                    start = Convert.ToDateTime(dTPStart.Value.ToString("yyyy-MM-dd") + dTPStartTime.Value.ToString(" HH:mm"));
                    end = Convert.ToDateTime(dTPStart.Value.ToString("yyyy-MM-dd") + dTPEndTime.Value.ToString(" HH:mm"));

                    ChangeAID();

                    //Check Coach appointmnets
                    string mtgStart;
                    string mtgEnd;
                    bool CoachHasAppointment;
                    (CoachHasAppointment, mtgStart, mtgEnd) = MySQLConnector.CheckCoachAppointmentTimesData(Convert.ToInt32(lblApptID.Text), coachID, start);
                    if (CoachHasAppointment)
                    {
                        char.ToUpper(cbUsr.Text[0]);
                        MessageBox.Show($"Coach {cbUsr.Text} already has a meeting from {mtgStart} to {mtgEnd} on That Day!");
                        //goto done;
                        return;
                    }
                    else
                    {
                        //Get AthleteID by name
                        AthleteID = MySQLConnector.GetAthleteIDDataByName(athleteName);

                        // Insert Appointment
                        MySQLConnector.SaveAppointmentData(Convert.ToInt32(lblApptID.Text), AthleteID, coachID, cbType.Text, t1, t2);

                        MessageBox.Show("Appointment Saved!");
                        Schedule schedulingScreen = new Schedule();
                        schedulingScreen.Show();
                        this.Hide();
                    }
                    //done:;
                }                  
         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        // Conditions for save button to be enabled
        private bool AllowSave()
        {
            if (((string.IsNullOrEmpty(cbCustName.Text)) || (string.IsNullOrEmpty(cbType.Text)) || (string.IsNullOrEmpty(cbUsr.Text))))
            {
                return false;
            }
            return true;
        }

        // Check if save button is enabled
        private void cbUsr_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnSaveApt.Enabled = AllowSave();
        }

        // Check if save button is enabled
        private void dTPStart_ValueChanged(object sender, EventArgs e)
        {
            dtpEnd.Value = dTPStart.Value;
            dTPStartTime.Value =dTPStart.Value;
            dTPEndTime.Value = dTPStart.Value;
        }

        // Check if save button is enabled
        private void cbCustName_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnSaveApt.Enabled = AllowSave();
        }

        // Validate the meeting starts befor ending
        private bool MeetingTimeValidation()
        {
            bool test = true;
            if (dTPStartTime.Value >= dTPEndTime.Value)
            {
                MessageBox.Show("Meeting start must be before meeting end!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                test = false;
            }
            return test;
            
        }

        // Validate new appointment is in the future
        private bool FutureAppointmentValidation()
        {
            DateTime apptStartTime = Convert.ToDateTime(dTPStart.Value.ToString("yyyy-MM-dd") + dTPStartTime.Value.ToString(" HH:mm"));
            bool test = true;
            if ((lblApptID.Text == "Auto Generated") && (apptStartTime <= DateTime.Now))
            {
                MessageBox.Show("New Appointments Must be Scheduled with a future Time/Date!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                test = false;
            }
            btnSaveApt.Enabled = AllowSave();
            return test;
        }

        // Validate appointment is within business hours
        private bool BusinessHrsValidation()
        {
            bool test = true;
            if ((((dTPStart.Value.DayOfWeek == DayOfWeek.Saturday) || (dtpEnd.Value.DayOfWeek == DayOfWeek.Saturday)) ||
                    ((dTPStart.Value.DayOfWeek == DayOfWeek.Sunday) || (dtpEnd.Value.DayOfWeek == DayOfWeek.Sunday))) ||
                    ((apptStartTime < lowerLimit) || (apptEndTime > upperLimit)))
            {

                MessageBox.Show("Out of Business Hrs, Please select a different time!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                test = false;
            }
            btnSaveApt.Enabled = AllowSave();
            return test;
        }

        // Handle type combobox textchange
        private void cbType_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbType.Text))
            {
                errorProvider1.SetError(cbType, "Please select Appointment Type!");
            }
            else
            {
                errorProvider1.SetError(cbType, null);
            }
            btnSaveApt.Enabled = AllowSave();
        }

        // Validate user combobox
        private void cbUsr_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbUsr.Text))
            {
                e.Cancel = true;
                cbUsr.Focus();
                errorProvider1.SetError(cbUsr, "Please select Consultant!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cbUsr, null);
            }
        }

        // Change Appointment ID label text
        private void ChangeAID()
        {
            if (lblApptID.Text == "0")
            {
                lblApptID.Text = "0";
            }
        }
        private void dTPStartTime_ValueChanged(object sender, EventArgs e)
        {
            dTPEndTime.Value = dTPStartTime.Value.AddHours(1);
        }

    }
}
