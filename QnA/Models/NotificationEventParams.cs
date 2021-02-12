using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class NotificationEventParams
    {
        public int Id { get; set; }

        public NotificationEvents NotificationEvents { get; set; }
        public int NotificationEventsId { get; set; }

        public String ParaName { get; set; }
    }
}