using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels;

namespace SimpleBlog.Areas.Admin.Controllers
{
     [Authorize(Roles = "admin")]
     [SelectedTab("posts")]
    public class PostsController: Controller
    {
         private const int PostPerPage = 5;


        public ActionResult Index(int page = 1)
        {
            // Get how many total Post in the Database
            var totalPostCount = Database.Session.Query<Post>().Count();

            // Get the currentPage
            var currentPostPage = Database.Session.Query<Post>()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToList();


            return View(new PostsIndex 
            { 
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostPerPage)
            });
        }
    }
}