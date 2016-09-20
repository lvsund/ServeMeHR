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
    public class RequestTypesController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: RequestTypes
        public async Task<ActionResult> Index(string SelectedTeam)

        {
            IEnumerable<SelectListItem> teamitems = db.Teams.Select(c => new SelectListItem
            {
                //Selected = c.Id == 1,
                Value = c.TeamDescription,
                Text = c.TeamDescription
            });
            ViewBag.SelectedTeam = teamitems;
            var requestTypes = db.RequestTypes.Include(r => r.Team1);

            requestTypes = from rt in db.RequestTypes
                           join t in db.Teams on rt.Team equals t.Id
                           where t.TeamDescription == SelectedTeam

                           select rt;
            return View(await requestTypes.ToListAsync());
        }

        // GET: RequestTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        // GET: RequestTypes/Create
        public ActionResult Create()
        {
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription");
            var model = new RequestType();
            model.LastUpdated = System.DateTime.Now;
            model.Active = true;
            //ViewBag.LastUpdated = System.DateTime.Now;
            return View(model);
        }

        // POST: RequestTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,RequestTypeDescription,LastUpdated,Active,Team")] RequestType requestType)
        {
            if (ModelState.IsValid)
            {
                db.RequestTypes.Include(i => i.RequestTypeSteps);

                requestType.RequestTypeSteps.Add(new RequestTypeStep()
                {
                    StepDescription = "--Start--",
                    StepNumber = 1,
                    LastUpdated = System.DateTime.Now,
                    Active = true,
                    RequestType = requestType.Id
                });

                db.RequestTypes.Add(requestType);
                requestType.LastUpdated = System.DateTime.Now;
                requestType.Active = true;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", requestType.Team);
            return View(requestType);
        }

        // GET: RequestTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", requestType.Team);

            return View(requestType);
        }

        // POST: RequestTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,RequestTypeDescription,LastUpdated,Active,Team")] RequestType requestType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestType).State = EntityState.Modified;
                requestType.LastUpdated = System.DateTime.Now;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", requestType.Team);
            return View(requestType);
        }

        // GET: RequestTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        // POST: RequestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            db.RequestTypes.Remove(requestType);
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