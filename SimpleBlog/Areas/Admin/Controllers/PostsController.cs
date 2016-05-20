using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.Infrastructure;
using SimpleBlog.Infrastructure.Extension;
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

            //
            var baseQuery = Database.Session.Query<Post>().OrderByDescending(f => f.CreatedAt);

            // pagination SQL
            var postIds = baseQuery
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .Select(p => p.Id)
                .ToArray();

            // (Data Retrieval) GetAll for the currentPage
            var currentPostPage = baseQuery
                .Where(p => postIds.Contains(p.Id))
                .FetchMany(f => f.Tags)
                .Fetch(f => f.User)
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
                IsNew = true,
                Tags = Database.Session.Query<Tag>().Select(tag => new TagCheckbox 
                { 
                   Id = tag.Id,
                   Name = tag.Name,
                   IsChecked = false
                }).ToList()
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
                 Tags = Database.Session.Query<Tag>().Select(tag => new TagCheckbox
                 {
                     Id = tag.Id,
                     Name = tag.Name,
                     IsChecked = post.Tags.Contains(tag)
                 }).ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var selectedTags = ReconsileTags(form.Tags).ToList();


            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User,
                };

                foreach(var tag in selectedTags)
                {
                    post.Tags.Add(tag);
                }
            }
            else {

                post = Database.Session.Load<Post>(form.PostId);

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;

                foreach (var toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                    post.Tags.Add(toAdd);

                foreach (var toRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToList())
                    post.Tags.Remove(toRemove);
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);

            //returnUrl = this.Request.UrlReferrer.AbsolutePath;
            
            //return RedirectToAction(returnUrl);

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

        //
         private IEnumerable<Tag> ReconsileTags(IEnumerable<TagCheckbox> tags)
         {
            foreach(var tag in tags.Where(t => t.IsChecked))
            {
                if (tag.Id != null)
                {
                    yield return Database.Session.Load<Tag>(tag.Id);
                    continue;
                }

                var existingTag = Database.Session.Query<Tag>().FirstOrDefault(t => t.Name == tag.Name);
                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag 
                { 
                    Name = tag.Name, 
                    Slug = tag.Name.Slugify()
                };
                Database.Session.Save(newTag);
                yield return newTag;
            }
         }


    }
}