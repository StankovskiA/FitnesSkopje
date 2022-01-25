using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using FitnesSkopjeWebApp.Models;

namespace FitnesSkopjeWebApp.Controllers
{
    [Authorize(Roles ="Admin,User")]
    public class FavouritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //visok 
        // GET: Favourites
        public ActionResult Index()
        {
            return View(db.Favourites.ToList());
        }

        [Authorize]
        public ActionResult apiFavourites()
        {
            return View();
        }

        //visok ama moze da stoi poso postoi kao funkcionalnost vo apito
        [Authorize]
        public ActionResult UserFavourites()
        {
            string curentUserEmail = User.Identity.Name;
            var id = db.AppUsers.Where(t => t.email == curentUserEmail).FirstOrDefault().id;
            ViewBag.User = db.AppUsers.Where(t => t.email == curentUserEmail).FirstOrDefault().firstName + " " + db.AppUsers.Where(t => t.email == curentUserEmail).FirstOrDefault().lastName;           
            return View((db.Gyms.ToList(),db.Favourites.Where(u => u.userId==id).ToList()));
        }

        //visok
        // GET: Favourites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favourite favourite = db.Favourites.Find(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            return View(favourite);
        }
        
        //visok samo post ni treba
        // GET: Favourites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Favourites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userId,gymId")] Favourite favourite,string gym_id)
        {
            //if (ModelState.IsValid)
            //{
            var userEmail = User.Identity.Name;
            var gymIdd = int.Parse(Request.Form["gymId"]);
            var gymNamee = Request.Form["gymName"];
            var userIdd = db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault().id;

            using (var client = new HttpClient())
                {
                    //this string needs to be adapted to the final uri after deployment
                    client.BaseAddress = new Uri("https://localhost:44359/api/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Favourite>("Favourites", new Favourite {userId = userIdd,  gymId = gymIdd, gymName = gymNamee});
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Gyms");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(favourite);

            //STARIOT CODE
            //if (db.Favourites.Where(t => t.userId == userId && t.gymId ==gymId).FirstOrDefault() == null)
            //{
            //    db.Favourites.Add(new Favourite()
            //    {
            //        userId = db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault().id,
            //        gymId = gymId,
            //        gymName=gymName
            //    });
            //    db.SaveChanges();
            //    return RedirectToAction("Index", "Gyms");
            //}
            //else
            //{
            //    Favourite favouriteGym = db.Favourites.Where(t => t.userId == userId && t.gymId == gymId).First();
            //    db.Favourites.Remove(favouriteGym);
            //    db.SaveChanges();
            //    return RedirectToAction("Index", "Gyms");
            //}
            // }


        }
        
        
        //visok
        // GET: Favourites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favourite favourite = db.Favourites.Find(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            return View(favourite);
        }

        //visok
        // POST: Favourites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userId,gymId")] Favourite favourite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favourite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(favourite);
        }

        // GET: Favourites/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favourite favourite = db.Favourites.Find(id);
            if (favourite == null)
            {
                return HttpNotFound();
            }
            return View(favourite);
        }

        // POST: Favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                //this string needs to be adapted to the final uri after deployment
                client.BaseAddress = new Uri("https://localhost:44359/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Favourites/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("apiFavourites");
                }
            }
            return RedirectToAction("apiFavourites");

            //return RedirectToAction("Index");
            //Favourite favourite = db.Favourites.Find(id);
            //db.Favourites.Remove(favourite);
            //db.SaveChanges();

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
