using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int AthleteID { get; set; }
        public string  AthleteName { get; set; }
        public int CoachID { get; set; }

        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Updated { get; set; }
    }
}
