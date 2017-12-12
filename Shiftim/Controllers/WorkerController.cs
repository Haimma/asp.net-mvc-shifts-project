using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shiftim.Models;
using Shiftim.Dal;
using System.Data.Entity;

namespace Shiftim.Controllers
{
    [Authorize]
    public class WorkerController : Controller
    {
        ShiftsDal sDal = new ShiftsDal();
        MSGDal mDal = new MSGDal();
        // GET: Shifts
        public ActionResult Index()
        {
                return RedirectToAction("Index", "Home");
        }
        public ActionResult Add_Shifts()
        {
            DateTime sunday = getSunday();
            ViewData["SundayDate"] = sunday.ToShortDateString();
            ViewData["MondayDate"] = sunday.AddDays(1).ToShortDateString();
            ViewData["TuesdayDate"] = sunday.AddDays(2).ToShortDateString(); ;
            ViewData["WednesdayDate"] = sunday.AddDays(3).ToShortDateString(); ;
            ViewData["ThursdayDate"] = sunday.AddDays(4).ToShortDateString(); ;
            ViewData["FridayDate"] = sunday.AddDays(5).ToShortDateString(); ;
            ViewData["SaturdayDate"] = sunday.AddDays(6).ToShortDateString();
            TempData["msg"] = null;
            ViewBag.e = false;
            string email_;
            try {
                email_ = Session["user"].ToString();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            var date_exists = (from x1 in sDal.Shifts
                               where(x1.email.Equals(email_)) select x1.date);
            foreach(var d in date_exists)
            {
                if (d.ToShortDateString().Equals(ViewData["SundayDate"]))
                {
                    TempData["msg"] = "!הזנת כבר משמרות לשבוע זה";
                    ViewBag.e = true;
                    return View();
                }
            }
                return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Add_Shifts(List<Shifts> s)
        {
            ViewBag.e = false;
            string Msg = null;
            try
            {
                List<Shifts> sh = addToList(s);
                for (int i = 0; i < 7; i++)
                    sDal.Shifts.Add(sh[i]);
                sDal.SaveChangesAsync();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                Msg = "ישנה בעיה בחיבור לשרת הנתונים! נסה שוב מאוחר יותר";
            }
            TempData["msg"] = Msg;
            return View("WorkerPanel");
        }

        public List<Shifts> addToList(List <Shifts> s)
        {
            DateTime sunday = getSunday();
            s[0].day = "א";
            s[1].day = "ב";
            s[2].day = "ג";
            s[3].day = "ד";
            s[4].day = "ה";
            s[5].day = "ו";
            s[6].day = "ש";
            s[0].date = sunday;
            s[1].date = sunday.AddDays(1);
            s[2].date = sunday.AddDays(2);
            s[3].date = sunday.AddDays(3);
            s[4].date = sunday.AddDays(4);
            s[5].date = sunday.AddDays(5);
            s[6].date = sunday.AddDays(6);

            for (int i=0; i < 7; i++)
                s[i].email = Session["user"].ToString();
            return s;
        }
        public DateTime getSunday()
        {
            DateTime now = DateTime.Now;
            string nowDate = now.DayOfWeek.ToString();
            switch (nowDate)
            {
                case "Sunday":
                    {
                        now = now.AddDays(7);
                        break;
                    }
                case "Monday":
                    {
                        now = now.AddDays(6);
                        break;
                    }
                case "Tuesday":
                    {
                        now = now.AddDays(5);
                        break;
                    }
                case "Wednesday":
                    {
                        now = now.AddDays(4);
                        break;
                    }
                case "Thursday":
                    {
                        now = now.AddDays(3);
                        break;
                    }
                case "Friday":
                    {
                        now = now.AddDays(2);
                        break;
                    }
                case "Saturday":
                    {
                        now = now.AddDays(1);
                        break;
                    }
            }
            return now;
    }

        public ActionResult WorkerPanel()
        {
            try
            {
                if (Session["rank"].ToString() == "עובדים")
                {
                    return View();
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult Add_Note()
        { 
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add_Note(MSG m)
        {
           
            string Msg = null;
            try
            {
                if (m.msg!=null)
                {
                    if (Session["rank"].ToString() == "עובדים")
                    {
                        m.date = DateTime.Now;
                        m.email = Session["user"].ToString();
                        mDal.Messages.Add(m);
                        mDal.SaveChanges();
                        Msg = "הבקשה שלך התווספה בהצלחה";
                    }
                    else
                        throw new Exception("אין לך הרשאה לפעולה זאת");
                }
                else
                    Msg = "הבקשה ריקה! נא למלא את הטופס";          
            }
            catch (System.Data.SqlClient.SqlException)
            {
                Msg = "ישנה בעיה בחיבור לשרת הנתונים! נסה שוב מאוחר יותר";
            }
            catch (Exception)
            {
                Msg = "הרשאה לא מתאימה";
            }
            ViewBag.msg = Msg;
            return View(m);
        }

        public ActionResult DayShift(string show = null)
        {
            ViewData["date"] = show;
            DateTime d = Convert.ToDateTime(show);
            var date_exists = (from x1 in sDal.Shifts
                               where (x1.date == d && x1.approve.Equals("T"))
                               select x1);
            ViewBag.work = date_exists;
            return PartialView("PartShift", date_exists);
        }

        public ActionResult Show_Shifts()
        {
            return View();
        }
    }
}