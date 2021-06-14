using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLibrary.BusinessLogic;
using DataLibrary.Models;

namespace DataLibrary
{
    public partial class Schedule : Form
    {
        public static int currentApptID;
        //MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
        //MySqlCommand cmd = new MySqlCommand();
        //MySqlDataAdapter da;
        //DataTable dt;
        //MySqlDataReader dr;
        string sp_AthleteByID;
        string athleteDiscipline;
        public string StartDate { get; set; }
        public string ToDate { get; set; }        
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public int DaysInMonth { get; set; }
        

        int indexOfSelectedAppt = -1;
        int indexOfSelectedAthlete = -1;
        public int currentCoachID;
        string currentCoach;

        public Schedule()
        {
            InitializeComponent();
            FormatDataGridView(dgvAppt);
            FormatDataGridView(dgvAthlete);
            currentCoachID = LogIn.GetCoachID();
            coachlbl.Text = LogIn.GetCoachName();
        }

        // On scheduling form load
        private void Scheduling_Load(object sender, EventArgs e)
        {            
            rbWeek.Checked = true;
            LoadAppointments();
            LoadCustomerData();
            currentCoachID = LogIn.GetCoachID();
            currentCoach = LogIn.GetCoachName();
            dTPFrom.Value = DateTime.Now;

        }

        // Format Appointment DGV dates
        private void DgvAppt_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if Cell value is a DateTime
            if (e.Value is DateTime)
            {
                DateTime value = (DateTime)e.Value; 
                e.Value = DateTimeProcessor.GetCorrectedDate(value);// Correct date formats to local
            }

        }

        // Check if radio button Week is selected
        private void rbWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeek.Checked)
            {
                lblApptDates.Visible = true;
                dTPFrom.CustomFormat = "MMM dd, yyyy";
            }
        }

        // Check if radio button Month is selected
        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth.Checked)
            {
                lblApptDates.Visible = false;
                dTPFrom.CustomFormat = "MMMM";
            } 
        }

        // Select Correct Appointment row on cell click by index
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                indexOfSelectedAppt = e.RowIndex;
            }

        }

        // Format DGV
        private void FormatDataGridView(DataGridView view)
        {
            view.ClearSelection();
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.RowHeadersVisible = false;
            view.DefaultCellStyle.SelectionBackColor = Color.SpringGreen;
            view.DefaultCellStyle.SelectionForeColor = Color.Blue;
            view.RowsDefaultCellStyle.BackColor = Color.White;
            //view.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;
            view.CellBorderStyle = DataGridViewCellBorderStyle.SunkenHorizontal;
        }

        // Get appointment data from database with given stored parameter starting date and end date
        private void UpdateTable(string StartDate, string ToDate)
        {
            DataTable dt =  CoachProcessor.GetCoachDataByDate(currentCoachID, StartDate, ToDate);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dgvAppt.DataSource = bSource;

        }

        // Get upcomeing appointments if withing 15 minutes from now
        private void GetUpcomingMeeting()
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string toDate = Convert.ToDateTime(startDate).AddHours(1).ToString("yyyy-MM-dd HH:mm");

            UpdateTable(startDate, toDate);

        }

        // Get week to display
        private void GetWeek()
        {
            ConvertDatesToWeek();

            UpdateTable(StartDate, ToDate);
            dgvAppt.ClearSelection();
        }

        // Get month to disply
        private void GetMonth()
        {
            int currentMonth = (int)dTPFrom.Value.Month;
            int currentYear = (int)dTPFrom.Value.Year;
            int daysInMonth = (int)DateTime.DaysInMonth(currentYear, currentMonth);

            CurrentMonth = currentMonth;
            CurrentYear = currentYear;
            DaysInMonth = daysInMonth;

            ConvertDatesToMonth(currentMonth,currentYear,daysInMonth);

            string monthName = dTPFrom.Value.ToString("MMMM - yyyy");
            lblApptDates.Visible = true;
            lblApptDates.Text = monthName;
            string sp = "sp_getApptsByDate";

            //Get appointment by month based on starting date.
            UpdateTable(StartDate, ToDate);
            dgvAppt.ClearSelection();
        }


        // Convert dates to week
        private void ConvertDatesToWeek()
        {
            var dateFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            int currentDay = (int)dTPFrom.Value.DayOfWeek;
            string tempDate = dTPFrom.Value.AddDays(-currentDay + 1).ToString("yyyy-MM-dd HH:mm");
            DateTime tempStartDate = Convert.ToDateTime(tempDate).ToLocalTime();
            string toDate = tempStartDate.AddDays(6).ToString("yyyy-MM-dd HH:mm");
            string startDate = tempStartDate.ToString("yyyy-MM-dd HH:mm");

            //Convert Dates to format to Display
            string toDatelabel = tempStartDate.AddDays(6).ToString("dddd, MMMM dd, yyyy");
            string startDatelabel = tempStartDate.ToString("dddd, MMMM dd, yyyy" + "  -  ") + tempStartDate.AddDays(4).ToString("dddd, MMMM dd, yyyy");
            lblApptDates.Visible = true;
            lblApptDates.Text = startDatelabel;
            StartDate = startDate;
            ToDate = toDate;
        }


        // Convert Dates to month
        private void ConvertDatesToMonth(int currentMonth, int currentYear, int daysInMonth)
        {
            DateTime tempToDate = Convert.ToDateTime(currentYear.ToString() + "-" + currentMonth.ToString() + "-" + daysInMonth.ToString()).ToLocalTime();
            DateTime tempStartDate = Convert.ToDateTime(currentYear.ToString() + "-" + currentMonth.ToString() + "-" + "01").ToLocalTime();
            string toDate = tempToDate.ToString("yyyy-MM-dd-HH:mm");
            string startDate = tempStartDate.ToString("yyyy-MM-dd-HH:mm");
            StartDate = startDate;
            ToDate = toDate;
        }

        // Handle Loading data by month or week
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            if (rbMonth.Checked == true)
            {
                GetMonth();
            }
            else
            {
                GetWeek();
            }

        }

        // Load All of the current Coaches Appointments
        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            lblApptDates.Text = "All of your Appointments are Displayed";
            string sp = "sp_getApptsbyCoachID";
            LoadAppointments();
        }

        //Load Appointment DGV with appointment data
        private void LoadAppointments()
        {
            DataTable dt = new DataTable();
            dt = AppointmentProcessor.GetAppointmentByCoach(currentCoachID);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dgvAppt.DataSource = bSource;

            dgvAppt.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvAppt.Columns["Start"].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt"; //Format Start date
            dgvAppt.Columns["Start"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Autosize Start Column

            dgvAppt.Columns["End"].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt"; //Format End date
            dgvAppt.Columns["End"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Autosize End Column
            dgvAppt.Columns["Athlete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Autosize Athlete column
                                                                                                     //da.Update(dt);
        }

        private void DispayUpcomingAppointment()
        {
            DateTime upcoming  = new DateTime();

            upcoming = AppointmentProcessor.GetUpcomingAppointment(currentCoachID);

            // Show if there is an appointment within 15 minutes of current time. 
            if (upcoming != null)
            {
                lblUpcoming.Text = $"Upcoming Appointment: {DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime(upcoming))}"; // Correct date for Appointment display
                lblUpcoming.BackColor = Color.SpringGreen;
            }
            else
            {
                lblUpcoming.Text = $"Upcoming Appointments: None";                      
            }

        }

        // View or Edit selected appointment
        private void btnApptView_Click(object sender, EventArgs e)
        {
            try
            {
                if (indexOfSelectedAppt >= 0)
                {
                    int currentApptID = (int)dgvAppt.Rows[indexOfSelectedAppt].Cells[5].Value;
                    AppointmentForm viewAppointment = new AppointmentForm();
                    if (currentApptID > 0)
                    {
                        List<string> athleteNames = new List<string>();
                        athleteNames = AthleteProcessor.GetAthleteNames();

                        foreach (var item in athleteNames)
                        {
                            viewAppointment.cbCustName.Items.Add(item);
                        }

                        // Fill appointment consultant combobox
                        List<string> coachNames = new List<string>();
                        coachNames = CoachProcessor.GetCoachNames();

                        foreach (var item in coachNames)
                        {
                            viewAppointment.cbUsr.Items.Add(item);
                        }

                        //Fill appointment type combobox
                        List<string> appointmentTypes = new List<string>();
                        appointmentTypes = AppointmentProcessor.GetAppointmentTypes();

                        foreach (var item in appointmentTypes)
                        {
                            viewAppointment.cbUsr.Items.Add(item);
                        }

                        // Get appointment by appointmentID
                        Appointment apt = new Appointment();
                        apt = AppointmentProcessor.GetAppointmentByID(currentApptID);

                        viewAppointment.lblApptID.Text = apt.AppointmentID.ToString();
                        viewAppointment.cbCustName.Text = apt.AthleteName.ToString();
                        viewAppointment.cbType.Text = apt.Type.ToString();
                        viewAppointment.dTPStart.Text = DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime(apt.StartDate)).ToString("MMMM dd, yyyy");
                        DateTime appStartTime = DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime((apt.StartDate)));
                        viewAppointment.dTPStartTime.Text = appStartTime.ToString(" HH:mm");
                        viewAppointment.dtpEnd.Text =DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime((apt.EndDate))).ToString();
                        DateTime appEndTime = DateTimeProcessor.GetCorrectedDate(Convert.ToDateTime(apt.EndDate));
                        viewAppointment.dTPEndTime.Text = appEndTime.ToString(" hh:mm tt");
                        DateTime lastUpdated = Convert.ToDateTime(apt.Updated).ToLocalTime();
                        viewAppointment.lblUpdated.Text = lastUpdated.ToString("MMMM dd, yyyy hh:mm tt");

                    }
                    viewAppointment.gbAppointment.Text = "Edit Appointment";
                    this.Hide();
                    viewAppointment.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select and Appointment to View/Edit!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        private void dgvAppt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnApptView_Click(sender, e);
        }

        // Create new appointment
        private void btnAptNew_Click(object sender, EventArgs e)
        {
            this.Hide();
            AppointmentForm newAppointment = new AppointmentForm();

            try
            {
                // Fill appointment type combobox
                List<string> appointmentTypes = new List<string>();
                appointmentTypes = AppointmentProcessor.GetAppointmentTypes();

                foreach (var item in appointmentTypes)
                {
                    newAppointment.cbType.Items.Add(item);
                }

                // Fill appointment consultant combobox
                List<string> coachNames = new List<string>();
                coachNames = CoachProcessor.GetCoachNames();

                foreach (var item in coachNames)
                {
                    newAppointment.cbUsr.Text = item;
                    newAppointment.cbUsr.Items.Add(item);
                }

                // Fill appointment Athlete name combobox
                List<string> athleteNames = new List<string>();
                athleteNames = AthleteProcessor.GetAthleteNames();

                foreach (var item in athleteNames)
                {
                    newAppointment.cbCustName.Items.Add(item);
                }
               
                newAppointment.gbAppointment.Text = "New Appointment";
                newAppointment.lblApptID.Text = "0";
                newAppointment.lblUpdated.Visible = false;
                newAppointment.lblLastUpdate.Visible = false;
                newAppointment.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        // Delete selected appointment
        private void btnAptDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (indexOfSelectedAppt >= 0)
                {
                    int currentApptID = (int)dgvAppt.Rows[indexOfSelectedAppt].Cells[5].Value;
                    string currentAthlete = dgvAppt.Rows[indexOfSelectedAppt].Cells[1].Value.ToString();
                    string currentStartDate = dgvAppt.Rows[indexOfSelectedAppt].Cells[2].Value.ToString();
                    DialogResult result = MessageBox.Show($"Are you sure you " +
                    $"want to delete Appointment ID# {currentApptID} on {currentStartDate} with Athlete-{currentAthlete}?", "Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Delete appointment. 
                    if (result == DialogResult.Yes)
                    {
                        AppointmentProcessor.DeleteAppointment(currentApptID);
                        LoadAppointments();
                        MessageBox.Show($"Appointment ID#{currentApptID} on {currentStartDate} " +
                            $"with Athlete-{currentAthlete} has been Removed!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("No Appointments Deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select an Appointment to Remove!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }

        }

        // Select index of athlete
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                indexOfSelectedAthlete= e.RowIndex;
            }
            IsCorner();
            sp_AthleteByID = AthleteProcessor.SetSp_AthleteID(IsCorner());
            athleteDiscipline = AthleteProcessor.SetAthleteDiscipline(IsCorner());
        }

        // View/Edit athlete record
        private void btnAthleteViewEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (indexOfSelectedAthlete >= 0)
                {
                    int currentAthleteID = (int)dgvAthlete.Rows[indexOfSelectedAthlete].Cells[0].Value;
                    this.Hide();
                    AthleteForm viewAthlete = new AthleteForm();
                    if (currentAthleteID > 0)
                    {
                        List<string> distinctZips = new List<string>();
                        
                        distinctZips =  AddressProcessor.GetDistinctZips();
                        foreach (var item in distinctZips)
                        {
                            viewAthlete.cbZip.Items.Add(item);
                        }

                        Athlete currentAthlete = new Athlete();

                        currentAthlete = AthleteProcessor.GetAthleteByID(IsCorner(), currentAthleteID);

                        viewAthlete.lblCustID.Text = currentAthlete.AthleteID.ToString();
                        viewAthlete.tbName.Text = currentAthlete.AthleteName;
                        viewAthlete.cbPosition.Text = currentAthlete.AthletePosition;
                        viewAthlete.cbDiscipline.Text = currentAthlete.AthleteDiscipline;

                        viewAthlete.tbPhone.Text = currentAthlete.Phone;
                        viewAthlete.tbAdd.Text = currentAthlete.StreetAddress;
                        viewAthlete.tbCity.Text = currentAthlete.City;
                        viewAthlete.tbCountry.Text = currentAthlete.Country;
                        viewAthlete.cbZip.Text = currentAthlete.PostalCode;
                        if (AthleteProcessor.ConvertToImage(currentAthlete.ImageURL) != null)
                        {
                            viewAthlete.pictureBox1.Image = AthleteProcessor.ConvertToImage(currentAthlete.ImageURL);
                        }

                        //using (var wc = new WebClient())
                        //{
                        //    Uri test = new Uri("https://static.clubs.nfl.com/image/private/t_thumb_squared/f_auto/saints/qgdjllpuvflnwcxexuxu.jpg");
                        //    string mystring = @"https://static.clubs.nfl.com/image/private/t_thumb_squared/f_auto/saints/qgdjllpuvflnwcxexuxu.jpg";
                        //    using (var imgStream = new MemoryStream(wc.DownloadData(test)))
                        //    {
                        //        using (var objImage = Image.FromStream(imgStream))
                        //        {
                        //            viewAthlete.pictureBox1.Image = objImage;
                        //            //do stuff with the image
                        //        }
                        //    }
                        //}

                        AthleteForm.AddressID = currentAthlete.AddressID;

                        viewAthlete.lblLastUpdated.Text = DateTimeProcessor.GetCorrectedDate(currentAthlete.Updated).ToString();
                    }
                    viewAthlete.gbCustomer.Text = "Edit Athlete";
                    viewAthlete.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select and Appointment to Edit the Athlete!");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        private void dgvAthlete_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAthleteViewEdit_Click(sender, e);
        }


        // Create new athlete record
        private void btnCustNew_Click(object sender, EventArgs e)
        {
            try
            {
                //Fill Combo boxes with data from database
                this.Hide();
                AthleteForm newAthlete = new AthleteForm();

                List<string> distinctZips = new List<string>();

                distinctZips = AddressProcessor.GetDistinctZips();
                foreach (var item in distinctZips)
                {
                    newAthlete.cbZip.Items.Add(item);
                }
                //con.Open();
                //cmd.Connection = con;
                //cmd.CommandText = "sp_distinctCitysZipCountry";

                //cmd.CommandType = CommandType.StoredProcedure;
                //MySqlDataReader dr = cmd.ExecuteReader();

                //while (dr.Read())
                //{
                //    newAthlete.cbZip.Items.Add(dr["Zip"].ToString());
                //}

                //con.Close();

                //Change labels on new customer form
                newAthlete.gbCustomer.Text = "New Athlete";
                newAthlete.lblCustID.Text = "0".ToString();
                newAthlete.lblLastupdate.Visible = false;
                newAthlete.lblLastUpdated.Visible = false;
                AthleteForm.AddressID = 0;
                newAthlete.tbAdd.Enabled = true;
                newAthlete.tbPhone.Enabled = true;
                newAthlete.cbZip.Enabled = true;
                newAthlete.btnEditAddress.Visible = false;
                newAthlete.btnNewAddress.Visible = false;
                newAthlete.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        // Delete Athlete by athleteID
        private void btnCustDelete_Click(object sender, EventArgs e)
        {
            if (indexOfSelectedAthlete >= 0)
            {
                // Select Athlete
                int currentAthleteID = (int)dgvAthlete.Rows[indexOfSelectedAthlete].Cells[0].Value;
                int address_ID = (int)dgvAthlete.Rows[indexOfSelectedAthlete].Cells["AddressID"].Value;
                string currentAthlete = dgvAthlete.Rows[indexOfSelectedAthlete].Cells[1].Value.ToString();

                if (AthleteProcessor.CheckAssociation(currentAthleteID))
                {
                    MessageBox.Show($"Deletion of athlete {currentAthlete} is not allowed. {currentAthlete} is " +
                                $"associated with Appointments!", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DialogResult result = MessageBox.Show($"Athlete: {currentAthlete} is not associated with any appointment(s). " +
                    $"Are you sure you want to delete Athlete ID# {currentAthleteID}: {currentAthlete}?", "Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Delete Athlete and Address
                        AthleteProcessor.DeleteAthleteByID(currentAthleteID);
                        AddressProcessor.DeleteAddress(address_ID);
                        MessageBox.Show($"Athlete ID#{currentAthleteID} name {currentAthlete} " +
                            $"has been Removed!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {

                        MessageBox.Show("No Athletes Deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                LoadCustomerData();
            }
            else
            {
                MessageBox.Show("Select a Customer to Remove!");
            }
        }

        // Get all athletes data from database
        private void LoadCustomerData()
        {
            indexOfSelectedAthlete = -1;

            DataTable dt;

            dt = AthleteProcessor.GetAthletes();
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            dgvAthlete.DataSource = bSource;
            dgvAthlete.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvAthlete.Columns["Address"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvAthlete.Columns["AddressID"].Visible = false;
            dgvAthlete.ClearSelection();
        }

        // Handle Generate report button
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            indexOfSelectedAthlete = -1;
            IsCorner();
            sp_AthleteByID = AthleteProcessor.SetSp_AthleteID(IsCorner());
            athleteDiscipline = AthleteProcessor.SetAthleteDiscipline(IsCorner());
            string result = cbReports.Text;
            string sp;
            switch (result)
            {
                case "Appointment Types by Month":
                    MonthlyAppointments();
                    break;
                case "Coach's Schedule":
                    CoachSchedule();
                    break;
                case "All Coach's Schedule":
                    AllCoachAppointments();
                    break;
                case "Safeties":
                    sp = "sp_safeties";
                    DBReports(sp,athleteDiscipline);
                    break;
                case "Corners":
                    sp = "sp_corners";
                    DBReports(sp,athleteDiscipline);
                    break;
                default:
                    break;
            }

        }

        // Print Monthly appointments by type
        private void MonthlyAppointments()
        {
            int currentMonth = (int)dtpMonth.Value.Month;
            int currentYear = (int)dtpMonth.Value.Year;
            int daysInMonth = (int)DateTime.DaysInMonth(currentYear, currentMonth);

            CurrentMonth = currentMonth;
            CurrentYear = currentYear;
            DaysInMonth = daysInMonth;

            ConvertDatesToMonth(currentMonth, currentYear, daysInMonth);
            try
            {
                var pg = new StringBuilder();
                pg = AppointmentProcessor.GetAppointmentCount(dtpMonth.Text, StartDate, ToDate);                

                rtbReport.Text = pg.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }

        }

        // Print coach's appointments by coach to a report
        private void CoachSchedule()
        {
            string coachName = cbConsultant.Text;

            try
            {
                var pg = new StringBuilder();
                pg = CoachProcessor.GetCoachSchedule(coachName);
                rtbReport.Text = pg.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }


        }

        // Print All coachs appointments to a report
        private void AllCoachAppointments()
        {
            try
            {
                // Initilize stringbuilder
                var pg = new StringBuilder();
                pg = CoachProcessor.GetAllCoachesAppointments();

                rtbReport.Text = pg.ToString(); // Print report to Rich text box
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        // Print All DBs by position to a report
        private void DBReports(string sp, string athleteDiscipline)
        {
            try
            {
                // Initilize stringbuilder
                var pg = new StringBuilder();
                pg = AthleteProcessor.GetDBReports(sp, athleteDiscipline);

                rtbReport.Text = pg.ToString(); // Print report to Rich text box
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        // Handle reports combobox selection 
        private void cbReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbReports.Text == "Appointment Types by Month")
            {
                cbConsultant.Visible = false;
                dtpMonth.Visible = true;
                label2.Visible = true;
                label2.Text = "Please select Month: ";
            }
            else if(cbReports.Text == "Coach's Schedule")
            {
                dtpMonth.Visible = false;
                cbConsultant.Visible = true;
                label2.Visible = true;
                GetCoachNames();
                label2.Text = "Select Coach: ";
            }
            else if(cbReports.Text == "All Coach's Schedule")
            {
                dtpMonth.Visible = false;
                cbConsultant.Visible = false;
                label2.Visible = false;
            }
            else if (cbReports.Text == "Corners" || cbReports.Text == "Safeties")
            {
                dtpMonth.Visible = false;
                cbConsultant.Visible = false;
                label2.Visible = false;
            }

            IsCorner();
            sp_AthleteByID = AthleteProcessor.SetSp_AthleteID(IsCorner());
            athleteDiscipline = AthleteProcessor.SetAthleteDiscipline(IsCorner());

        }

        // Get Coach names from database
        private void GetCoachNames()
        {
            List<string> coachNames = new List<string>();
            coachNames = CoachProcessor.GetCoachNames();
            foreach (var coachName in coachNames)
            {
                cbConsultant.Items.Add(coachName);
            }
            
        }

        public bool IsCorner()
        {

            if(indexOfSelectedAthlete != -1)
            {
                if((string)dgvAthlete.Rows[indexOfSelectedAthlete].Cells[1].Value == "Corner")
                {
                    return true;
                }
                
            }
            if(cbReports.Text == "Corners")
            {
                return true;

            }
            if(cbReports.Text == "Safeties")
            {
                return false;
            }
            else
            {
                return false;
            }

        }

        public static string SetSp_AthleteID(bool isCorner)
        {
            string sp_AthleteByID;
            if (isCorner)
            {
                sp_AthleteByID = "sp_viewEditCornerByID";

            }
            else
            {
                sp_AthleteByID = "sp_viewEditSafetyByID";;
            }
            return sp_AthleteByID;
        }
        public static string SetAthleteDiscipline(bool isCorner)
        {
            string athleteDiscipline;
            if (isCorner)
            {
                athleteDiscipline = "cornerDiscipline";
            }
            else
            {
                athleteDiscipline = "safetyDiscipline";
            }
            return athleteDiscipline;
        }

        // Exit program
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void searchBoxAppt_TextChanged(object sender, EventArgs e)
        {
            // Search for Part by Name.
            
            ForeColor = Color.Black;
            dgvAppt.ClearSelection();
            dgvAppt.ClearSelection();
            dgvAppt.DefaultCellStyle.SelectionBackColor = Color.SpringGreen;
            bool found = false;

            // If searchbox is not blank, select parts.
            if (searchBoxAppt.Text != "")
            {
                // Loop through parts.
                for (int i = 0; i < dgvAppt.Rows.Count; i++)
                {
                    if (dgvAppt.Rows[i].Cells[0].Value.ToString().ToUpper().Contains(searchBoxAppt.Text.ToUpper())
                        || dgvAppt.Rows[i].Cells[1].Value.ToString().ToUpper().Contains(searchBoxAppt.Text.ToUpper()))
                    {
                        // Select parts.
                        dgvAppt.MultiSelect = true;
                        dgvAppt.Rows[i].Selected = true;
                        found = true;
                    }
                }
            }
        }

        private void searchBoxAppt_Enter(object sender, EventArgs e)
        {
            searchBoxAppt.Text = string.Empty;
            searchBoxAppt.ForeColor = Color.Black;
        }
        private void searchBoxAppt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchBoxAppt.Text))
            {
                searchBoxAppt.Text = "Search for...";
                searchBoxAppt.ForeColor = Color.DarkGray;
            }
        }

        private void searchBoxAthl_TextChanged(object sender, EventArgs e)
        {
            // Search for Part by Name.
            ForeColor = Color.Black;
            dgvAthlete.ClearSelection();
            dgvAthlete.ClearSelection();
            dgvAthlete.DefaultCellStyle.SelectionBackColor = Color.SpringGreen;
            bool found = false;

            // If searchbox is not blank, select parts.
            if (searchBoxAthl.Text != "")
            {
                // Loop through parts.
                for (int i = 0; i < dgvAthlete.Rows.Count; i++)
                {
                    if (dgvAthlete.Rows[i].Cells[1].Value.ToString().ToUpper().Contains(searchBoxAthl.Text.ToUpper())
                        || dgvAthlete.Rows[i].Cells[2].Value.ToString().ToUpper().Contains(searchBoxAthl.Text.ToUpper())
                        || dgvAthlete.Rows[i].Cells[5].Value.ToString().ToUpper().Contains(searchBoxAthl.Text.ToUpper()))
                    {
                        // Select parts.
                        dgvAthlete.MultiSelect = true;
                        dgvAthlete.Rows[i].Selected = true;
                        found = true;
                    }
                }
            }

        }

        private void searchBoxAthl_Enter(object sender, EventArgs e)
        {
            searchBoxAthl.Text = string.Empty;
            searchBoxAthl.ForeColor = Color.Black;
        }

        private void searchBoxAthl_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchBoxAthl.Text))
            {
                searchBoxAthl.Text = "Search for...";
                searchBoxAthl.ForeColor = Color.DarkGray;
            }
        }
    }
}
