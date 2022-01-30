using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using FitnesSkopjeWebApp.Models;

namespace FitnesSkopjeWebApp.Controllers.api
{
    public class FavouritesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/Favourites
        public IQueryable<Favourite> GetFavourites()
        {
            string curentUserEmail = User.Identity.Name;
            var id = db.AppUsers.Where(t => t.email == curentUserEmail).FirstOrDefault().id;
           
            return db.Favourites.Where(u => u.userId == id);
        }


        // POST: api/Favourites
        [ResponseType(typeof(Favourite))]
        public IHttpActionResult PostFavourite(Favourite favourite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Favourites.Add(favourite);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = favourite.id }, favourite);
        }

        // DELETE: api/Favourites/5
        [ResponseType(typeof(Favourite))]
        public IHttpActionResult DeleteFavourite(int id)
        {
            Favourite favourite = db.Favourites.Find(id);
            if (favourite == null)
            {
                return NotFound();
            }

            db.Favourites.Remove(favourite);
            db.SaveChanges();

            return Ok(favourite);
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