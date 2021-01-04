using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QnA.Models;
using QnA.ViewModels;

namespace QnA.Controllers
{
    public class BadgesController : Controller
    {
        private QnAContext _context;

        public BadgesController()
        {
            _context = new QnAContext();
        }


        public ActionResult All()
        {
            var Badges = _context.Badge.ToList();
            var viewModel = new BadgeViewModel
            {
                badgelist = Badges

            };
            return View("Admin/Index", viewModel);
        }

        public ActionResult Index()
        {
            var Badges = _context.Badge.ToList();
            var viewModel = new BadgeViewModel
            {
                badgelist = Badges

            };
            return View("User/Index",viewModel);
        }


        public ActionResult Add()
        {
            var viewModel = new BadgeViewModel
            {
                badge = new Badge()
            };
            return View("Admin/Add",viewModel);
        }

        public ActionResult Save(Badge badge)
        {
            if (badge.Id == 0)
            {
                _context.Badge.Add(badge);
            }
            else
            {
                var getBadge = _context.Badge.Single(c => c.Id == badge.Id);

                getBadge.Title = badge.Title;
                getBadge.Description = badge.Description;
                getBadge.Score = badge.Score;
                getBadge.BadgeType = badge.BadgeType;

            }
            _context.SaveChanges();
            return RedirectToAction("All", "Badges");
        }

        public ActionResult Edit(int id)
        {
            var badge = _context.Badge.SingleOrDefault(c => c.Id == id);

            if (badge == null)
            {
                return HttpNotFound();
            }

            var viewModel = new BadgeViewModel
            {
                badge = badge
            };

            return View("Admin/Add", viewModel);

        }

        public ActionResult Delete(int id)
        {
            var badge = _context.Badge.SingleOrDefault(c => c.Id == id);

            if (badge == null)
            {
                return HttpNotFound();
            }

            _context.Badge.Remove(badge);
            _context.SaveChanges();
            return RedirectToAction("All", "Badges");

        }
    }
}