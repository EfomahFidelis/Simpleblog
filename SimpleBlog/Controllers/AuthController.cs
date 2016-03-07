using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.ViewModels;
using System.Web.Security;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        public ActionResult Login()
        {
            return View(new AuthLogin 
            { 
 
            });
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View();

            // This tells ASPNET that a person says who he/she is
            FormsAuthentication.SetAuthCookie(form.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("home");
            
        }
    }
}
