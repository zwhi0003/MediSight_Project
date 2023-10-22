using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediSight_Project.Models;
using System.Threading.Tasks;
using MediSight_Project.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MediSight_Project.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        // database context
        private ApplicationDbContext db = new ApplicationDbContext();
        // email services
        private BrevoService es = new BrevoService();
        private SendGridService sendGridService = new SendGridService();
        // timespan
        private List<TimeSpan> possibleBookingTimes = new List<TimeSpan>
                {
                    new TimeSpan(9, 0, 0),
                    new TimeSpan(10, 0, 0),
                    new TimeSpan(11, 0, 0),
                    new TimeSpan(12, 0, 0),
                    new TimeSpan(13, 0, 0),
                    new TimeSpan(14, 0, 0),
                    new TimeSpan(15, 0, 0),
                    new TimeSpan(16, 0, 0),
                    new TimeSpan(17, 0, 0)
                };

        // GET: Bookings
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult Index()
        {
            // returns all the bookings and users for the bookings
            var bookings = db.Bookings.ToList();
            var userNames = new Dictionary<string, string>();

            foreach (var booking in bookings)
            {
                var user = db.Users.Find(booking.UserId);
                if (user != null)
                {
                    userNames[booking.UserId] = user.UserName;
                }
                else
                {
                    userNames[booking.UserId] = "User Not Found";
                }
            }
            var viewModel = new BookingUserView
            {
                Bookings = bookings,
                UserNames = userNames
            };

            return View(viewModel);
        }

        // GET: Bookings/Details/5
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        [Authorize]
        public ActionResult Create(DateTime? desiredDate)
        {
            if (desiredDate.HasValue)
            {
                var availableTimes = GetAvailableTimesForDate(desiredDate.Value);
                ViewBag.AvailableTimes = new SelectList(availableTimes);
            }
            else
            {
                ViewBag.AvailableTimes = new SelectList(new List<TimeSpan>());
            }

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "BookingId,UserId,Time")] Booking booking)
        {
            // disabling the response header for security
            MvcHandler.DisableMvcResponseHeader = true;
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                booking.UserId = currentUserId;
               // allowing file upload for referrals
                if (booking.UploadedFile != null && booking.UploadedFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(booking.UploadedFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                    booking.UploadedFile.SaveAs(path);
                }
                // add the booking to the db
                db.Bookings.Add(booking);
                db.SaveChanges();

                // send the email
                string response = await es.SendEmail(
                    "MediSight", 
                    "MediSight@exammple.com", 
                    "Patient", 
                    User.Identity.GetUserName(),
                    "Booking Created",
                    $"<html><head></head><body><p>Hello,</p>Your booking has been created for {booking.Time.ToString()} .</p></body></html>"
                    );

                
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        [Authorize]
        private List<TimeSpan> GetAvailableTimesForDate(DateTime date)
        {

            var bookingsForDay = db.Bookings
                                   .Where(b => DbFunctions.TruncateTime(b.Time) == date.Date)
                                   .ToList();

            var bookedTimesForDay = bookingsForDay.Select(b => b.Time.TimeOfDay).ToList();

            return possibleBookingTimes.Except(bookedTimesForDay).ToList();
        }

        // GET: Bookings/Edit/5
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }


        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult Edit([Bind(Include = "BookingId,UserId,Time")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Practitioner")]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
