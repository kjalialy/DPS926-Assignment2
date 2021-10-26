using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
    public class TeamClass
    {
        public string teamId { get; set; }
        public string teamName { get; set; }

        public TeamClass(string id, string name)
        {
            this.teamId = id;
            this.teamName = name;
        }
    }
}
