using System;

namespace ScanFriendFB
{
    public class Reaction : IUserFB
    {
        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public DateTime age_range { get; set; }
        public string relationship_status { get; set; }

        public Reaction()
        {
        }

        public Reaction(string id, string name, string type)//, string p)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            //this.idP = p;
        }
    }
}