using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
        public DateTime DateTime { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

    }
}