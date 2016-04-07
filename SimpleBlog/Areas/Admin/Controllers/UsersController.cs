using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using SimpleBlog.Areas.Admin.ViewModels;
using NHibernate.Linq;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View(new UsersIndex 
            {
                Users = Database.Session.Query<User>().ToList()
            });
        }

    }
}
