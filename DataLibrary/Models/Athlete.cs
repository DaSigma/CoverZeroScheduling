using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Athlete
    {
        public int AthleteID { get; set; }
        public string AthleteName { get; set; }
        public int AddressID { get; set; }
        public string AthletePosition { get; set; }
        public string AthleteDiscipline { get; set; }
        public string CornerDiscipline { get; set; }
        public string SafetyDiscipline { get; set; }
        public string ImageURL { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; internal set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public DateTime Updated { get; set; }
        
    }
}
