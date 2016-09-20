using ServeMeHR.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServeMeHR.Controllers
{
    public class StatusTypesController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: StatusTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.StatusTypes.ToListAsync());
        }

        // GET: StatusTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusType statusType = await db.StatusTypes.FindAsync(id);
            if (statusType == null)
            {
                return HttpNotFound();
            }
            return View(statusType);
        }

        // GET: StatusTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StatusTypeDescription")] StatusType statusType)
        {
            if (ModelState.IsValid)
            {
                db.StatusTypes.Add(statusType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(statusType);
        }

        // GET: StatusTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusType statusType = await db.StatusTypes.FindAsync(id);
            if (statusType == null)
            {
                return HttpNotFound();
            }
            return View(statusType);
        }

        // POST: StatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StatusTypeDescription")] StatusType statusType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(statusType);
        }

        // GET: StatusTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusType statusType = await db.StatusTypes.FindAsync(id);
            if (statusType == null)
            {
                return HttpNotFound();
            }
            return View(statusType);
        }

        // POST: StatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StatusType statusType = await db.StatusTypes.FindAsync(id);
            db.StatusTypes.Remove(statusType);
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