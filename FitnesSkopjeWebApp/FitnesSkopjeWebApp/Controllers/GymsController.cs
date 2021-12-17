using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnesSkopjeWebApp.Models;

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

            return View((db.Gyms.ToList(), db.Favourites.ToList()));
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
            return View((gym, GetReviews(id), IsGymOpened(id)));
        }

        // GET: Gyms/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gyms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        //Get reviews
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

        //Check Working Hours
        public String IsGymOpened(int? id)
        {
            if (id == null)
            {
                throw new Exception("Null gym id");
            }

            var gymId = (int)id;
            var today = DateTime.Now.DayOfWeek;

            if (today == DayOfWeek.Sunday)
            {
                var working_time = db.Gyms.Where(gym => gym.Id.Equals(gymId)).First().workingTimeSunday;
                return checkTime(working_time);
            }
            else
            {
                var working_time = db.Gyms.Where(gym => gym.Id.Equals(gymId)).First().workingTimeWeek;
                return checkTime(working_time);
            }

        }
        public String checkTime(string working_time)
        {
            //08:00-22:00 
            var parts = working_time.Split('-');

            //08:00
            int openingHour = Int32.Parse(parts[0].Split(':')[0]);
            //22:00
            int closingHour = Int32.Parse(parts[1].Split(':')[0]);

            //vrakja vo 24h format
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
