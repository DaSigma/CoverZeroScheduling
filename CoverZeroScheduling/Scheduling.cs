using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        MySqlConnection con = new MySqlConnection(@"server=3.227.166.251;user id=U04cRO;password=53688204070;persistsecurityinfo=True;database=U04cRO");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da;
        DataTable dt;
        MySqlDataReader dr;
        public string StartDate { get; set; }
        public string ToDate { get; set; }
        
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public int DaysInMonth { get; set; }

        int indexOfSelectedAppt = -1;
        int indexOfSelectedCustomer = -1;
        int currentUserID;
        string currentUser;

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
            string sp = "sp_getApptsbyUsrID";
            LoadAppointmentData(sp);
            LoadCustomerData();
            currentUserID = LogIn.GetUserID();
            currentUser = LogIn.GetUserName();
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
                cmd.Parameters.AddWithValue("@usrID", LogIn.GetUserID());
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

        // Get upcomeing meeting if withing 15 minutes from now
        private void GetUpcomingMeeting()
        {
            string startDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string toDate = Convert.ToDateTime(startDate).AddMinutes(15).ToString("yyyy-MM-dd HH:mm");

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

        // Load All of the current Consultants Appointments
        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            lblApptDates.Text = "All of your Appointments are Displayed";
            string sp = "sp_getApptsbyUsrID";
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
                    cmd.Parameters.AddWithValue("@usrID", LogIn.GetUserID());
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
                    dgvAppt.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Autosize Customer column
                    da.Update(dt);

                    string startDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local).ToString("yyyy-MM-dd HH:mm");
                    string toDate = Convert.ToDateTime(startDate).AddMinutes(15).ToString("yyyy-MM-dd HH:mm");

                    MySqlCommand cmd2 = new MySqlCommand("sp_getApptsByDate");
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Connection = con;
                    cmd2.Parameters.AddWithValue("@usrID", LogIn.GetUserID());
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

                        // Fill appointment customer name combobox
                        MySqlCommand cmd2 = new MySqlCommand("sp_customerName");
                        cmd2.Connection = con;
                        dr = cmd2.ExecuteReader();

                        while (dr.Read())
                        {
                            viewAppointment.cbCustName.Items.Add(dr["customerName"].ToString());
                        }
                        dr.Close();

                        // Fill appointment consultant combobox
                        MySqlCommand cmd3 = new MySqlCommand("sp_getDistinctUsr");
                        cmd3.Connection = con;
                        dr = cmd3.ExecuteReader();

                        while (dr.Read())
                        {
                            viewAppointment.cbUsr.Items.Add(dr["userName"].ToString());
                            viewAppointment.cbUsr.Text = currentUser.ToString();
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
                            viewAppointment.cbCustName.Text = dr["customerName"].ToString();
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
                MySqlCommand cmd3 = new MySqlCommand("sp_getDistinctUsr");
                cmd3.Connection = con;
                dr = cmd3.ExecuteReader();

                while (dr.Read())
                {
                    newAppointment.cbUsr.Text = dr["userName"].ToString();
                    newAppointment.cbUsr.Items.Add(dr["userName"].ToString());
                }
                dr.Close();

                // Fill appointment Custoomer name combobox
                MySqlCommand cmd2 = new MySqlCommand("sp_customerName");
                cmd2.Connection = con;
                dr = cmd2.ExecuteReader();

                while (dr.Read())
                {
                    newAppointment.cbCustName.Items.Add(dr["customerName"].ToString());
                }
               
                newAppointment.gbAppointment.Text = "New Appointment";
                newAppointment.lblApptID.Text = "Auto Generated";
                newAppointment.lblUpdated.Visible = false;
                newAppointment.lblLastUpdate.Visible = false;
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
                    string currentCustomer = dgvAppt.Rows[indexOfSelectedAppt].Cells[1].Value.ToString();
                    string currentStartDate = dgvAppt.Rows[indexOfSelectedAppt].Cells[2].Value.ToString();
                    DialogResult result = MessageBox.Show($"Are you sure you " +
                    $"want to delete Appointment ID# {currentApptID} on {currentStartDate} with Customer-{currentCustomer}?", "Delete",
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
                        string sp = "sp_getApptsbyUsrID";
                        LoadAppointmentData(sp);
                        MessageBox.Show($"Appointment ID#{currentApptID} on {currentStartDate} " +
                            $"with Customer-{currentCustomer} has been Removed!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        // Select index of customer
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                indexOfSelectedCustomer = e.RowIndex;
            }
        }

        // View/Edit customer record
        private void btnCustViewEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (indexOfSelectedCustomer >= 0)
                {
                    int currentCustomerID = (int)dgvCustomer.Rows[indexOfSelectedCustomer].Cells[0].Value;
                    this.Hide();
                    CustomerForm viewCustomer = new CustomerForm();
                    if (currentCustomerID > 0)
                    {
                        con.Open();

                        MySqlCommand cmd4 = new MySqlCommand("sp_distinctZip");
                        cmd4.Connection = con;
                        MySqlDataReader dr = cmd4.ExecuteReader();

                        while (dr.Read())
                        {
                            viewCustomer.cbCustZip.Items.Add(dr["postalCode"].ToString());
                        }
                        dr.Close();

                        cmd.Connection = con;
                        cmd.CommandText = "sp_viewEditCustomerByID";

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@custID", currentCustomerID);
                        dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            viewCustomer.lblCustID.Text = dr["customerId"].ToString();
                            viewCustomer.tbCustName.Text = dr["customerName"].ToString();
                            viewCustomer.tbCustPhone.Text = dr["phone"].ToString();
                            viewCustomer.tbCustAdd.Text = dr["address"].ToString();
                            viewCustomer.tbCustCity.Text = dr["city"].ToString();
                            viewCustomer.tbCustCountry.Text = dr["country"].ToString();
                            viewCustomer.cbCustZip.Text = dr["postalCode"].ToString();
                            CustomerForm.AddressID = Convert.ToInt32(dr["addressId"]);
                            viewCustomer.lblLastUpdated.Text = Scheduling.GetCorrectedDate(Convert.ToDateTime(dr["lastUpdate"])).ToString();
                        }
                        con.Close();
                    }
                    viewCustomer.gbCustomer.Text = "Edit Customer";
                    viewCustomer.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select and Appointment to Edit the Customer!");
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

        // Create new customer record
        private void btnCustNew_Click(object sender, EventArgs e)
        {
            try
            {
                //Fill Combo boxes with data from database
                this.Hide();
                CustomerForm newCustomer = new CustomerForm();

                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "sp_distinctCitysZipCountry";

                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    newCustomer.cbCustZip.Items.Add(dr["Zip"].ToString());
                }

                con.Close();

                //Change labels on new customer form
                newCustomer.gbCustomer.Text = "New Customer";
                newCustomer.lblCustID.Text = "0".ToString();
                newCustomer.lblLastupdate.Visible = false;
                newCustomer.lblLastUpdated.Visible = false;
                CustomerForm.AddressID = 0;
                newCustomer.tbCustAdd.Enabled = true;
                newCustomer.tbCustPhone.Enabled = true;
                newCustomer.cbCustZip.Enabled = true;
                newCustomer.btnEditAddress.Visible = false;
                newCustomer.btnNewAddress.Visible = false;
                newCustomer.ShowDialog();
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
            if (indexOfSelectedCustomer >= 0)
            {
                // Select Customer
                int currentCustomerID = (int)dgvCustomer.Rows[indexOfSelectedCustomer].Cells[0].Value;
                int address_ID = (int)dgvCustomer.Rows[indexOfSelectedCustomer].Cells["AddressID"].Value;
                string currentCustomer = dgvCustomer.Rows[indexOfSelectedCustomer].Cells[1].Value.ToString();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "sp_distinctApptCustomerID";
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        if (currentCustomerID.Equals(Convert.ToInt32((dr["customerId"]))))
                        {
                            MessageBox.Show($"Deletion of customer {currentCustomer} is not allowed. {currentCustomer} is " +
                                $"associated with Appointments!", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto done;
                        }

                    }

                    dr.Close();
                DialogResult result = MessageBox.Show($"Customer: {currentCustomer} is not associated with any appointment(s). " +
                    $"Are you sure you want to delete Customer ID# {currentCustomerID}: {currentCustomer}?", "Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Delete Customer
                if (result == DialogResult.Yes)
                {

                    MySqlCommand cmd2 = new MySqlCommand("sp_custDeletebyID");
                    cmd2.Connection = con;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("@custID", currentCustomerID);
                    cmd2.ExecuteNonQuery();


                    cmd2.Parameters.Clear();
                    cmd2.CommandText = "sp_deleteAddressbyID";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@Address_ID", address_ID);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show($"Customer ID#{currentCustomerID} name {currentCustomer} " +
                        $"has been Removed!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }                
                else
                {
                
                    MessageBox.Show("No Customers Deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                indexOfSelectedCustomer = -1;
                using (MySqlCommand cmd = new MySqlCommand("sp_viewAllCustomers", con))
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
            switch (result)
            {
                case "Appointment Types by Month":
                    MonthlyAppointments();
                    break;
                case "Consultant Schedule":
                    ConsultantSchedule();
                    break;
                case "All Consultants Schedule":
                    AllConsultantAppointments();
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

        // Print consultant appointments by consultant to a report
        private void ConsultantSchedule()
        {
            string usrName = cbConsultant.Text;

            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("sp_getConsultantSchedule", con);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usrName", usrName);

                MySqlDataReader dr = cmd.ExecuteReader();
                var pg = new StringBuilder();
                pg.Append($"\t\t\t All appointments for Consultant {usrName} \n\n");
                pg.Append(String.Format("{0,-15}\t{1,-15}\t{2,-25}\t\t{3,-25}\n", "Consultant", "Type", "Start", "End"));
                pg.Append($"{string.Concat(Enumerable.Repeat("*", 100))} \n");
                while (dr.Read())
                {
                    pg.AppendFormat(String.Format("{0,-15}\t{1,-15}\t{2,-25}\t{3,-25} \n", dr["userName"], dr["type"],
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

        // Print All consultant appointments to a report
        private void AllConsultantAppointments()
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
                pg.Append($"\t\t\t All appointments for all Consultants \n\n");// Report Title
                pg.Append(String.Format("{0,-15}\t{1,-15}\t{2,-25}\t\t{3,-25}\n", "Consultant", "Type", "Start", "End"));// Report Header
                pg.Append($"{string.Concat(Enumerable.Repeat("*", 100))} \n");
                while (dr.Read())
                {
                    pg.AppendFormat(String.Format("{0,-15}\t{1,-15}\t{2,-25}\t{3,-25} \n", dr["userName"], dr["type"], 
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
            else if(cbReports.Text == "Consultant Schedule")
            {
                dtpMonth.Visible = false;
                cbConsultant.Visible = true;
                label2.Visible = true;
                GetConsultants();
                label2.Text = "Select Consultant: ";
            }
            else if(cbReports.Text == "All Consultants Schedule")
            {
                dtpMonth.Visible = false;
                cbConsultant.Visible = false;
                label2.Visible = false;
            }

        }

        // Get Consultant names from database
        private void GetConsultants()
        {
            using (con)
            {
                con.Open();


                MySqlCommand cmd = new MySqlCommand("sp_getDistinctUsr");
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cbConsultant.Items.Add(dr["userName"].ToString());
                }

            }
            con.Close();
        }

        // Exit program
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
