using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class UserTags
    {
        public int Id { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Tag Tag { get; set; }
        public int TagId { get; set; }

    }
}