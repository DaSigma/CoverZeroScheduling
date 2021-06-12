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
using DataLibrary.Models;
using System.Configuration;
using System.Text.RegularExpressions;
using DataLibrary.BusinessLogic;

namespace DataLibrary
{
    public partial class AthleteForm : Form
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
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
               Schedule schedulingScreen = new Schedule();
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
                cityID = AddressProcessor.GetCityID(cbZip.Text);
                addressID = AddressProcessor.GetAddressID(cityID, cbZip.Text, tbAdd.Text, tbPhone.Text);
                AddressProcessor.Insert_UpdateAddress(addressID, tbAdd.Text, cityID, cbZip.Text, tbPhone.Text);

                addressID = AddressProcessor.GetAddressID(cityID, cbZip.Text, tbAdd.Text, tbPhone.Text);

                SelectDiscipline();
                if (isCorner)
                {
                    AthleteProcessor.InsertUpdateCorner(athleteID, tbName.Text, cbPosition.Text, cbDiscipline.Text, addressID);
                }
                else
                {
                    AthleteProcessor.InsertUpdateSafety(athleteID, tbName.Text, cbPosition.Text, cbDiscipline.Text, addressID);
                }

                MessageBox.Show("Athlete Added/Updated");// Feedback

                this.Hide();
                Schedule schedulingScreen = new Schedule();
                schedulingScreen.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// print error message
            }
        }

        // Conditions to allow save button to be enabled
        private bool AllowSave()
        {
            if (((string.IsNullOrEmpty(tbName.Text)) || (ValidatePhoneNumber(tbPhone.Text) == false) || (string.IsNullOrEmpty(tbAdd.Text)) ||
                (string.IsNullOrEmpty(cbZip.Text))))
            {
                return false;
            }
            else
            {
                return true;
            }
            
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
            if (ValidatePhoneNumber(tbPhone.Text))
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


        private bool ValidatePhoneNumber(string phone)
        {
            var phoneNumber = phone.Trim()
                 .Replace(" ", "")
                 .Replace("-", "")
                 .Replace("(", "")
                 .Replace(")", "");
            var test = Regex.Match(phoneNumber, @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$").Success; 
            return test;
        }

        private void tbPhone_Leave(object sender, EventArgs e)
        {

            if (ValidatePhoneNumber(tbPhone.Text) == false)
            {
                errorProvider1.SetError(tbPhone, "Please provide athlete's Phone Number.");

            }
            else
            {
                errorProvider1.SetError(tbPhone, null);
                tbPhone.Text = TrimNumber(tbPhone.Text);
            }
            
        }

        private string TrimNumber(string phoneNumber)
        {
            string trimmed = phoneNumber.Replace(" ", "")
                                        .Replace("-", "")
                                        .Replace("(", "")
                                        .Replace(")", "");
            string test = String.Format("{0:(###) ###-####}", Convert.ToInt64(trimmed));
            return test;
        }
    }
}
