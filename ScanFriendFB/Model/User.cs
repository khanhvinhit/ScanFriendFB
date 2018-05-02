using System;

namespace ScanFriendFB
{
    public class User : IUserFB
    {
        public string id { get; set; }

        public string name { get; set; }

        public int likes { get; set; }

        public int cmts { get; set; }

        public string relationship_status { get; set; }
        public DateTime age_range { get; set; }

        public int sum
        {
            get
            {
                return likes + cmts;
            }
        }

        public User()
        {
        }

        public User(string id, string name, string relationship_status, DateTime age_range)
        {
            this.relationship_status = relationship_status;
            this.age_range = age_range;
            this.id = id;
            this.name = name;
        }

        public User(string id, string name, string relationship_status, DateTime age_range, int like, int cmt)
        {
            this.id = id;
            this.name = name;
            this.relationship_status = relationship_status;
            this.age_range = age_range;
            this.likes = like;
            this.cmts = cmt;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}