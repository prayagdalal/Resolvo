using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QnA.ViewModels;
using QnA.Models;

namespace QnA.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var viewModel = new QuestionViewModel
            {
                type = "Add",
                forum = new Forum()
            };
            return View(viewModel);
        }
    }
}