using System;
using System.Collections.Generic;
using System.Data;
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

        public static string SetSp_AthleteID(bool isCorner)
        {
            string sp_AthleteByID;
            if (isCorner)
            {
                sp_AthleteByID = "sp_viewEditCornerByID";

            }
            else
            {
                sp_AthleteByID = "sp_viewEditSafetyByID"; ;
            }
            return sp_AthleteByID;
        }
        public static string SetAthleteDiscipline(bool isCorner)
        {
            string athleteDiscipline;
            if (isCorner)
            {
                athleteDiscipline = "cornerDiscipline";
            }
            else
            {
                athleteDiscipline = "safetyDiscipline";
            }
            return athleteDiscipline;
        }

        public static Athlete GetAthleteByID(bool IsCorner, int athleteID)
        {
            Athlete currentAthlete = new Athlete();
            string sp = SetSp_AthleteID(IsCorner);

            currentAthlete = MySQLDataAccess.LoadAthleteDataByID(IsCorner, athleteID, sp);
            return currentAthlete;

        }

        public static void DeleteAthleteByID(int athleteID)
        {
            string sp = "sp_AthleteDeletebyID";
            MySQLDataAccess.DeleteAthleteDataByID(athleteID, sp);            
        }

        public static bool CheckAssociation(int athleteID)
        {
            if (MySQLDataAccess.CheckAthleteAssociationData(athleteID))
            {
                return true;
            }
            return false;
                        
        }
        public static DataTable GetAthletes()
        {
            string sp = "sp_viewAllAthletes";
            DataTable dt = new DataTable();
            dt = MySQLDataAccess.LoadAllAthletesData(sp);
            return dt;
        }

        public static StringBuilder GetDBReports(string sp, string athleteDiscipline)
        {
            var pg = new StringBuilder();
            pg = MySQLDataAccess.LoadDBReportData(sp, athleteDiscipline);
            return pg;
        }

        public static void InsertUpdateCorner(int athleteID, string athleteName, string athletePostition, string AthleteDiscipline, int addressID)
        {
            string sp = "sp_addUpdateCorner";
            MySQLDataAccess.Insert_UpdateCornerData(athleteID, athleteName, athletePostition, AthleteDiscipline, addressID, sp);
        }

        public static void InsertUpdateSafety(int athleteID, string athleteName, string athletePostition, string AthleteDiscipline, int addressID)
        {
            string sp = "sp_addUpdateSafety";
            MySQLDataAccess.Insert_UpdateSafetyData(athleteID, athleteName, athletePostition, AthleteDiscipline, addressID, sp);
        }
    }
}
