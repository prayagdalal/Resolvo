using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace QnA.Models
{
    public class Notifications
    {
        public int Id { get; set; }

        public NotificationEvents NotificationEvents { get; set; }
        public int NotificationEventsId { get; set; }

        public User ByUser { get; set; }
        public int ByUserId { get; set; }

        public User ToUser { get; set; }
        public int ToUserId { get; set; }

        public bool Status { get; set; }

        public DateTime DateTime { get; set; }

        public Notifications()
        {
            DateTime = DateTime.Today;
        }

        [NotMapped]
        public List<NotificationParams> ParamsList { get; set; }

    }
}