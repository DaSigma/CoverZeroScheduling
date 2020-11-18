using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverZeroScheduling.Models
{
    public class Corner : Athlete
    {
        public string CornerPosition { get; set; }

        public Corner(int athleteID, string athleteName, int addressID, string cornerPosition) : base(athleteID, athleteName, addressID)
        {
            CornerPosition = cornerPosition;
        }

    }
}
