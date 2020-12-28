using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QnA.ViewModels;
using QnA.Models;

namespace QnA.Controllers
{
    public class UserController : Controller
    {
        private QnAContext _context;

        public UserController()
        {
            _context = new QnAContext();
        }
        // GET: User
        public ActionResult Index()
        {
            var users = _context.User.ToList();
            var viewModel = new UserViewModel
            {
                userList = users 
            };
            return View("Admin/Index",viewModel);
        }

        public ActionResult Add()
        {
            var viewModel = new UserViewModel
            {
                type = "Add",
                user = new User()
            };
            return View("Admin/Add",viewModel);
        }

        [HttpPost]
        public ActionResult Save(User user)
        {
            if (user.Id == 0)
            {
                _context.User.Add(user);
            }
            else
            {
                var getUser = _context.User.Single(c => c.Id == user.Id);
                getUser.Name = user.Name;
                getUser.Email = user.Email;
                getUser.Password = user.Password;
                getUser.Type = user.Type;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        public ActionResult Edit(int id)
        {
            var user = _context.User.SingleOrDefault(c => c.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var viewModel = new UserViewModel
            {
                user = user,
                type = "Edit"
            };

            return View("Admin/Add", viewModel);

        }

        public ActionResult Delete(int id)
        {
            var user = _context.User.SingleOrDefault(c => c.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            _context.User.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index","User");

        }


        /* [Route("User/{id}/{name}")]*/
        public ActionResult Detail(int id)
        {
            return Content("workin");
        }
    }
}