using ServeMeHR.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServeMeHR.Controllers
{
    public class IndividualAssignmentHistoriesController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: IndividualAssignmentHistories
        public async Task<ActionResult> Index()
        {
            var individualAssignmentHistories = db.IndividualAssignmentHistories.Include(i => i.ServiceRequest1).Include(i => i.Member);
            return View(await individualAssignmentHistories.ToListAsync());
        }

        // GET: IndividualAssignmentHistories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndividualAssignmentHistory individualAssignmentHistory = await db.IndividualAssignmentHistories.FindAsync(id);
            if (individualAssignmentHistory == null)
            {
                return HttpNotFound();
            }
            return View(individualAssignmentHistory);
        }

        // GET: IndividualAssignmentHistories/Create
        public ActionResult Create()
        {
            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading");
            ViewBag.AssignedTo = new SelectList(db.Members, "Id", "MemberUserid");
            return View();
        }

        // POST: IndividualAssignmentHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AssignedTo,AssignedBy,DateAssigned,ServiceRequest")] IndividualAssignmentHistory individualAssignmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.IndividualAssignmentHistories.Add(individualAssignmentHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", individualAssignmentHistory.ServiceRequest);
            ViewBag.AssignedTo = new SelectList(db.Members, "Id", "MemberUserid", individualAssignmentHistory.AssignedTo);
            return View(individualAssignmentHistory);
        }

        // GET: IndividualAssignmentHistories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndividualAssignmentHistory individualAssignmentHistory = await db.IndividualAssignmentHistories.FindAsync(id);
            if (individualAssignmentHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", individualAssignmentHistory.ServiceRequest);
            ViewBag.AssignedTo = new SelectList(db.Members, "Id", "MemberUserid", individualAssignmentHistory.AssignedTo);
            return View(individualAssignmentHistory);
        }

        // POST: IndividualAssignmentHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AssignedTo,AssignedBy,DateAssigned,ServiceRequest")] IndividualAssignmentHistory individualAssignmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(individualAssignmentHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceRequest = new SelectList(db.ServiceRequests, "Id", "RequestHeading", individualAssignmentHistory.ServiceRequest);
            ViewBag.AssignedTo = new SelectList(db.Members, "Id", "MemberUserid", individualAssignmentHistory.AssignedTo);
            return View(individualAssignmentHistory);
        }

        // GET: IndividualAssignmentHistories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndividualAssignmentHistory individualAssignmentHistory = await db.IndividualAssignmentHistories.FindAsync(id);
            if (individualAssignmentHistory == null)
            {
                return HttpNotFound();
            }
            return View(individualAssignmentHistory);
        }

        // POST: IndividualAssignmentHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IndividualAssignmentHistory individualAssignmentHistory = await db.IndividualAssignmentHistories.FindAsync(id);
            db.IndividualAssignmentHistories.Remove(individualAssignmentHistory);
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