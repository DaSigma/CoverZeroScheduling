using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public class AddressProcessor
    {
        public static List<string> GetDistinctZips()
        {
            List<string> distinctZips = new List<string>();
            string sp = "sp_distinctZip";

            distinctZips = MySQLDataAccess.LoadDistinctAddressZipData(sp);

            return distinctZips;
        }

        public static void DeleteAddress(int addressID)
        {
            string sp = "sp_deleteAddressbyID";
            MySQLDataAccess.DeleteAddressDataByID(addressID, sp);
        }

        public static void Insert_UpdateAddress(int addressID, string street, int cityID, string zip, string phoneNumber)
        {
            string sp = "sp_addUpdateAddress";
            MySQLDataAccess.Insert_UpdateAddressData(addressID, street, cityID, zip, phoneNumber, sp);
        }

        public static int GetAddressID(int cityID, string zip, string street, string phoneNumber)
        {
            int addressID;
            string sp = "sp_getAddressID";
            addressID = MySQLDataAccess.GetAddressIDData(cityID, zip, street, phoneNumber, sp);
            return addressID;
        }

        public static int GetCityID(string zip)
        {
            int cityID;
            string sp = "sp_getCityID";
            cityID = MySQLDataAccess.GetCityIDData(zip, sp);
            return cityID;

        }

        public static Address GetAddress(string zip)
        {
            string sp = "sp_getAddressInfo";
            Address address = new Address();
            address =  MySQLDataAccess.LoadCityCountryData(zip, sp);
            return address;
        }

        public static bool ValidatePhoneNumber(string phone)
        {
            var phoneNumber = phone.Trim()
                 .Replace(" ", "")
                 .Replace("-", "")
                 .Replace("(", "")
                 .Replace(")", "");
            var test = Regex.Match(phoneNumber, @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$").Success;
            return test;
        }

        public static string TrimNumber(string phoneNumber)
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
