using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class NotificationParams
    {
        public int Id { get; set; }

        public Notifications Notifications { get; set; }
        public int NotificationsId { get; set; }

        public NotificationEventParams NotificationEventParams { get; set; }
        public int NotificationEventParamsId { get; set; }

        public String ParaValue { get; set; }
    }
}