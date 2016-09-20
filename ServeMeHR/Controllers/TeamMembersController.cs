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
    public class TeamMembersController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: TeamMembers
        public async Task<ActionResult> Index(string SelectedTeam)
        {
            IEnumerable<SelectListItem> teamitems = db.Teams.Select(c => new SelectListItem
            {
                //Selected = c.Id == 1,
                Value = c.TeamDescription,
                Text = c.TeamDescription
            });
            ViewBag.SelectedTeam = teamitems;

            var teamMembers = db.TeamMembers.Include(t => t.Member1).Include(t => t.Team1);

            teamMembers = from tm in db.TeamMembers
                          join t in db.Teams on tm.Team equals t.Id
                          where t.TeamDescription == SelectedTeam

                          select tm;

            return View(await teamMembers.ToListAsync());
        }

        // GET: TeamMembers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = await db.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // GET: TeamMembers/Create
        public ActionResult Create()
        {
            ViewBag.Member = new SelectList(db.Members, "Id", "MemberUserid");
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription");
            return View();
        }

        // POST: TeamMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Member,Team")] TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                db.TeamMembers.Add(teamMember);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
                //return RedirectToAction("Details", "Team", new { id = teamMember.Team.Id });
            }

            ViewBag.Member = new SelectList(db.Members, "Id", "MemberUserid", teamMember.Member);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", teamMember.Team);
            return View(teamMember);
        }

        // GET: TeamMembers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = await db.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.Member = new SelectList(db.Members, "Id", "MemberUserid", teamMember.Member);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", teamMember.Team);
            return View(teamMember);
        }

        // POST: TeamMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Member,Team")] TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamMember).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Member = new SelectList(db.Members, "Id", "MemberUserid", teamMember.Member);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", teamMember.Team);
            return View(teamMember);
        }

        // GET: TeamMembers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = await db.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // POST: TeamMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TeamMember teamMember = await db.TeamMembers.FindAsync(id);
            db.TeamMembers.Remove(teamMember);
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