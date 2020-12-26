using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class UserBadges
    {
        public int Id { get; set; }
        public Boolean Status { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Badge Badge { get; set; }
        public int BadgeId { get; set; }

        public int ObtainScore { get; set; }
    }
}