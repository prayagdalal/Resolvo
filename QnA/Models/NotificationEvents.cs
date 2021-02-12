using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class NotificationEvents
    {
        public int Id { get; set; }

        public String Type { get; set; }

        public String Text { get; set; }

        public String QueryString { get; set; }
    }
}