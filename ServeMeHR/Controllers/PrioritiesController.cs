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
    public class PrioritiesController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: Priorities
        public async Task<ActionResult> Index(string SelectedTeam)
        {
            IEnumerable<SelectListItem> teamitems = db.Teams.Select(c => new SelectListItem
            {
                //Selected = c.Id == 1,
                Value = c.TeamDescription,
                Text = c.TeamDescription
            });
            ViewBag.SelectedTeam = teamitems;

            var priorities = db.Priorities.Include(p => p.Team1);
            priorities = from p in db.Priorities
                         join t in db.Teams on p.Team equals t.Id
                         where t.TeamDescription == SelectedTeam

                         select p;

            return View(await priorities.ToListAsync());
        }

        // GET: Priorities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priority priority = await db.Priorities.FindAsync(id);
            if (priority == null)
            {
                return HttpNotFound();
            }
            return View(priority);
        }

        // GET: Priorities/Create
        public ActionResult Create()
        {
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription");
            var model = new Priority();
            model.LastUpdated = System.DateTime.Now;
            model.Active = true;

            return View(model);
        }

        // POST: Priorities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PriorityDescription,LastUpdated,Active,Team")] Priority priority)
        {
            if (ModelState.IsValid)
            {
                db.Priorities.Add(priority);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", priority.Team);
            return View(priority);
        }

        // GET: Priorities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priority priority = await db.Priorities.FindAsync(id);
            if (priority == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", priority.Team);
            return View(priority);
        }

        // POST: Priorities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PriorityDescription,LastUpdated,Active,Team")] Priority priority)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priority).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", priority.Team);
            return View(priority);
        }

        // GET: Priorities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priority priority = await db.Priorities.FindAsync(id);
            if (priority == null)
            {
                return HttpNotFound();
            }
            return View(priority);
        }

        // POST: Priorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Priority priority = await db.Priorities.FindAsync(id);
            db.Priorities.Remove(priority);
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