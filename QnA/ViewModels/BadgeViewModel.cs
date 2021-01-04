using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnA.Models;

namespace QnA.ViewModels
{
    public class BadgeViewModel
    {
        public Badge badge { get; set; }
        public List<Badge> badgelist { get; set; }
    }
}