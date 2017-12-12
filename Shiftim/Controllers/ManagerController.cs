using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shiftim.Models;
using Shiftim.Dal;
using System.Data.SqlClient;
using System.Data.Entity;
using Newtonsoft.Json;
namespace Shiftim.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        WorkerDal wDal = new WorkerDal();
        ShiftsDal sDal = new ShiftsDal();
        MSGDal mDal = new MSGDal();
        //List
        public ActionResult ListOfWorker()
        {
            if (Session["rank"].ToString() == "מנהלה")
            {
                TempData["corItem"] = new Worker();
                return View(wDal.Workers.OrderBy(x => x.firstName).ToList());
            }
            else
                return RedirectToAction("Index", "Home");

        }
        //Details
        public ActionResult Details(string Email=null)
        {
            ViewBag.corItem = wDal.Workers.Find(Email);
            return PartialView("details_part");
        }

        //Create
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create(Worker w)
        {
            string Msg = null;
            try
            {
            w.user1.email = w.email;
                if (w.role == "אחראי משמרת")
                    w.user1.rank = "מנהלה";
                else
                    w.user1.rank = "עובדים";
            using (wDal)
            {
                    wDal.Workers.Add(w);
                    wDal.SaveChanges();
                    Msg = "העובד נוסף בהצלחה";
                    ViewBag.newworker = "true";
            }
            return RedirectToAction("ListOfWorker");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                Msg = "האימייל או התעודת זהות כבר קיימים במערכת!";
            }
            catch (System.Data.SqlClient.SqlException)
            {
                Msg = "ישנה בעיה בחיבור לשרת הנתונים! נסה שוב מאוחר יותר";
            }
            catch (Exception ex)
            {
                Msg = "ERROR\n" + ex.GetType();
            }
            ViewBag.msg = Msg;
            return View("Create", w);
        }

        //Update
        public ActionResult Edit(string email = null)
        {
            ViewBag.corItem = wDal.Workers.Find(email);
          return View(wDal.Workers.Find(email));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Worker w)
        {        
            string Msg = null;
            try
            {
                w.user1.email = w.email;
                w.user1.password = w.user1.password;
                if (w.role == "אחראי משמרת")
                    w.user1.rank = "מנהלה";
                else
                    w.user1.rank = "עובדים";
                wDal.Entry(w).State = EntityState.Modified;
                wDal.Entry(w.user1).State = EntityState.Modified;
                wDal.SaveChanges();
                return RedirectToAction("ListOfWorker");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                Msg = "ישנה בעיה בחיבור לשרת הנתונים! נסה שוב מאוחר יותר";
            }
            catch (Exception ex)
            {
                Msg = "ERROR\n" + ex.GetType();
            }
            ViewBag.msg = Msg;
            return View("Edit", w);
        }

        //Delete
        public ActionResult DeleteWorker(string Email = null)
        {
           ViewBag.corItem = wDal.Workers.Find(Email);
            TempData["corItemEmail"] = Email;
            return PartialView("details_part");
        }

        public ActionResult Delete_conf()
        {
            Worker w = wDal.Workers.Find(TempData["corItemEmail"]);
            wDal.Workers.Remove(w);

            User u = wDal.Users.Find(TempData["corItemEmail"]);
            wDal.Users.Remove(u);
            wDal.SaveChanges();
            return RedirectToAction("ListOfWorker");
        }
   
        public ActionResult ManagerPanel(Worker w)
        {
            try {
                if (Session["rank"].ToString() == "מנהלה")
                {
                    ViewBag.newworker = "false";
                    ViewBag.msg = null;
                    return View("ManagerPanel", new Worker());
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult ShowApproveShifts(string d=null)
        {
            TempData["corDay"] = "א";        
            TempData["corItem"] = new Shifts();
            var sh = (from x in sDal.Shifts
                      where (x.day.Equals("א") && x.approve.Equals(null))
                      select x);
            switch (d)
            {
                case "א":
                    {
                         sh = (from x in sDal.Shifts
                                   where (x.day.Equals("א") && x.approve.Equals(null))
                                   select x);
                        break;
                    }
                case "ב":
                    {
                         sh = (from x in sDal.Shifts
                                   where (x.day.Equals("ב") && x.approve.Equals(null))
                                   select x).OrderBy(y=>y.date);
                        break;
                    }
                case "ג":
                    {
                         sh = (from x in sDal.Shifts
                                   where (x.day.Equals("ג") && x.approve.Equals(null))
                                   select x);
                        break;
                    }
                case "ד":
                    {
                         sh = (from x in sDal.Shifts
                                   where (x.day.Equals("ד") && x.approve.Equals(null))
                                   select x);
                        break;
                    }
                case "ה":
                    {
                         sh = (from x in sDal.Shifts
                                   where (x.day.Equals("ה") && x.approve.Equals(null))
                                   select x);
                        break;
                    }
                case "ו":
                    {
                         sh = (from x in sDal.Shifts
                                   where (x.day.Equals("ו") && x.approve.Equals(null))
                                   select x);
                        break;
                    }
                case "ש":
                    {
                         sh = (from x in sDal.Shifts
                                   where (x.day.Equals("ש") && x.approve.Equals(null))
                                   select x);
                        break;
                    }
                default: return View(sh); 
            }
            return View(sh);
        }

        //approve shift
        [HttpPost]
        public ActionResult ApproveShift(string Email = null, string d = null)
        {
            var tmp = (from x in sDal.Shifts
                       where (x.email.Equals(Email) && x.day == d)
                       select x).SingleOrDefault();
            Shifts newt = new Shifts();
            newt = tmp;
            newt.approve = "T";
            sDal.Shifts.Remove(tmp);
            sDal.SaveChanges();
            sDal.Shifts.Add(newt);
            sDal.SaveChanges();
            return RedirectToAction("ApproveShifts");
        }
        [HttpPost]
        public ActionResult RejectShift(string Email = null, string d = null)
        {
            var tmp = (from x in sDal.Shifts
                       where (x.email.Equals(Email) && x.day == d)
                       select x).SingleOrDefault();
            Shifts newt = new Shifts();
            newt = tmp;
            newt.approve = "F";
            sDal.Shifts.Remove(tmp);
            sDal.SaveChanges();
            sDal.Shifts.Add(newt);
            sDal.SaveChanges();
            return RedirectToAction("ApproveShifts");
        }
        public ActionResult Show_Notes()
        {
            TempData["corItem"] = new MSG();
            return View();
        }
        public JsonResult getShowNotes()
        {
            return Json(mDal.Messages.OrderBy(x => x.date).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}