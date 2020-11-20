using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CoverZeroScheduling
{
    public partial class Scheduling : Form
    {
        public static int currentApptID;
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da;
        DataTable dt;
        MySqlDataReader dr;
        string sp_AthleteByID;
        string athleteDiscipline;
        public string StartDate { get; set; }
        public string ToDate { get; set; }
        
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public int DaysInMonth { get; set; }

        int indexOfSelectedAppt = -1;
        int indexOfSelectedAthlete = -1;
        int currentCoachID;
        string currentCoach;

        public Scheduling()
        {
            InitializeComponent();
            FormatDataGridView(dgvAppt);
            FormatDataGridView(dgvCustomer);  
        }

        // On scheduling form load
        private void Scheduling_Load(object sender, EventArgs e)
        {            
            rbWeek.Checked = true;
            string sp = "sp_getApptsbyCoachID";
            LoadAppointmentData(sp);
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
                e.Value = GetCorrectedDate(value);// Correct date formats to local
            }

        }

        // Correct DateTime to local and output datetime
        internal static DateTime GetCorrectedDate(DateTime dateTime)
        {
            TimeZone curTimeZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = curTimeZone.GetUtcOffset(DateTime.Now);
            return dateTime.AddHours(currentOffset.TotalHours);
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
            view.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;
        }

        // Get appointment data from database with given stored parameter starting date and end date
        private void GetData(string sp, string StartDate, string ToDate)
        {
            using (con)
            {
                con.Open();
                cmd = new MySqlCommand(sp, con);
                da = new MySqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CoachID", LogIn.GetCoachID());
                cmd.Parameters.AddWithValue("@startDate", StartDate);
                cmd.Parameters.AddWithValue("@endDate", ToDate);
                //da.Fill(ds);
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                da.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                dgvAppt.DataSource = bSource;
                da.Update(dt);
            }
            con.Close();

        }

        // Get upcomeing appointments if withing 15 minutes from now
        private void GetUpcomingMeeting()
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string toDate = Convert.ToDateTime(startDate).AddHours(1).ToString("yyyy-MM-dd HH:mm");

            string sp = "sp_getApptsByDate";
            GetData(sp, startDate, toDate);

        }

        // Get week to display
        private void GetWeek()
        {
            ConvertDatesToWeek();
            string sp = "sp_getApptsByDate";

            GetData(sp, StartDate, ToDate);
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
            GetData(sp, StartDate, ToDate);
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
            LoadAppointmentData(sp);
        }

        //Load Appointment DGV with appointment data
        private void LoadAppointmentData(string sp)
        {

            using (con)
            {                
                using (MySqlCommand cmd = new MySqlCommand(sp, con))
                {
                    con.Open();
                    da = new MySqlDataAdapter(cmd);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CoachID", LogIn.GetCoachID());
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dgvAppt.DataSource = bSource;

                    dgvAppt.Columns["Start"].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt"; //Format Start date
                    dgvAppt.Columns["Start"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Autosize Start Column

                    dgvAppt.Columns["End"].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt"; //Format End date
                    dgvAppt.Columns["End"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Autosize End Column
                    dgvAppt.Columns["Athlete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Autosize Athlete column
                    da.Update(dt);

                    string startDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local).ToString("yyyy-MM-dd HH:mm");
                    string toDate = Convert.ToDateTime(startDate).AddHours(1).ToString("yyyy-MM-dd HH:mm");

                    MySqlCommand cmd2 = new MySqlCommand("sp_getApptsByDate");
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Connection = con;
                    cmd2.Parameters.AddWithValue("@CoachID", LogIn.GetCoachID());
                    cmd2.Parameters.AddWithValue("@startDate", startDate);
                    cmd2.Parameters.AddWithValue("@endDate", toDate);
                    MySqlDataReader dr = cmd2.ExecuteReader();

                    // Show if there is an appointment within 15 minutes of current time. 
                    if (dr.Read())
                    {
                        lblUpcoming.Text = $"Upcoming Appointment: {GetCorrectedDate(Convert.ToDateTime(dr["Start"])).ToString()}"; // Correct date for Appointment display
                        lblUpcoming.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        lblUpcoming.Text = $"Upcoming Appointments: None";                      
                    }
                    dr.Close();

                }
                con.Close();
                dgvAppt.ClearSelection();
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
                        con.Open();

                        // Fill appointment athlete name combobox
                        MySqlCommand cmd2 = new MySqlCommand("sp_athleteName");
                        cmd2.Connection = con;
                        dr = cmd2.ExecuteReader();

                        while (dr.Read())
                        {
                            viewAppointment.cbCustName.Items.Add(dr["athleteName"].ToString());
                        }
                        dr.Close();

                        // Fill appointment consultant combobox
                        MySqlCommand cmd3 = new MySqlCommand("sp_getDistinctCoach");
                        cmd3.Connection = con;
                        dr = cmd3.ExecuteReader();

                        while (dr.Read())
                        {
                            viewAppointment.cbUsr.Items.Add(dr["coachName"].ToString());
                            viewAppointment.cbUsr.Text = currentCoach.ToString();
                        }
                        dr.Close();

                        //Fill appointment type combobox
                        MySqlCommand cmd4 = new MySqlCommand("sp_allAppTypes");
                        cmd4.Connection = con;
                        dr = cmd4.ExecuteReader();

                        while (dr.Read())
                        {
                            viewAppointment.cbType.Items.Add(dr["type"].ToString());
                        }
                        dr.Close();

                        // Get appointment by appointmentID
                        cmd.Connection = con;
                        cmd.CommandText = "sp_viewAppts";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@apptID", currentApptID);
                        dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            viewAppointment.lblApptID.Text = dr["appointmentID"].ToString();
                            viewAppointment.cbCustName.Text = dr["athleteName"].ToString();
                            viewAppointment.cbType.Text = dr["type"].ToString();
                            viewAppointment.dTPStart.Text = GetCorrectedDate(Convert.ToDateTime((dr["start"]))).ToString("MMMM dd, yyyy");
                            DateTime appStartTime = GetCorrectedDate(Convert.ToDateTime((dr["start"])));
                            viewAppointment.dTPStartTime.Text = appStartTime.ToString(" HH:mm");
                            viewAppointment.dtpEnd.Text = GetCorrectedDate(Convert.ToDateTime((dr["end"]))).ToString();
                            DateTime appEndTime = GetCorrectedDate(Convert.ToDateTime(dr["end"]));
                            viewAppointment.dTPEndTime.Text = appEndTime.ToString(" hh:mm tt");
                            DateTime lastUpdated = Convert.ToDateTime(dr["lastUpdate"]).ToLocalTime();
                            viewAppointment.lblUpdated.Text = lastUpdated.ToString("MMMM dd, yyyy hh:mm tt");

                        }
                        con.Close();
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
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }

        // Create new appointment
        private void btnAptNew_Click(object sender, EventArgs e)
        {
            this.Hide();
            AppointmentForm newAppointment = new AppointmentForm();

            try
            {
                con.Open();

                // Fill appointment type combobox
                cmd.Connection = con;
                cmd.CommandText = "sp_allAppTypes";
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    newAppointment.cbType.Items.Add(dr["type"].ToString());
                }
                dr.Close();

                // Fill appointment consultant combobox
                MySqlCommand cmd3 = new MySqlCommand("sp_getDistinctCoach");
                cmd3.Connection = con;
                dr = cmd3.ExecuteReader();

                while (dr.Read())
                {
                    newAppointment.cbUsr.Text = dr["coachName"].ToString();
                    newAppointment.cbUsr.Items.Add(dr["coachName"].ToString());
                }
                dr.Close();

                // Fill appointment Athlete name combobox
                MySqlCommand cmd2 = new MySqlCommand("sp_athleteName");
                cmd2.Connection = con;
                dr = cmd2.ExecuteReader();

                while (dr.Read())
                {
                    newAppointment.cbCustName.Items.Add(dr["athleteName"].ToString());
                }
               
                newAppointment.gbAppointment.Text = "New Appointment";
                newAppointment.lblApptID.Text = "0";
                newAppointment.lblUpdated.Visible = false;
                newAppointment.lblLastUpdate.Visible = false;
                //newAppointment.dTPStartTime.Value = DateTime.Today;
                //newAppointment.dTPEndTime.Value = DateTime.Today;
                newAppointment.ShowDialog();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
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
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "sp_apptDeletebyID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@apptID", currentApptID);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string sp = "sp_getApptsbyCoachID";
                        LoadAppointmentData(sp);
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
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
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
            SetAthlete();
        }

        // View/Edit athlete record
        private void btnCustViewEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (indexOfSelectedAthlete >= 0)
                {
                    int currentAthleteID = (int)dgvCustomer.Rows[indexOfSelectedAthlete].Cells[0].Value;
                    this.Hide();
                    AthleteForm viewAthlete = new AthleteForm();
                    if (currentAthleteID > 0)
                    {
                        con.Open();

                        MySqlCommand cmd4 = new MySqlCommand("sp_distinctZip");
                        cmd4.Connection = con;
                        MySqlDataReader dr = cmd4.ExecuteReader();

                        while (dr.Read())
                        {
                            viewAthlete.cbZip.Items.Add(dr["postalCode"].ToString());
                            
                        }
                        dr.Close();

                        cmd.Connection = con;
                        cmd.CommandText = sp_AthleteByID;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@AthlID", currentAthleteID);
                        dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            viewAthlete.lblCustID.Text = dr["athleteId"].ToString();
                            viewAthlete.tbName.Text = dr["athleteName"].ToString();
                            
                            //viewAthlete.cbPosition.Items.Add(dr["athletePosition"].ToString());
                            viewAthlete.cbPosition.Text = dr["athletePosition"].ToString();
                            //viewAthlete.cbDiscipline.Items.Add(dr["athleteDiscipline"].ToString());
                            viewAthlete.cbDiscipline.Text = dr[athleteDiscipline].ToString();
                            //viewAthlete.cbDiscipline.Text = dr["athleteDiscipline"].ToString();
                            viewAthlete.tbPhone.Text = dr["phone"].ToString();
                            viewAthlete.tbAdd.Text = dr["address"].ToString();
                            viewAthlete.tbCity.Text = dr["city"].ToString();
                            viewAthlete.tbCountry.Text = dr["country"].ToString();
                            viewAthlete.cbZip.Text = dr["postalCode"].ToString();
                            AthleteForm.AddressID = Convert.ToInt32(dr["addressId"]);
                            viewAthlete.lblLastUpdated.Text = Scheduling.GetCorrectedDate(Convert.ToDateTime(dr["lastUpdate"])).ToString();
                        }
                        con.Close();
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
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        // Create new athlete record
        private void btnCustNew_Click(object sender, EventArgs e)
        {
            try
            {
                //Fill Combo boxes with data from database
                this.Hide();
                AthleteForm newAthlete = new AthleteForm();

                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "sp_distinctCitysZipCountry";

                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    newAthlete.cbZip.Items.Add(dr["Zip"].ToString());
                }

                con.Close();

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
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }

        // Delete customer by customerID
        private void btnCustDelete_Click(object sender, EventArgs e)
        {
            if (indexOfSelectedAthlete >= 0)
            {
                // Select Customer
                int currentAthleteID = (int)dgvCustomer.Rows[indexOfSelectedAthlete].Cells[0].Value;
                int address_ID = (int)dgvCustomer.Rows[indexOfSelectedAthlete].Cells["AddressID"].Value;
                string currentAthlete = dgvCustomer.Rows[indexOfSelectedAthlete].Cells[1].Value.ToString();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "sp_distinctApptAthleteID";
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        if (currentAthleteID.Equals(Convert.ToInt32((dr["athleteId"]))))
                        {
                            MessageBox.Show($"Deletion of athlete {currentAthlete} is not allowed. {currentAthlete} is " +
                                $"associated with Appointments!", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto done;
                        }

                    }

                    dr.Close();
                DialogResult result = MessageBox.Show($"Athlete: {currentAthlete} is not associated with any appointment(s). " +
                    $"Are you sure you want to delete Athlete ID# {currentAthleteID}: {currentAthlete}?", "Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Delete Athlete
                if (result == DialogResult.Yes)
                {

                    MySqlCommand cmd2 = new MySqlCommand("sp_AthleteDeletebyID");
                    cmd2.Connection = con;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("@Athlete_ID", currentAthleteID);
                    cmd2.ExecuteNonQuery();


                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "sp_deleteAddressbyID";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@Address_ID", address_ID);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show($"Athlete ID#{currentAthleteID} name {currentAthlete} " +
                        $"has been Removed!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }                
                else
                {
                
                    MessageBox.Show("No Athletes Deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                done:;
                con.Close();
                LoadCustomerData();

            }
            else
            {
                MessageBox.Show("Select a Customer to Remove!");
            }
        }

        // Get all customer data from database
        private void LoadCustomerData()
        {
            using (con)
            {
                indexOfSelectedAthlete = -1;
                using (MySqlCommand cmd = new MySqlCommand("sp_viewAllAthletes", con))
                {
                    con.Open();
                    da = new MySqlDataAdapter(cmd);
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dgvCustomer.DataSource = bSource;
                    dgvCustomer.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgvCustomer.Columns["Address"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgvCustomer.Columns["AddressID"].Visible = false;
                    da.Update(dt);
                }
            }
            con.Close();
            dgvCustomer.ClearSelection();
        }

        // Handle Generate report button
        private void btnGenerate_Click(object sender, EventArgs e)
        {
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
                case "Safties":
                    sp = "sp_safeties";
                    DBReports(sp);
                    break;
                case "Corners":
                    sp = "sp_corners";
                    DBReports(sp);
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

            try
            {
                con.Open();
                ConvertDatesToMonth(currentMonth, currentYear, daysInMonth);

                MySqlCommand cmd2 = new MySqlCommand("sp_appointmentTypebyMonth", con);
                cmd2.Connection = con;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@startDate", StartDate);
                cmd2.Parameters.AddWithValue("@endDate", ToDate);
                MySqlDataReader dr = cmd2.ExecuteReader();
                var pg = new StringBuilder();
                pg.Append($"\t\t Appointments for the month of {dtpMonth.Text} \n\n");
                pg.Append(String.Format("{0,-15}{1,10}\n", "Type", "Count"));
                pg.Append($"{string.Concat(Enumerable.Repeat("*", 80))} \n");
                while (dr.Read())
                {
                    pg.AppendFormat(String.Format("{0,-15}\t {1,-10:N0} \n", dr["type"], Convert.ToInt32(dr["Count(type)"])));
                }
                con.Close();

                rtbReport.Text = pg.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }

        // Print coach's appointments by coach to a report
        private void CoachSchedule()
        {
            string coachName = cbConsultant.Text;

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("sp_getCoachSchedule", con);
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
                        GetCorrectedDate(Convert.ToDateTime(dr["start"])), GetCorrectedDate(Convert.ToDateTime(dr["end"]))));
                }
                con.Close();

                rtbReport.Text = pg.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }

        // Print All coachs appointments to a report
        private void AllCoachAppointments()
        {
            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("sp_getAllAppointments", con);
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
                        GetCorrectedDate(Convert.ToDateTime(dr["start"])), GetCorrectedDate(Convert.ToDateTime(dr["end"]))));
                }
                con.Close();

                rtbReport.Text = pg.ToString(); // Print report to Rich text box
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        // Print All DBs by position to a report
        private void DBReports(string sp)
        {
            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(sp, con);
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
                    pg.AppendFormat(String.Format("{0,-25}\t{1,-25}\t{2,-25} \n", dr["athleteName"], dr["athleteDiscipline"],
                        dr["athletePosition"]));
                }
                con.Close();

                rtbReport.Text = pg.ToString(); // Print report to Rich text box
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
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

        }

        // Get Coach names from database
        private void GetCoachNames()
        {
            using (con)
            {
                con.Open();


                MySqlCommand cmd = new MySqlCommand("sp_getDistinctCoach");
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cbConsultant.Items.Add(dr["coachName"].ToString());
                }

            }
            con.Close();
        }

        private bool IsCorner()
        {
            if((string)dgvCustomer.Rows[indexOfSelectedAthlete].Cells[1].Value == "Corner")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void SetAthlete()
        {
            if (IsCorner())
            {
                sp_AthleteByID = "sp_viewEditCornerByID";
                athleteDiscipline = "cornerDiscipline";
            }
            else
            {
                sp_AthleteByID = "sp_viewEditSafetyByID";
                athleteDiscipline = "safetyDiscipline";
            }
        }

        // Exit program
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
