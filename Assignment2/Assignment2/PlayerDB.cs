using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class PlayerDB
    {
        [PrimaryKey, AutoIncrement]
        public int dbID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personId { get; set; }
        public string teamId { get; set; }
        public string yearsPro { get; set; }
        public string fullName { get; set; }
        public string imageURL { get; set; }
        public string posFull { get; set; }
    }
}
