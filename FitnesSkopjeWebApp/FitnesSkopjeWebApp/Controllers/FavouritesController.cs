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
    [Authorize(Roles = "Admin,User")]
    public class FavouritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult apiFavourites()
        {
            return View();
        }

        // GET: Favourites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Favourites/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userId,gymId")] Favourite favourite, string gym_id)
        {

            var userEmail = User.Identity.Name;
            var gymIdd = int.Parse(Request.Form["gymId"]);
            var gymNamee = Request.Form["gymName"];
            var userIdd = db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault().id;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44359/api/");

                var postTask = client.PostAsJsonAsync<Favourite>("Favourites", new Favourite { userId = userIdd, gymId = gymIdd, gymName = gymNamee });
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Gyms");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

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
                client.BaseAddress = new Uri("https://localhost:44359/api/");

                var deleteTask = client.DeleteAsync("Favourites/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("apiFavourites");
                }
            }
            return RedirectToAction("apiFavourites");


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
