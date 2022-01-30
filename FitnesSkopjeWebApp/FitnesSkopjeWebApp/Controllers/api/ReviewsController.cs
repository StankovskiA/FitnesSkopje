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
using FitnesSkopjeWebApp.Models;

namespace FitnesSkopjeWebApp.Controllers.api
{
    public class review
    {
        public string gymId { get; set; }
        public string comment { get; set; }
    }
    public class ReviewsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Reviews
        public  IQueryable<TableJoinResult> GetReviews()
        {
            string curentUserEmail = User.Identity.Name;            
            return (IQueryable<TableJoinResult>)(from r in db.Reviews
                    join g in db.Gyms on r.gymId equals g.Id
                    where r.user.email==curentUserEmail
                    select new TableJoinResult {gym=g,review=r});
        }

        // UPDATE: api/Reviews
        [ResponseType(typeof(Review))]
        public IHttpActionResult PostReview(int? id,Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != null)
            {
                Review reviewnew = db.Reviews.Find(id);
                reviewnew.rating = review.rating;
                reviewnew.comment = review.comment;
                db.Entry(reviewnew).State = EntityState.Modified;
              
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists((int)id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToRoute("default", new { controller = "Reviews", action = "apiReviews" });

        }

        //CREATE : api/CreateReview
        [Route("api/CreateReview")]
        [HttpPost]
        public IHttpActionResult CreateReview(review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = User.Identity.Name;
            int rating =int.Parse(System.Web.HttpContext.Current.Request["hiddenRatingNumber"]);
            db.Reviews.Add(new Review()
            {
                userId = db.AppUsers.Where(t => t.email == userEmail).FirstOrDefault().id,
                gymId = int.Parse(review.gymId),
                rating = rating,
                comment = review.comment
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToRoute("default", new { controller = "Gyms", action = "Details",id=review.gymId });
        }
        

        // DELETE: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult DeleteReview(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(review);
            db.SaveChanges();

            return Ok(review);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReviewExists(int id)
        {
            return db.Reviews.Count(e => e.id == id) > 0;            
        }
    }
}