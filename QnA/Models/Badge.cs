using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public int Score { get; set; }
        public String BadgeType { get; set; }
    }
}