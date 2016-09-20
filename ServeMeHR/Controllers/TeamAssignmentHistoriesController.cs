using ServeMeHR.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServeMeHR.Controllers
{
    public class TeamAssignmentHistoriesController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: TeamAssignmentHistories
        public async Task<ActionResult> Index()
        {
            var teamAssignmentHistories = db.TeamAssignmentHistories.Include(t => t.ServiceRequest1).Include(t => t.Team1);
            return View(await teamAssignmentHistories.ToListAsync());
        }

        // GET: TeamAssignmentHistories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAssignmentHistory teamAssignmentHistory = await db.TeamAssignmentHistories.FindAsync(id);
            if (teamAssignmentHistory == null)
            {
                return HttpNotFound();
            }
            return View(teamAssignmentHistory);
        }

        // GET: TeamAssignmentHistories/Create
        public ActionResult Create()
        {
            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading");
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription");
            return View();
        }

        // POST: TeamAssignmentHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AssignedBy,DateAssigned,ServiceRequest,Team")] TeamAssignmentHistory teamAssignmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.TeamAssignmentHistories.Add(teamAssignmentHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", teamAssignmentHistory.ServiceRequest);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", teamAssignmentHistory.Team);
            return View(teamAssignmentHistory);
        }

        // GET: TeamAssignmentHistories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAssignmentHistory teamAssignmentHistory = await db.TeamAssignmentHistories.FindAsync(id);
            if (teamAssignmentHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", teamAssignmentHistory.ServiceRequest);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", teamAssignmentHistory.Team);
            return View(teamAssignmentHistory);
        }

        // POST: TeamAssignmentHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AssignedBy,DateAssigned,ServiceRequest,Team")] TeamAssignmentHistory teamAssignmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamAssignmentHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", teamAssignmentHistory.ServiceRequest);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", teamAssignmentHistory.Team);
            return View(teamAssignmentHistory);
        }

        // GET: TeamAssignmentHistories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAssignmentHistory teamAssignmentHistory = await db.TeamAssignmentHistories.FindAsync(id);
            if (teamAssignmentHistory == null)
            {
                return HttpNotFound();
            }
            return View(teamAssignmentHistory);
        }

        // POST: TeamAssignmentHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TeamAssignmentHistory teamAssignmentHistory = await db.TeamAssignmentHistories.FindAsync(id);
            db.TeamAssignmentHistories.Remove(teamAssignmentHistory);
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