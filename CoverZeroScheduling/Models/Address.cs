using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoverZeroScheduling.Models
{
    public class Address
    {

        public int addressID;

        public int AddressID { get; set; }
        public string Street { get; set; }
        public int CityID { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }

        public Address(int addressID, string street, int cityID, string zip, string phoneNumber)
        {
            AddressID = addressID;
            Street = street;
            CityID = cityID;
            Zip = zip;
            PhoneNumber = phoneNumber;

        }

        public Address(string street, int cityID, string zip, string phoneNumber)
        {
            Street = street;
            CityID = cityID;
            Zip = zip;
            PhoneNumber = phoneNumber;

        }

        public static void Insert_UpdateAddress(int addressID, string street, int cityID, string zip, string phoneNumber)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            MySqlCommand cmd;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand("sp_addUpdateAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@address_ID", addressID);
                cmd.Parameters.AddWithValue("@address1", street);
                cmd.Parameters.AddWithValue("@city_ID", cityID);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                cmd.ExecuteNonQuery();
            }
        }

        // Get addressID with cityID and zip
        public static int GetAddressID(int cityID, string zip, string street, string phoneNumber)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString); ;
            MySqlCommand cmd;
            MySqlDataReader dr;
            int addressID;

            using (con)
            {
                con.Open();

                cmd = new MySqlCommand("sp_getAddressID", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@city_ID", cityID);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@AthleteAddress", street);
                cmd.Parameters.AddWithValue("@AthletePhone", phoneNumber);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    addressID = Convert.ToInt32(dr["addressId"]);
                    return addressID;
                }
                dr.Close();                                  
            }
            return 0;
        }

    }
}
