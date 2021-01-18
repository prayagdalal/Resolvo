using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QnA.ViewModels;
using QnA.Models;
using System.Web.Security;
using System.Data.Entity;

namespace QnA.Controllers
{
    public class UserController : Controller
    {
        private QnAContext _context;

        public UserController()
        {
            _context = new QnAContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //forUser

        public ActionResult Index()
        {
            var users = _context.User.ToList();
            var viewModel = new UserViewModel
            {
                userList = users
            };
            return View("User/Index", viewModel);
        }



        //for admin
        public ActionResult All()
        {
            var users = _context.User.ToList();
            var viewModel = new UserViewModel
            {
                userList = users
            };
            return View("Admin/Index", viewModel);
        }

        public ActionResult Add()
        {
            var viewModel = new UserViewModel
            {
                type = "Add",
                user = new User()
            };
            return View("Admin/Add", viewModel);
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
            return RedirectToAction("All", "User");
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

            return RedirectToAction("All", "User");

        }


        /* [Route("User/{id}/{name}")]*/
        public ActionResult Detail(int id)
        {
            return Content("workin");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string email, string password)
        {
            var res = _context.User.SingleOrDefault(u => u.Email == email && u.Password == password);
            if (res != null)
            {
                FormsAuthentication.SetAuthCookie(res.Email, false);
                return Json(res);
            }
            else
            {
                return Json("invalid user name or password");
            }


        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(int[] tags, User user)
        {
            var recordCount = _context.User.Count(a => a.Email == user.Email);
            if (recordCount >= 1)
            {
                return Json("Email Already Exist");
            }
            else
            {
                var User = new User();
                User.Name = user.Name;
                User.Email = user.Email;
                User.Password = user.Password;
                _context.User.Add(User);
                _context.SaveChanges();

                for (int i = 0; i < tags.Length; i++)
                {
                    var userTags = new UserTags();
                    userTags.UserId = User.Id;
                    userTags.TagId = tags[i];
                    _context.UserTags.Add(userTags);
                    _context.SaveChanges();

                }
                return RedirectToAction("All", "User");

            }


        }


        [HttpPost]
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
    }
}