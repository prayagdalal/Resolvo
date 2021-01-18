using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        [NotMapped]
        public int CountQuestion { get; set; }
    }
}