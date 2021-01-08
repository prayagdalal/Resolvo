using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnA.Models;

namespace QnA.Models
{
    public class ThreadVotes
    {

        public int Id { get; set; }

        public Thread Thread { get; set; }
        public int ThreadId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public bool UpVote { get; set; }

        public bool DownVote { get; set; }

        public ThreadVotes()
        {
            UpVote = false;
            DownVote = false;
        }
    }
}