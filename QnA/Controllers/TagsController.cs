using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnA.Models;
using QnA.ViewModels;
using System.Web.Mvc;

namespace QnA.Controllers
{
    public class TagsController : Controller
    {
        // GET: Tags
        private QnAContext _context;

        public TagsController()
        {
            _context = new QnAContext();
        }
            
        //for user
        public ActionResult Index()
        {
            var Tags = _context.Tag.ToList();

            var viewModel = new TagViewModel
            {
                taglist = Tags
            };
            return View("User/Index",viewModel);
        }

        //for admin
        public ActionResult All()
        {
            var Tags = _context.Tag.ToList();

            var viewModel = new TagViewModel
            {
                taglist = Tags
            };
            return View("Admin/Index", viewModel);
        }

        public ActionResult Add()
        {
            var viewModel = new TagViewModel
            {
                tag = new Tag(),
                type = "Add"
            };
            return View("Admin/Add",viewModel);
        }

        public ActionResult Save(Tag tag)
        {
            if (tag.Id == 0)
            {
                _context.Tag.Add(tag);
            }
            else
            {
                var getTag = _context.Tag.Single(c => c.Id == tag.Id);

                getTag.Name = tag.Name;
                getTag.Description = tag.Description;
            }
            _context.SaveChanges();
            return RedirectToAction("All", "Tags");
        }

        public ActionResult Edit(int id)
        {
            var tag = _context.Tag.SingleOrDefault(c => c.Id == id);

            if (tag == null)
            {
                return HttpNotFound();
            }

            var viewModel = new TagViewModel { 
                tag=tag,
                type="Edit"
            };

            return View("Admin/Add", viewModel);

        }

        public ActionResult Delete(int id)
        {
            var tag = _context.Tag.SingleOrDefault(c => c.Id == id);

            if (tag == null)
            {
                return HttpNotFound();
            }

            _context.Tag.Remove(tag);
            _context.SaveChanges();
            return RedirectToAction("All", "Tags");

        }
    }
}