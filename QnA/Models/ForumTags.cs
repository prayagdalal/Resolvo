using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class ForumTags
    {
        public int Id { get; set; }

        public Forum Forum { get; set; }
        public int ForumId { get; set; }

        public Tag Tag { get; set; }
        public int TagId { get; set; }


    }
}