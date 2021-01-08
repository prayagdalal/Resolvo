using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace QnA.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Forum()
        {
            Upvote = 0;
            Downvote = 0;
            DateTime = DateTime.Now;
        }

        [NotMapped]
        public List<ForumTags> tags { get; set; }
        
        [NotMapped]
        public List<ForumVotes> ForumVotes { get; set; }
    }
}