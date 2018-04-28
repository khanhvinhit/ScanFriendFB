using System;
using System.Collections.Generic;

namespace ScanFriendFB
{
    public class Post
    {
        public string id { get; set; }
        public DateTime created_time { get; set; }
        public string message { get; set; }
        public string story { get; set; }
        public string link { get; set; }
        public HashSet<Withtag> with_tag { get; set; }
        public HashSet<Reaction> reactions { get; set; }
        public HashSet<Comment> cmts { get; set; }

        public Post()
        {
        }

        public Post(string id, DateTime date, string message, string story, string link, HashSet<Withtag> with, HashSet<Reaction> reactions, HashSet<Comment> cmts)
        {
            this.id = id;
            this.created_time = date;
            this.message = message;
            this.story = story;
            this.link = link;
            this.with_tag = with;
            this.reactions = reactions;
            this.cmts = cmts;
        }
    }
}