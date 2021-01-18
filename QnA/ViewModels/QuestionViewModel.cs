﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnA.Models;

namespace QnA.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public Forum forum { get; set; }
        public Thread thread { get; set; }
        public String type { get; set; }
        public List<Forum> forumList { get; set; }
        public List<Thread> threadList { get; set; }
        public String Tagname { get; set; }

    }
}