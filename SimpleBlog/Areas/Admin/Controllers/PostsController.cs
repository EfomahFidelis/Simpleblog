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
         private const int PostPerPage = 8;


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

         // Creating a new post
        public ActionResult New()
        {
            return View("Form", new PostsForm 
            { 
                IsNew = true
            });
        }

         // Editing an Existing post
        public ActionResult Edit(int id)
        { 
            var post = Database.Session.Load<Post>(id);
                if(post == null)
                    return HttpNotFound();
            return View("Form", new PostsForm
            {
                 IsNew = false,
                PostId = id,
                Content = post.Content,
                Slug = post.Slug,
                Title = post.Title,
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User,
                };
            }
            else {

                post = Database.Session.Load<Post>(form.PostId);

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }

         // Trashing a post
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = DateTime.UtcNow;
            Database.Session.Update(post);
            return RedirectToAction("Index");
        }

        // Deleting a post
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            Database.Session.Delete(post);
            return RedirectToAction("Index");
        }

        // Restoring a post
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            post.DeletedAt = null;
            Database.Session.Update(post);
            return RedirectToAction("Index");
        }
    }
}