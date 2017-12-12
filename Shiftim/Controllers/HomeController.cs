using Shiftim.Dal;
using Shiftim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shiftim.Controllers
{
    public class HomeController : Controller
    {
       
        // GET: Home
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                if (Session["rank"].Equals("מנהלה"))
                    return RedirectToAction("ManagerPanel","Manager");
                else if (Session["rank"].Equals("עובדים"))
                    return RedirectToAction("WorkerPanel","Worker");
                else
                    return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Login()
     {
            if (Session["user"] != null)
            {
                if (Session["rank"].Equals("מנהלה"))
                    return RedirectToAction("ManagerPanel", "Manager");
                else if (Session["rank"].Equals("עובדים"))
                    return RedirectToAction("WorkerPanel","Worker");
                else
                    return View();
            }
            else
                return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {

            UserDal uDal = new UserDal();
            var userlogin = uDal.Users.Where(usr => usr.email.Equals(u.email) && usr.password.Equals(u.password)).SingleOrDefault();
            if (userlogin != null)
            {
                
                FormsAuthentication.SetAuthCookie("cookie", true);
                Session["user"] = userlogin.email.ToString();
                Session["rank"] = userlogin.rank.ToString();
                Worker w = new WorkerDal().Workers.Find(u.email);
                Session["Name"] = w.firstName + " " + w.lastName;
                return RedirectToAction("Index");
            }
            else
            {
                string Msg = "אחד מהנתונים אינו נכון*";
                ViewBag.msg = Msg;
                return View();
            }
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["user"] = null; //it's my session variable
            Session["rank"] = null; //it's my session variable
            Session.Clear();
            Session.Abandon();
            //FormsAuthentication.SignOut(); //you write this when you use FormsAuthentication
            return RedirectToAction("Login", "Home");
        }
    }
}