using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoverZeroScheduling
{
    public partial class AthleteForm : Form
    {
        MySqlConnection con = new MySqlConnection(@"server=3.227.166.251;user id=U04cRO;password=53688204070;persistsecurityinfo=True;database=U04cRO");
        int athleteID;
        int addressID;
        public static int AddressID { get; set; }
        int cityID;
        bool isCorner;

        
        public AthleteForm()
        {
            InitializeComponent();
            LoadcbPosition();

            this.btnClose.Click += (object sender, EventArgs e) =>
            {
               this.Hide();
               Scheduling schedulingScreen = new Scheduling();
               schedulingScreen.Show();
               addressID = AddressID;
           };// Lambda expression to create Close button click event
        }

        // Save Athlete and address
        private void btnSaveAthlete_Click(object sender, EventArgs e)
        {
            athleteID = Convert.ToInt32(lblCustID.Text);

            try
            {
                using (con)
                {
                    con.Open();

                    MySqlCommand cmd_GetCityID = new MySqlCommand("sp_getCityID", con);
                    MySqlCommand cmd_GetAddressID = new MySqlCommand("sp_getAddressID", con);

                    // Get City ID with zip
                    cmd_GetCityID.CommandType = CommandType.StoredProcedure;
                    cmd_GetCityID.Parameters.AddWithValue("@zip", cbZip.Text);
                    MySqlDataReader dr = cmd_GetCityID.ExecuteReader();

                    if (dr.Read())
                    {
                        cityID = Convert.ToInt32(dr["cityId"]);
                    }
                    dr.Close();

                    // Insert or update address Info
                    MySqlCommand cmd_addUpdateAddress = new MySqlCommand("sp_addUpdateAddress", con);
                    cmd_addUpdateAddress.CommandType = CommandType.StoredProcedure;
                    cmd_addUpdateAddress.Parameters.AddWithValue("@address_ID", addressID);
                    cmd_addUpdateAddress.Parameters.AddWithValue("@address1", tbAdd.Text);
                    cmd_addUpdateAddress.Parameters.AddWithValue("@city_ID", cityID);
                    cmd_addUpdateAddress.Parameters.AddWithValue("@zip", cbZip.Text);
                    cmd_addUpdateAddress.Parameters.AddWithValue("@phoneNumber", tbPhone.Text);
                    cmd_addUpdateAddress.ExecuteNonQuery();

                    // Get addressID with cityID and zip
                    cmd_GetAddressID.CommandType = CommandType.StoredProcedure;
                    cmd_GetAddressID.Parameters.AddWithValue("@city_ID", cityID);
                    cmd_GetAddressID.Parameters.AddWithValue("@zip", cbZip.Text);
                    cmd_GetAddressID.Parameters.AddWithValue("@AthleteAddress", tbAdd.Text);
                    cmd_GetAddressID.Parameters.AddWithValue("@AthletePhone", tbPhone.Text);
                    dr = cmd_GetAddressID.ExecuteReader();

                    if (dr.Read())
                    {
                        addressID = Convert.ToInt32(dr["addressId"]);

                    }
                    dr.Close();

                    // Insert or update athlete Info
                    MySqlCommand cmd_addUpdateAthlete = new MySqlCommand("sp_addUpdateAthlete", con);
                    cmd_addUpdateAthlete.Parameters.Clear();
                    cmd_addUpdateAthlete.CommandType = CommandType.StoredProcedure;
                    cmd_addUpdateAthlete.Parameters.AddWithValue("@AthlID", athleteID);
                    cmd_addUpdateAthlete.Parameters.AddWithValue("@AthlName", tbName.Text);
                    cmd_addUpdateAthlete.Parameters.AddWithValue("@AthlPosition", cbPosition.Text);
                    cmd_addUpdateAthlete.Parameters.AddWithValue("@AthlDiscipline", cbDiscipline.Text);
                    cmd_addUpdateAthlete.Parameters.AddWithValue("@addID", addressID);
                    cmd_addUpdateAthlete.ExecuteNonQuery();

                    MessageBox.Show("Athlete Added/Updated");// Feedback
                }
                con.Close();
                this.Hide();
                Scheduling schedulingScreen = new Scheduling();
                schedulingScreen.Show();
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

        // Conditions to allow save button to be enabled
        private bool AllowSave()
        {
            if (((string.IsNullOrEmpty(tbName.Text)) || (string.IsNullOrEmpty(tbPhone.Text)) || (string.IsNullOrEmpty(tbAdd.Text)) ||
                (string.IsNullOrEmpty(cbZip.Text))))
            {
                return false;
            }
            return true;
        }

        // Update City and country Texbox based on Zip
        private void cbCustZip_TextChanged(object sender, EventArgs e)
        {
            using (con)
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("sp_getAddressInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@zip", cbZip.Text);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    tbCity.Text = (dr["City"].ToString());
                    tbCountry.Text = (dr["Country"].ToString());
                }
            }
            con.Close();
        }

        // Validation check for empty Athlete name textbox
        private void tbAthleteName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                errorProvider1.SetError(tbName, "Please provide athlete's name.");
            }
            else
            {
                errorProvider1.SetError(tbName, null);
            }
        }

        // Validation check for empty Athlete phone textbox
        private void tbAthletePhone_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                errorProvider1.SetError(tbPhone, "Please provide athlete's Phone Number.");
            }
            else
            {
                errorProvider1.SetError(tbPhone, null);
            }
        
        }

        // Validation check for empty Athlete address textbox
        private void tbAthleteAddress_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAdd.Text))
            {

                errorProvider1.SetError(tbAdd, "Please provide athlete's Address.");
            }
            else
            {
                errorProvider1.SetError(tbAdd, null);
            }
            
        }

        // Check if save button is enabled
        private void tbAthleteName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                errorProvider1.SetError(tbName, "Please provide athlete's name.");
            }
            else
            {
                errorProvider1.SetError(tbName, null);
            }
            btnSaveCustomer.Enabled = AllowSave();
        }

        // Check if save button is enabled
        private void tbAthletePhone_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                errorProvider1.SetError(tbPhone, "Please provide athlete's Phone Number.");
            }
            else
            {
                errorProvider1.SetError(tbPhone, null);
            }
            btnSaveCustomer.Enabled = AllowSave();
        }

        // Check if save button is enabled
        private void tbAthleteAddress_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAdd.Text))
            {

                errorProvider1.SetError(tbAdd, "Please provide athlete's Address.");
            }
            else
            {
                errorProvider1.SetError(tbAdd, null);
            }
            btnSaveCustomer.Enabled = AllowSave();
        }

        // Check if save button is enabled
        private void cbAthleteZip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbZip.Text))
            {

                errorProvider1.SetError(cbZip, "Please provide athlete's Zip.");
            }
            else
            {
                errorProvider1.SetError(cbZip, null);
            }
            btnSaveCustomer.Enabled = AllowSave();
        }

        // Edit address information - enable textboxes
        private void btnEditAddress_Click(object sender, EventArgs e)
        {
            tbAdd.Enabled = true;
            tbPhone.Enabled = true;
            cbZip.Enabled = true;
            addressID = AddressID;
        }

        // Add new address btn - clear text
        private void btnAddAddress_Click(object sender, EventArgs e)
        {
            tbPhone.Text = "";
            tbAdd.Text = "";
            tbCity.Text = "";
            cbZip.SelectedIndex = -1;
            tbCountry.Text = "";

            tbAdd.Enabled = true;
            tbPhone.Enabled = true;
            cbZip.Enabled = true;
            addressID = 0;
        }

        private void SelectDiscipline()
        {
            if(cbPosition.Text == "Corner")
            {
                isCorner = true;
            }
            else
            {
                isCorner = false;
            }
            
        }

        private void LoadcbPosition()
        {
            cbPosition.Items.Add("Corner");
            cbPosition.Items.Add("Safety");
        }
        private void LoadcbDiscipline()
        {
            if (isCorner)
            {
                cbDiscipline.Items.Clear();
                cbDiscipline.Items.Add("Left");
                cbDiscipline.Items.Add("Right");

            }
            else
            {
                cbDiscipline.Items.Clear();
                cbDiscipline.Items.Add("Strong");
                cbDiscipline.Items.Add("Free");
            }
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectDiscipline();
            LoadcbDiscipline();
        }
    }
}
