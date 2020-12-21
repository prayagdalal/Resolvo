using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Image { get; set; }
        public String CompanyName { get; set; }
        public DateTime CompanyFrom { get; set; }
        public DateTime ComapnyTo { get; set; }
        public String Education { get; set; }
        public DateTime EducationFrom { get; set; }
        public DateTime EducationtTo { get; set; }
        public String Github { get; set; }
        public String Website { get; set; }
    }
}