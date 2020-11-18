using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverZeroScheduling.Models
{
    public class Athlete
    {
        public int AthleteID { get; set; }
        public string AthleteName { get; set; }
        public int AddressID { get; set; }


        public Athlete(int athleteID, string athleteName, int addressID)
        {
            AthleteID = athleteID;
            AthleteName = athleteName;
            AddressID = addressID;
        }

    }

}
