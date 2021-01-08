using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnA.Models;

namespace QnA.Models
{
    public class ForumVotes
    {

        public int Id { get; set; }

        public Forum Forum { get; set; }
        public int ForumId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public bool UpVote { get; set; }

        public bool DownVote { get; set; }

        public ForumVotes()
        {
            UpVote = false;
            DownVote = false;
        }
    }
}