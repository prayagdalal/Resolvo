﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnA.Models;

namespace QnA.ViewModels
{
    public class QuestionViewModel
    {
        public Forum forum { get; set; }
        public String type { get; set; }
    }
}