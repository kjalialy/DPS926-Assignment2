using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class TeamObj
    {
        [PrimaryKey, AutoIncrement]
        public int dbID { get; set; }

        public string city { get; set; }
        public string fullName { get; set; }
        public bool isNBAFranchise { get; set; }
        public string confName { get; set; }
        public string tricode { get; set; }
        public string teamShortName { get; set; }
        public string divName { get; set; }
        public bool isAllStar { get; set; }
        public string nickname { get; set; }
        public string urlName { get; set; }
        public string teamId { get; set; }
        public string altCityName { get; set; }
        public string imageURL { get; set; }
    }
}
