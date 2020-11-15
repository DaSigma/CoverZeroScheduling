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
    public partial class CustomerForm : Form
    {
        MySqlConnection con = new MySqlConnection(@"server=3.227.166.251;user id=U04cRO;password=53688204070;persistsecurityinfo=True;database=U04cRO");
        int custID;
        int addressID;
        public static int AddressID { get; set; }
        int cityID;

        
        public CustomerForm()
        {
            InitializeComponent();

            this.btnClose.Click += (object sender, EventArgs e) =>
            {
               this.Hide();
               Scheduling schedulingScreen = new Scheduling();
               schedulingScreen.Show();
               addressID = AddressID;
           };// Lambda expression to create Close button click event
        }

        // Save Customer and address
        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            custID = Convert.ToInt32(lblCustID.Text);

            try
            {
                using (con)
                {
                    con.Open();

                    MySqlCommand cmd_GetCityID = new MySqlCommand("sp_getCityID", con);
                    MySqlCommand cmd_GetAddressID = new MySqlCommand("sp_getAddressID", con);

                    // Get City ID with zip
                    cmd_GetCityID.CommandType = CommandType.StoredProcedure;
                    cmd_GetCityID.Parameters.AddWithValue("@zip", cbCustZip.Text);
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
                    cmd_addUpdateAddress.Parameters.AddWithValue("@address1", tbCustAdd.Text);
                    cmd_addUpdateAddress.Parameters.AddWithValue("@city_ID", cityID);
                    cmd_addUpdateAddress.Parameters.AddWithValue("@zip", cbCustZip.Text);
                    cmd_addUpdateAddress.Parameters.AddWithValue("@phoneNumber", tbCustPhone.Text);
                    cmd_addUpdateAddress.ExecuteNonQuery();

                    // Get addressID with cityID and zip
                    cmd_GetAddressID.CommandType = CommandType.StoredProcedure;
                    cmd_GetAddressID.Parameters.AddWithValue("@city_ID", cityID);
                    cmd_GetAddressID.Parameters.AddWithValue("@zip", cbCustZip.Text);
                    cmd_GetAddressID.Parameters.AddWithValue("@custAddress", tbCustAdd.Text);
                    cmd_GetAddressID.Parameters.AddWithValue("@custPhone", tbCustPhone.Text);
                    dr = cmd_GetAddressID.ExecuteReader();

                    if (dr.Read())
                    {
                        addressID = Convert.ToInt32(dr["addressId"]);

                    }
                    dr.Close();

                    // Insert or update customr Info
                    MySqlCommand cmd_addUpdateCustomer = new MySqlCommand("sp_addUpdateCustomer", con);
                    cmd_addUpdateCustomer.Parameters.Clear();
                    cmd_addUpdateCustomer.CommandType = CommandType.StoredProcedure;
                    cmd_addUpdateCustomer.Parameters.AddWithValue("@custID", custID);
                    cmd_addUpdateCustomer.Parameters.AddWithValue("@custName", tbCustName.Text);
                    cmd_addUpdateCustomer.Parameters.AddWithValue("@addressID", addressID);
                    cmd_addUpdateCustomer.ExecuteNonQuery();

                    MessageBox.Show("Customer Added/Updated");// Feedback
                }
                con.Close();
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
            this.Hide();
            Scheduling schedulingScreen = new Scheduling();
            schedulingScreen.Show();
        }

        // Conditions to allow save button to be enabled
        private bool AllowSave()
        {
            if (((string.IsNullOrEmpty(tbCustName.Text)) || (string.IsNullOrEmpty(tbCustPhone.Text)) || (string.IsNullOrEmpty(tbCustAdd.Text)) ||
                (string.IsNullOrEmpty(cbCustZip.Text))))
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
                cmd.Parameters.AddWithValue("@zip", cbCustZip.Text);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    tbCustCity.Text = (dr["City"].ToString());
                    tbCustCountry.Text = (dr["Country"].ToString());
                }
            }
            con.Close();
        }

        // Validation check for empty Customer name textbox
        private void tbCustName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustName.Text))
            {
                errorProvider1.SetError(tbCustName, "Please provide customer name.");
            }
            else
            {
                errorProvider1.SetError(tbCustName, null);
            }
        }

        // Validation check for empty Customer phone textbox
        private void tbCustPhone_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustPhone.Text))
            {
                errorProvider1.SetError(tbCustPhone, "Please provide customer Phone Number.");
            }
            else
            {
                errorProvider1.SetError(tbCustPhone, null);
            }
        
        }

        // Validation check for empty Customer address textbox
        private void tbCustAdd_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustAdd.Text))
            {

                errorProvider1.SetError(tbCustAdd, "Please provide customer Address.");
            }
            else
            {
                errorProvider1.SetError(tbCustAdd, null);
            }
            
        }

        // Check if save button is enabled
        private void tbCustName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustName.Text))
            {
                errorProvider1.SetError(tbCustName, "Please provide customer name.");
            }
            else
            {
                errorProvider1.SetError(tbCustName, null);
            }
            btnSaveCustomer.Enabled = AllowSave();
        }

        // Check if save button is enabled
        private void tbCustPhone_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustPhone.Text))
            {
                errorProvider1.SetError(tbCustPhone, "Please provide customer Phone Number.");
            }
            else
            {
                errorProvider1.SetError(tbCustPhone, null);
            }
            btnSaveCustomer.Enabled = AllowSave();
        }

        // Check if save button is enabled
        private void tbCustAdd_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbCustAdd.Text))
            {

                errorProvider1.SetError(tbCustAdd, "Please provide customer Address.");
            }
            else
            {
                errorProvider1.SetError(tbCustAdd, null);
            }
            btnSaveCustomer.Enabled = AllowSave();
        }

        // Check if save button is enabled
        private void cbCustZip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbCustZip.Text))
            {

                errorProvider1.SetError(cbCustZip, "Please provide customer Zip.");
            }
            else
            {
                errorProvider1.SetError(cbCustZip, null);
            }
            btnSaveCustomer.Enabled = AllowSave();
        }

        // Edit address information - enable textboxes
        private void btnEditAddress_Click(object sender, EventArgs e)
        {
            tbCustAdd.Enabled = true;
            tbCustPhone.Enabled = true;
            cbCustZip.Enabled = true;
            addressID = AddressID;
        }

        // Add new address btn - clear text
        private void btnAddAddress_Click(object sender, EventArgs e)
        {
            tbCustPhone.Text = "";
            tbCustAdd.Text = "";
            tbCustCity.Text = "";
            cbCustZip.SelectedIndex = -1;
            tbCustCountry.Text = "";

            tbCustAdd.Enabled = true;
            tbCustPhone.Enabled = true;
            cbCustZip.Enabled = true;
            addressID = 0;
        }

    }
}
