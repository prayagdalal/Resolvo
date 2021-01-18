using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QnA.ViewModels;
using QnA.Models;
using System.Data.Entity;
using System.Web.Script.Serialization;

/*using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;*/

namespace QnA.Controllers
{
    public class Flag {
        public int statusflag { get; set; }
        public int vote { get; set; }
    }

    public class QuestionController : Controller
    {
        // GET: Forum
        int loginuserid = 3;

        private QnAContext _context;
        public QuestionController()
        {
            _context = new QnAContext();
        }

        //----------------------------------------------------------forum----------------------------------------------------------------//
        //----------------------------------------------------------forum----------------------------------------------------------------//
        //----------------------------------------------------------forum----------------------------------------------------------------//

        //user interest
        public ActionResult Index()
        {
            List<int> userTags = new List<int>();
            List<int> forumList = new List<int>();

            var getTags = _context.UserTags.Where(c => c.UserId == loginuserid).ToList();

            foreach (var data in getTags)
            {
                userTags.Add(data.TagId);
            }

            var getForumTags = _context.ForumTags.Where(c => userTags.Contains(c.TagId)).GroupBy(c => c.ForumId);

            foreach(var data in getForumTags)
            {
                forumList.Add(data.Key);
            }

            var getQuestions = _context.Forum.Include(c => c.User).Where(c => forumList.Contains(c.Id)).ToList();

            foreach(var data in getQuestions)
            {
                var getQuestionTags = _context.ForumTags.Include(c => c.Tag).Include(c => c.Forum).Where(c => c.ForumId == data.Id).ToList();
                data.tags = getQuestionTags;
            }

            foreach (var data in getQuestions)
            {
                var threadlist = _context.Thread.Include(c => c.Forum).Include(c => c.User).Where(c => c.ForumId == data.Id).Where(c => c.ParentThreadId == 0).ToList();
                data.threadlist = threadlist;
            }

            var viewModel = new QuestionViewModel
            {
                forumList = getQuestions
            };

            return View(viewModel);

        }

        //all
        public ActionResult All()
        {
            var getQuestions = _context.Forum.Include(c => c.User).ToList();

            foreach (var data in getQuestions)
            {
                var getQuestionTags = _context.ForumTags.Include(c => c.Tag).Include(c => c.Forum).Where(c => c.ForumId == data.Id).ToList();
                data.tags = getQuestionTags;
            }

            foreach (var data in getQuestions)
            {
                var threadlist = _context.Thread.Include(c => c.Forum).Include(c => c.User).Where(c => c.ForumId == data.Id).Where(c => c.ParentThreadId == 0).ToList();
                data.threadlist = threadlist;
            }

            var viewModel = new QuestionViewModel
            {
                forumList = getQuestions
            };

            return View(viewModel);

        }


        public ActionResult Add()
        {
            var viewModel = new QuestionViewModel
            {
                type = "Add",
                forum = new Forum(),
                Id = loginuserid
            };
            return View(viewModel);
        }


        public ActionResult AddForum(int[] tags,Forum forum)
        {
            _context.Forum.Add(forum);
            _context.SaveChanges();

            var forumId = forum.Id;
            for(var i = 0; i < tags.Length; i++)
            {
                var forumTags = new ForumTags();
                forumTags.ForumId = forumId;
                forumTags.TagId = tags[i];
                _context.ForumTags.Add(forumTags);
                _context.SaveChanges();
            }
            return Json("Done");
        }

        public ActionResult EditForum(int[] tags,Forum forum)
        {
            var getForum = _context.Forum.Single(c => c.Id == forum.Id);
            getForum.Title= forum.Title;
            getForum.Body = forum.Body;
            getForum.ModifyDateTime = DateTime.Today;
            _context.SaveChanges();


            var getTags = _context.ForumTags.Where(c => c.ForumId == forum.Id).ToList();
            _context.ForumTags.RemoveRange(getTags);
            _context.SaveChanges();
            
            for (var i = 0; i < tags.Length; i++)
            {
                var forumTags = new ForumTags();
                forumTags.ForumId = forum.Id;
                forumTags.TagId = tags[i];
                _context.ForumTags.Add(forumTags);
                _context.SaveChanges();
            }

            return Json("Done");

        }

        public ActionResult DeleteForum(int ForumID)
        {
            var getForum = _context.Forum.SingleOrDefault(c => c.Id == ForumID);

            if (getForum == null)
            {
                return HttpNotFound();
            }

            _context.Forum.Remove(getForum);
            _context.SaveChanges();
            return Json("Done");

        }

        public void UpdateForumVotes(int ForumId)
        {
            var UpVotes = _context.ForumVotes.Where(c => c.UpVote == true).Where(c => c.ForumId == ForumId).ToList();
            var DownVotes = _context.ForumVotes.Where(c => c.DownVote == true).Where(c => c.ForumId == ForumId).ToList();

            var getThread = _context.Forum.Single(c => c.Id == ForumId);
            getThread.Upvote = UpVotes.Count;
            getThread.Downvote = DownVotes.Count;
            _context.SaveChanges();

        }

        public ActionResult ForumUpVote(int ForumId)
        {
            //1-add
            //2-delete
            //3-update
            //4-none
            int statusflag;
            var getThread = _context.Forum.SingleOrDefault(c => c.Id == ForumId);
            if (getThread.UserId != loginuserid)
            {
                var getUserVote = _context.ForumVotes.Where(c => c.ForumId == ForumId).Where(c => c.UserId == loginuserid).SingleOrDefault();

                if (getUserVote == null)
                {
                    var voteObj = new ForumVotes();
                    voteObj.ForumId = ForumId;
                    voteObj.UserId = loginuserid;
                    voteObj.UpVote = true;
                    _context.ForumVotes.Add(voteObj);
                    statusflag = 1;
                }
                else
                {
                    if (getUserVote.UpVote)
                    {
                        _context.ForumVotes.Remove(getUserVote);
                        statusflag = 2;
                    }
                    else
                    {
                        getUserVote.UpVote = true;
                        getUserVote.DownVote = false;
                        statusflag = 3;
                    }
                }
                _context.SaveChanges();
                UpdateForumVotes(ForumId);
            }
            else
            {
                statusflag = 4;
            }

            var getThreadVote = _context.Forum.Single(c => c.Id == ForumId);
            var up = getThreadVote.Upvote;
            var down = getThreadVote.Downvote;
            var vote = up - down;

            var obj = new Flag();
            obj.statusflag = statusflag;
            obj.vote = vote;
           
            return Json(obj);


        }

        public ActionResult ForumDownVote(int ForumId)
        {
            //1-add
            //2-delete
            //3-update
            //4-none
            int statusflag;
            var getThread = _context.Forum.SingleOrDefault(c => c.Id == ForumId);
            if (getThread.UserId != loginuserid)
            {
                var getUserVote = _context.ForumVotes.Where(c => c.ForumId == ForumId).Where(c => c.UserId == loginuserid).SingleOrDefault();

                if (getUserVote == null)
                {
                    var voteObj = new ForumVotes();
                    voteObj.ForumId = ForumId;
                    voteObj.UserId = loginuserid;
                    voteObj.DownVote = true;
                    _context.ForumVotes.Add(voteObj);
                    statusflag = 1;
                }
                else
                {
                    if (getUserVote.DownVote)
                    {
                        _context.ForumVotes.Remove(getUserVote);
                        statusflag = 2;
                    }
                    else
                    {
                        getUserVote.DownVote = true;
                        getUserVote.UpVote = false;
                        statusflag = 3;
                    }
                }
                _context.SaveChanges();
                UpdateForumVotes(ForumId);
            }
            else
            {
                statusflag = 4;
            }

            var getThreadVote = _context.Forum.Single(c => c.Id == ForumId);
            var up = getThreadVote.Upvote;
            var down = getThreadVote.Downvote;
            var vote = up - down;

            var obj = new Flag();
            obj.statusflag = statusflag;
            obj.vote = vote;

            return Json(obj);
        }



        //----------------------------------------------------------Thread----------------------------------------------------------------//
        //----------------------------------------------------------Thread----------------------------------------------------------------//
        //----------------------------------------------------------Thread----------------------------------------------------------------//



        [HttpGet]
        public ActionResult Thread(int id=1)
        {
            //forum
            var getQuestion = _context.Forum.Include(c => c.User).Single(c => c.Id == id);
            var getQuestionTags = _context.ForumTags.Include(c => c.Tag).Include(c => c.Forum).Where(c => c.ForumId == id).ToList();
            getQuestion.tags = getQuestionTags;
            var getForumVotes = _context.ForumVotes.Include(c => c.Forum).Include(c => c.User).Where(c => c.ForumId == id).ToList();
            getQuestion.ForumVotes = getForumVotes;

            //thread
            var getThreads = _context.Thread.Include(c => c.Forum).Include(c => c.User).Where(c => c.ForumId == id).Where(c => c.ParentThreadId == 0).ToList();
            foreach(var data in getThreads)
            {
                var getThreadVotes = _context.ThreadVotes.Include(c => c.Thread).Include(c => c.User).Where(c => c.ThreadId == data.Id).ToList();
                data.ThreadVotes = getThreadVotes;
                var commentList = _context.Thread.Include(c => c.Forum).Include(c => c.User).Where(c => c.ParentThreadId == data.Id).ToList();
                data.CommentList = commentList;

            }

            var ViewModels = new QuestionViewModel
            {
                Id = 3,
                forum = getQuestion,
                threadList = getThreads
            };

            return View(ViewModels);     
        }


        public void UpdateThreadVotes(int ThreadId)
        {
            var UpVotes = _context.ThreadVotes.Where(c => c.UpVote == true).Where(c => c.ThreadId == ThreadId).ToList();
            var DownVotes = _context.ThreadVotes.Where(c => c.DownVote == true).Where(c => c.ThreadId == ThreadId).ToList();

            var getThread = _context.Thread.Single(c => c.Id == ThreadId);
            getThread.Upvote = UpVotes.Count;
            getThread.Downvote = DownVotes.Count;
            _context.SaveChanges();

        }

        public ActionResult ThreadUpVote(int ThreadId)
        {
            //1-add
            //2-delete
            //3-update
            //4-none
            int statusflag;
            var getThread = _context.Thread.SingleOrDefault(c => c.Id == ThreadId);
            if(getThread.UserId != loginuserid)
            {
                var getUserVote = _context.ThreadVotes.Where(c => c.ThreadId == ThreadId).Where(c => c.UserId == loginuserid).SingleOrDefault();

                if (getUserVote == null)
                {
                    var voteObj = new ThreadVotes();
                    voteObj.ThreadId = ThreadId;
                    voteObj.UserId = loginuserid;
                    voteObj.UpVote = true;
                    _context.ThreadVotes.Add(voteObj);
                    statusflag = 1;
                }
                else
                {
                    if (getUserVote.UpVote)
                    {
                        _context.ThreadVotes.Remove(getUserVote);
                        statusflag = 2;
                    }
                    else
                    {
                        getUserVote.UpVote = true;
                        getUserVote.DownVote = false;
                        statusflag = 3;
                    }
                }
                _context.SaveChanges();
                UpdateThreadVotes(ThreadId);
            }
            else
            {
                statusflag = 4;
            }

            var getThreadVote = _context.Thread.Single(c => c.Id == ThreadId);
            var up = getThreadVote.Upvote;
            var down = getThreadVote.Downvote;
            var vote = up - down;

            var obj = new Flag();
            obj.statusflag = statusflag;
            obj.vote = vote;

            return Json(obj);
        }

        public ActionResult ThreadDownVote(int ThreadId)
        {
            //1-add
            //2-delete
            //3-update
            //4-none
            int statusflag;
            var getThread = _context.Thread.SingleOrDefault(c => c.Id == ThreadId);
            if (getThread.UserId != loginuserid)
            {
                var getUserVote = _context.ThreadVotes.Where(c => c.ThreadId == ThreadId).Where(c => c.UserId == loginuserid).SingleOrDefault();

                if (getUserVote == null)
                {
                    var voteObj = new ThreadVotes();
                    voteObj.ThreadId = ThreadId;
                    voteObj.UserId = loginuserid;
                    voteObj.DownVote = true;
                    _context.ThreadVotes.Add(voteObj);
                    statusflag = 1;
                }
                else
                {
                    if (getUserVote.DownVote)
                    {
                        _context.ThreadVotes.Remove(getUserVote);
                        statusflag = 2;
                    }
                    else
                    {
                        getUserVote.DownVote = true;
                        getUserVote.UpVote = false;
                        statusflag = 3;
                    }
                }
                _context.SaveChanges();
                UpdateThreadVotes(ThreadId);
            }
            else
            {
                statusflag = 4;
            }

            var getThreadVote = _context.Thread.Single(c => c.Id == ThreadId);
            var up = getThreadVote.Upvote;
            var down = getThreadVote.Downvote;
            var vote = up - down;

            var obj = new Flag();
            obj.statusflag = statusflag;
            obj.vote = vote;

            return Json(obj);
        }

        public ActionResult AddThread(Thread thread)
        {
            _context.Thread.Add(thread);
            _context.SaveChanges();
            return Json("done");
        }

        [HttpGet]
        public ActionResult EditThread(int threadid)
        {
            var thread = _context.Thread.SingleOrDefault(c => c.Id == threadid);

            var viewModel = new QuestionViewModel { 
                thread = thread
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditThread(Thread thread)
        {
            var getThread = _context.Thread.Single(c => c.Id == thread.Id);
            getThread.Body = thread.Body;
            getThread.ModifyDateTime = DateTime.Today;

            _context.SaveChanges();
            return Json("done");
        }

        public ActionResult DeleteThread(int ThreadId)
        {
            var getThread = _context.Thread.SingleOrDefault(c => c.Id == ThreadId);

            if (getThread == null)
            {
                return HttpNotFound();
            }

            _context.Thread.Remove(getThread);
            _context.SaveChanges();
            return Json("done");
        }

        public ActionResult AddComment(Thread thread)
        {
            var Thread = new Thread();
            Thread.ParentThreadId = thread.ParentThreadId;
            Thread.Body = thread.Body;
            Thread.ForumId = thread.ForumId;
            Thread.UserId = thread.UserId;
            _context.Thread.Add(Thread);
            _context.SaveChanges();

            var getThread = _context.Thread.Include(c => c.User).SingleOrDefault(c => c.Id == Thread.Id);
            return Json(getThread);
        }

        public ActionResult DeleteComment(int ThreadId)
        {
            var getThread = _context.Thread.SingleOrDefault(c => c.Id == ThreadId);

            if (getThread == null)
            {
                return HttpNotFound();
            }

            _context.Thread.Remove(getThread);
            _context.SaveChanges();

            return Json(true);
        }

        public ActionResult sample()
        {
            return View();
        }

        public ActionResult test(int[] unid,Thread thread)
        {
            return Json(new { 
                unid = unid,
                thread = thread
            });
        }

        public ActionResult Tag(int tagId)
        {
            List<int> forumList = new List<int>();

            var getForumTags = _context.ForumTags.Where(c => c.TagId == tagId).ToList();
            foreach (var data in getForumTags)
            {
                forumList.Add(data.ForumId);
            }

            var getQuestions = _context.Forum.Include(c => c.User).Where(c => forumList.Contains(c.Id)).ToList();


            foreach (var data in getQuestions)
            {
                var getQuestionTags = _context.ForumTags.Include(c => c.Tag).Include(c => c.Forum).Where(c => c.ForumId == data.Id).ToList();
                data.tags = getQuestionTags;
            }
            foreach (var data in getQuestions)
            {
                var threadlist = _context.Thread.Include(c => c.Forum).Include(c => c.User).Where(c => c.ForumId == data.Id).Where(c => c.ParentThreadId == 0).ToList();
                data.threadlist = threadlist;
            }
            var getTagname = _context.Tag.SingleOrDefault(c => c.Id == tagId);

            var viewModel = new QuestionViewModel
            {
                forumList = getQuestions,
                Tagname = getTagname.Name

            };

            return View(viewModel);
        }


    }
}