using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnA.Models;

namespace QnA.ViewModels
{
    public class TagViewModel
    {
        public String type { get; set; }
        public Tag tag { get; set; }
        public List<Tag> taglist { get; set; }
    }
}