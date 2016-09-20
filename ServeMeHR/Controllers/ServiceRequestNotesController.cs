using ServeMeHR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServeMeHR.Controllers
{
    public class ServiceRequestNotesController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: ServiceRequestNotes
        public async Task<ActionResult> Index()
        {
            var serviceRequestNotes = db.ServiceRequestNotes.Include(s => s.ServiceRequest1);
            return View(await serviceRequestNotes.ToListAsync());
        }

        // GET: ServiceRequestNotes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequestNote serviceRequestNote = await db.ServiceRequestNotes.FindAsync(id);
            if (serviceRequestNote == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequestNote);
        }

        // GET: ServiceRequestNotes/Create
        public ActionResult Create(int ServiceRequest)
        {
            //ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading");

            ViewBag.ServiceRequest = ServiceRequest;
            ViewBag.LastUpdated = DateTime.Now;
            ViewBag.WrittenBy = User.Identity.Name;
            return View();
        }

        // POST: ServiceRequestNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NoteDescription,LastUpdated,WrittenBy,ServiceRequest")] ServiceRequestNote serviceRequestNote, string returncontroller)
        {
            if (ModelState.IsValid)
            {
                db.ServiceRequestNotes.Add(serviceRequestNote);
                serviceRequestNote.LastUpdated = DateTime.Now;
                serviceRequestNote.WrittenBy = User.Identity.Name;
                await db.SaveChangesAsync();
                //return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
                return RedirectToAction("Index", returncontroller);
            }

            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", serviceRequestNote.ServiceRequest);
            return View(serviceRequestNote);
        }

        // GET: ServiceRequestNotes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequestNote serviceRequestNote = await db.ServiceRequestNotes.FindAsync(id);
            if (serviceRequestNote == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", serviceRequestNote.ServiceRequest);
            return View(serviceRequestNote);
        }

        // POST: ServiceRequestNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NoteDescription,LastUpdated,WrittenBy,ServiceRequest")] ServiceRequestNote serviceRequestNote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceRequestNote).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", serviceRequestNote.ServiceRequest);
            return View(serviceRequestNote);
        }

        // GET: ServiceRequestNotes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequestNote serviceRequestNote = await db.ServiceRequestNotes.FindAsync(id);
            if (serviceRequestNote == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequestNote);
        }

        // POST: ServiceRequestNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServiceRequestNote serviceRequestNote = await db.ServiceRequestNotes.FindAsync(id);
            db.ServiceRequestNotes.Remove(serviceRequestNote);
            await db.SaveChangesAsync();
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