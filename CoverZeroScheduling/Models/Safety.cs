using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverZeroScheduling.Models
{
    public class Safety:Athlete
    {
        public string SafetyPosition { get; set; }

        public Safety(int athleteID, string athleteName, int addressID, string safetyPosition) : base(athleteID, athleteName, addressID)
        {
            SafetyPosition = safetyPosition;
        }

    }

}
