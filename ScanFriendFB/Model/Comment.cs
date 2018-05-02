using System;

namespace ScanFriendFB
{
    public class Comment : IUserFB
    {
        public string id { get; set; }

        public string name { get; set; }

        public string message { get; set; }
        public string idmessage { get; set; }
        public string relationship_status { get; set; }
        public DateTime created_time { get; set; }
        public DateTime age_range { get; set; }

        public Comment()
        {
        }

        public Comment(string idmessage, string message, DateTime created_time, string id, string name)//, string p)
        {
            this.idmessage = idmessage;
            this.message = message;
            this.id = id;
            this.name = name;
            this.created_time = created_time;
            // this.idP = p;
        }
    }
}