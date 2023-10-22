using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediSight_Project.Models;
using Microsoft.AspNet.Identity;

namespace MediSight_Project.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Index()
        {

            var reviews = db.Reviews.ToList();
            var userNames = new Dictionary<string, string>();
            var totalRating = 0;
            foreach (var review in reviews)
            {
                var user = db.Users.Find(review.UserId);
                if (user != null)
                {
                    userNames[review.UserId] = user.UserName.Split('@')[0];
                }
                else
                {
                    userNames[review.UserId] = "User Not Found";
                }
                totalRating += review.ReviewRating;
            }
            var viewModel = new ReviewUserView
            {
                Reviews = reviews,
                UserNames = userNames,
                AverageRating = totalRating / reviews.Count
            };

            return View(viewModel);
        }


        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ReviewId,UserId,ReviewText,Time,ReviewRating")] Review review)
        {
            string currentUserId = User.Identity.GetUserId();
            review.UserId = currentUserId;
            review.Time = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult Edit([Bind(Include = "ReviewId,UserId,ReviewText,ReviewRating")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
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
