using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string StreetAddress { get; set; }
        public int CityID { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public DateTime Updated { get; set; }


    }
}
