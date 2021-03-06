﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLibrary.DataAccess;
using DataLibrary.Models;
using MySql.Data;

namespace DataLibrary.BusinessLogic
{
    public static class CoachProcessor
    {
        public static void AddCoach(string userName, string password)
        {
            Coach newCoach = new Coach
            {
                UserName = userName,
                Password = password
            };

           MySQLConnector.SaveCoach(newCoach.UserName, newCoach.Password);
        }

        public static Coach Login(string userName, string password)
        {
            Coach coach = MySQLConnector.Login(userName, password);
            return coach;
        }

        public static DataTable GetCoachDataByDate(int coachID, string StartDate, string ToDate)
        {
            string sp = "sp_getApptsByDate"; 
            DataTable dt = MySQLConnector.GetData(sp, coachID, StartDate, ToDate);
            return dt;
        }

        public static List<string> GetCoachNames()
        {
            string sp = "sp_getDistinctCoach";
            List<string> coachNames = new List<string>();
            coachNames =  MySQLConnector.LoadCoachNamesData(sp);
            return coachNames;

        }

        public static StringBuilder GetCoachSchedule(string coachName)
        {
            var pg = new StringBuilder();
            string sp = "sp_getCoachSchedule";
            pg = MySQLConnector.LoadCoachScheduleData(coachName, sp);

            return pg;
        }

        public static StringBuilder GetAllCoachesAppointments()
        {
            string sp = "sp_getAllAppointments";
            var pg = new StringBuilder();
            pg = MySQLConnector.LoadAllCoachesAppointmentData(sp);

            return pg;
        }

        public static int GetCoachIDByName(string coachName)
        {
            string sp = "sp_getCoachIDbyName";
            int coachID;
            coachID = MySQLConnector.LoadCoachIDDataByName(coachName, sp);
            return coachID;
        }
    }
}
