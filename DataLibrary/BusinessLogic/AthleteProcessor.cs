﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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
            athleteNames = MySQLConnector.LoadAthleteNameData(sp);
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

            currentAthlete = MySQLConnector.LoadAthleteDataByID(IsCorner, athleteID, sp);
            return currentAthlete;

        }

        public static void DeleteAthleteByID(int athleteID)
        {
            string sp = "sp_AthleteDeletebyID";
            MySQLConnector.DeleteAthleteDataByID(athleteID, sp);            
        }

        public static bool CheckAssociation(int athleteID)
        {
            if (MySQLConnector.CheckAthleteAssociationData(athleteID))
            {
                return true;
            }
            return false;
                        
        }
        public static DataTable GetAthletes()
        {
            string sp = "sp_viewAllAthletes";
            DataTable dt = new DataTable();
            dt = MySQLConnector.LoadAllAthletesData(sp);
            return dt;
        }

        public static StringBuilder GetDBReports(string sp, string athleteDiscipline)
        {
            var pg = new StringBuilder();
            pg = MySQLConnector.LoadDBReportData(sp, athleteDiscipline);
            return pg;
        }

        public static void InsertUpdateCorner(int athleteID, string athleteName, string athletePostition, string AthleteDiscipline, int addressID)
        {
            string sp = "sp_addUpdateCorner";
            MySQLConnector.Insert_UpdateCornerData(athleteID, athleteName, athletePostition, AthleteDiscipline, addressID, sp);
        }

        public static void InsertUpdateSafety(int athleteID, string athleteName, string athletePostition, string AthleteDiscipline, int addressID)
        {
            string sp = "sp_addUpdateSafety";
            MySQLConnector.Insert_UpdateSafetyData(athleteID, athleteName, athletePostition, AthleteDiscipline, addressID, sp);
        }

        public static System.Drawing.Image ConvertToImage(string url)
        {
            if (url != null && url != string.Empty)
            {
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(url);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                return img;
            }
            return null;
        }
    }
}
