using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace QnA.Models
{
    public class Thread
    {
        public int Id { get; set; }
        public int ParentThreadId { get; set; }
        public String Body { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public bool IsAccepted { get; set; }

        public Forum Forum { get; set; }
        public int ForumId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Thread()
        {
            Upvote = 0;
            Downvote = 0;
            IsAccepted = false;
            DateTime = DateTime.Today;
        }

        [NotMapped]
        public List<ThreadVotes> ThreadVotes { get; set; }

        [NotMapped]
        public List<Thread> CommentList { get; set; }


    }
}