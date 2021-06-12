using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;

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
    }
}
