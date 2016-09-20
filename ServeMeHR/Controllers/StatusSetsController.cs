using ServeMeHR.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServeMeHR.Controllers
{
    public class StatusSetsController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: StatusSets
        public async Task<ActionResult> Index()
        {
            var statusSets = db.StatusSets.Include(s => s.StatusType1);
            return View(await statusSets.ToListAsync());
        }

        // GET: StatusSets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusSet statusSet = await db.StatusSets.FindAsync(id);
            if (statusSet == null)
            {
                return HttpNotFound();
            }
            return View(statusSet);
        }

        // GET: StatusSets/Create
        public ActionResult Create()
        {
            ViewBag.StatusType = new SelectList(db.StatusTypes, "Id", "StatusTypeDescription");
            return View();
        }

        // POST: StatusSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StatusDescription,LastUpdated,Active,StatusType")] StatusSet statusSet)
        {
            if (ModelState.IsValid)
            {
                db.StatusSets.Add(statusSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StatusType = new SelectList(db.StatusTypes, "Id", "StatusTypeDescription", statusSet.StatusType);
            return View(statusSet);
        }

        // GET: StatusSets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusSet statusSet = await db.StatusSets.FindAsync(id);
            if (statusSet == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusType = new SelectList(db.StatusTypes, "Id", "StatusTypeDescription", statusSet.StatusType);
            return View(statusSet);
        }

        // POST: StatusSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StatusDescription,LastUpdated,Active,StatusType")] StatusSet statusSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StatusType = new SelectList(db.StatusTypes, "Id", "StatusTypeDescription", statusSet.StatusType);
            return View(statusSet);
        }

        // GET: StatusSets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusSet statusSet = await db.StatusSets.FindAsync(id);
            if (statusSet == null)
            {
                return HttpNotFound();
            }
            return View(statusSet);
        }

        // POST: StatusSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StatusSet statusSet = await db.StatusSets.FindAsync(id);
            db.StatusSets.Remove(statusSet);
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