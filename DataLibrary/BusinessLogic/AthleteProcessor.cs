using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public class AthleteProcessor
    {
        public static List<string> GetAthleteNames()
        {
            string sp = "sp_athleteName";
            List<string> athleteNames = new List<string>();
            athleteNames = MySQLDataAccess.LoadAthleteNameData(sp);
            return athleteNames;
        }
    }
}
