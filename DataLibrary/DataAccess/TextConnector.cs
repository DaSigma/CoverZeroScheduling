using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess
{
    class TextConnector : IDataConnection
    {
        public Appointment CreateAppointment(Appointment appointment)
        {
            return appointment;
        }
    }

}
