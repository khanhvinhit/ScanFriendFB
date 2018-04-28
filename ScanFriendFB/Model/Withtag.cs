using System;

namespace ScanFriendFB
{
    public class Withtag : IUserFB
    {
        public string id { get; set; }

        public string name { get; set; }

        public string gender { get; set; }
        public string relationship_status { get; set; }
        public DateTime age_range { get; set; }

        public Withtag()
        {
        }

        public Withtag(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}