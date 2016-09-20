using ServeMeHR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServeMeHR.Controllers
{
    public class RequestTypeStepsController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: RequestTypeSteps
        public async Task<ActionResult> Index(string SelectedRequestType)
        {
            IEnumerable<SelectListItem> requestTypeitems = db.RequestTypes.Select(c => new SelectListItem
            {
                //Selected = c.Id == 1,
                Value = c.RequestTypeDescription,
                Text = c.RequestTypeDescription
            });
            ViewBag.SelectedRequestType = requestTypeitems;

            var requestTypeSteps = db.RequestTypeSteps.Include(r => r.RequestType1);
            requestTypeSteps = from rts in db.RequestTypeSteps
                               join rt in db.RequestTypes on rts.RequestType equals rt.Id
                               where rt.RequestTypeDescription == SelectedRequestType

                               select rts;
            return View(await requestTypeSteps.ToListAsync());
        }

        // GET: RequestTypeSteps/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTypeStep requestTypeStep = await db.RequestTypeSteps.FindAsync(id);
            if (requestTypeStep == null)
            {
                return HttpNotFound();
            }
            return View(requestTypeStep);
        }

        // GET: RequestTypeSteps/Create
        public ActionResult Create()
        {
            ViewBag.RequestType = new SelectList(db.RequestTypes, "Id", "RequestTypeDescription");
            var model = new RequestTypeStep();
            model.LastUpdated = System.DateTime.Now;
            model.Active = true;

            return View(model);
        }

        // POST: RequestTypeSteps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StepDescription,StepNumber,LastUpdated,Active,RequestType")] RequestTypeStep requestTypeStep)
        {
            if (ModelState.IsValid)
            {
                db.RequestTypeSteps.Add(requestTypeStep);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RequestType = new SelectList(db.RequestTypes, "Id", "RequestTypeDescription", requestTypeStep.RequestType);
            return View(requestTypeStep);
        }

        // GET: RequestTypeSteps/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTypeStep requestTypeStep = await db.RequestTypeSteps.FindAsync(id);
            if (requestTypeStep == null)
            {
                return HttpNotFound();
            }
            ViewBag.RequestType = new SelectList(db.RequestTypes, "Id", "RequestTypeDescription", requestTypeStep.RequestType);
            return View(requestTypeStep);
        }

        // POST: RequestTypeSteps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StepDescription,StepNumber,LastUpdated,Active,RequestType")] RequestTypeStep requestTypeStep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestTypeStep).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RequestType = new SelectList(db.RequestTypes, "Id", "RequestTypeDescription", requestTypeStep.RequestType);
            return View(requestTypeStep);
        }

        // GET: RequestTypeSteps/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTypeStep requestTypeStep = await db.RequestTypeSteps.FindAsync(id);
            if (requestTypeStep == null)
            {
                return HttpNotFound();
            }
            return View(requestTypeStep);
        }

        // POST: RequestTypeSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RequestTypeStep requestTypeStep = await db.RequestTypeSteps.FindAsync(id);
            db.RequestTypeSteps.Remove(requestTypeStep);
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