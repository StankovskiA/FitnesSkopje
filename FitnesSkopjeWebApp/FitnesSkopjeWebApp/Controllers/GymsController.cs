using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnesSkopjeWebApp.Models;
using FitnesSkopjeWebApp.ViewModels;
using PagedList;

namespace FitnesSkopjeWebApp.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class GymsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Gyms
        public ActionResult Index()
        {
            var userEmail = User.Identity.Name;
            if (db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault() != null)
            {
                ViewBag.userId = db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault().id;
            };

            ViewBag.gymTypes = db.Gym_Types.ToList();
            SearchApplicationModels search = new SearchApplicationModels();
            var gyms = GetApplicationsSearch(search).ToList();
            return View((gyms, db.Favourites.ToList(), search));
        }

        [HttpPost]
        public ActionResult Index(SearchApplicationModels search)
        {
            var gymTypeId = Request.Form["checkboxListItem"];
            var lstApps = new List<Gym>();
           
            if (gymTypeId != null)
            {

                var gymTypeName = db.Gym_Types.Find(int.Parse(gymTypeId)).Type;

                lstApps = GetApplicationsSearch(search).Where(a => a.Areas.Contains(gymTypeName)).ToList();

                var userEmail = User.Identity.Name;
                if (db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault() != null)
                {
                    ViewBag.userId = db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault().id;
                };
                ViewBag.gymTypes = db.Gym_Types.ToList();
                ViewBag.SelectedCheckboxItem = int.Parse(gymTypeId);
            }
            else
            {
                lstApps = GetApplicationsSearch(search).ToList();

                var userEmail = User.Identity.Name;
                if (db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault() != null)
                {
                    ViewBag.userId = db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault().id;
                };
                ViewBag.gymTypes = db.Gym_Types.ToList();

            }
            return View((lstApps, db.Favourites.ToList(), search));
        }

        public JsonResult GetGyms(string term)
        {
            List<String> gyms;

            gyms = db.Gyms.Where(x => x.Name.Contains(term)).Select(y => y.Name).ToList();

            return Json(gyms, JsonRequestBehavior.AllowGet);
        }

        // GET: Gyms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gym gym = db.Gyms.Find(id);
            if (gym == null)
            {
                return HttpNotFound();
            }

            ViewBag.GymName = db.Gyms.Find(id).Name;

            var userEmail = User.Identity.Name;
            var userId = db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault().id;
            var isReviewed = db.Reviews.Where(a => a.gymId == id && a.userId == userId).FirstOrDefault();
            ViewBag.IsReviewed = isReviewed;
            if (isReviewed != null)
            {
                ViewBag.gymReview = isReviewed.id;
            }
            return View((gym, GetReviews(id), IsGymOpened(id)));
        }

        // GET: Gyms/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gyms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Number,WorkingTime,Areas")] Gym gym)
        {
            if (ModelState.IsValid)
            {
                db.Gyms.Add(gym);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gym);
        }

        // GET: Gyms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gym gym = db.Gyms.Find(id);
            if (gym == null)
            {
                return HttpNotFound();
            }
            return View(gym);
        }

        // POST: Gyms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Number,WorkingTime,Areas")] Gym gym)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gym).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gym);
        }

        // GET: Gyms/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gym gym = db.Gyms.Find(id);
            if (gym == null)
            {
                return HttpNotFound();
            }
            return View(gym);
        }

        // POST: Gyms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gym gym = db.Gyms.Find(id);
            db.Gyms.Remove(gym);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IQueryable<Gym> GetGyms()
        {
            return db.Gyms;
        }

        public IQueryable<Gym> GetApplicationsSearch(SearchApplicationModels search)
        {
            var projectIQ = GetGyms();

            if (search.GymId > 0)
                projectIQ = projectIQ.Where(t => t.Id == search.GymId);

            if (search.SearchText != null && search.SearchText?.Trim() != "")
                projectIQ = projectIQ.Where(t => t.Name.Contains(search.SearchText) || t.Areas.Contains(search.SearchText) || t.Address.Contains(search.SearchText));

            return projectIQ;
        }
        public List<Review> GetReviews(int? id)
        {
            if (id != null)
            {
                int gymId = (int)id;
                return db.Reviews
                .Where(r => r.gymId.Equals(gymId))
                .ToList();
            }

            return null;
        }

        public String IsGymOpened(int? id)
        {
            if (id == null)
            {
                throw new Exception("Null gym id");
            }

            var gymId = (int)id;
            var today = DateTime.Now.DayOfWeek;
            var working_time = " ";

            if (today == DayOfWeek.Sunday)
            {
                working_time = db.Gyms.Where(gym => gym.Id.Equals(gymId)).First().workingTimeSunday;

            }
            else
            {
                working_time = db.Gyms.Where(gym => gym.Id.Equals(gymId)).First().workingTimeWeek;

            }
            return CheckTime(working_time);

        }


        public String CheckTime(string working_time)
        {
            if (working_time.Equals(""))
            {
                return "Теретаната не е отворена!";
            }
          
            var parts = working_time.Split('-');

          
            int openingHour = Int32.Parse(parts[0].Split(':')[0]);

            int closingHour = Int32.Parse(parts[1].Split(':')[0]);

            var currentHour = DateTime.Now.Hour;

            if (currentHour >= openingHour &&
                currentHour <= closingHour)
            {
                return "Теретаната е отворена!";
            }
            else
                return "Теретаната не е отворена!";

        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
