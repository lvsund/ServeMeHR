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
    public class ApplicConfsController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: ApplicConfs
        public async Task<ActionResult> Index()
        {
            return View(await db.ApplicConfs.ToListAsync());
        }

        // GET: ApplicConfs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicConf applicConf = await db.ApplicConfs.FindAsync(id);
            if (applicConf == null)
            {
                return HttpNotFound();
            }
            return View(applicConf);
        }

        // GET: ApplicConfs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicConfs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FileSystemUpload,ADActive,EmailConfirmation,ModifiedBy,Modified,AppAdmin,BackAdmin,LDAPConn,LDAPPath,ManageHREmail,ManageHREmailPass,SMTPHost,SMTPPort,EnableSSL")] ApplicConf applicConf)
        {
            if (ModelState.IsValid)
            {
                if ((applicConf.ADActive == true && (applicConf.LDAPConn == null || applicConf.LDAPPath == null)
                    || (applicConf.EmailConfirmation == true && (applicConf.ManageHREmail == null || applicConf.ManageHREmailPass == null || applicConf.SMTPHost == null || applicConf.SMTPPort == null))

                    ))

                {
                    TempData["msg"] = "<script>alert('If ADActive or Email Confirmation is true you must provide all other LDAP or EMAIL Server information');</script>";
                }
                else
                {
                    db.ApplicConfs.Add(applicConf);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(applicConf);
        }

        // GET: ApplicConfs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicConf applicConf = await db.ApplicConfs.FindAsync(id);
            if (applicConf == null)
            {
                return HttpNotFound();
            }
            return View(applicConf);
        }

        // POST: ApplicConfs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FileSystemUpload,ADActive,EmailConfirmation,ModifiedBy,Modified,AppAdmin,BackAdmin,LDAPConn,LDAPPath,ManageHREmail,ManageHREmailPass,SMTPHost,SMTPPort,EnableSSL")] ApplicConf applicConf)
        {
            if (ModelState.IsValid)
            {
                if ((applicConf.ADActive == true && (applicConf.LDAPConn == null || applicConf.LDAPPath == null)
                    || (applicConf.EmailConfirmation == true && (applicConf.ManageHREmail == null || applicConf.ManageHREmailPass == null || applicConf.SMTPHost == null || applicConf.SMTPPort == null))

                    ))

                {
                    TempData["msg"] = "<script>alert('If ADActive or Email Confirmation is true you must provide all other LDAP or EMAIL Server information');</script>";
                }
                else
                {
                    applicConf.ModifiedBy = User.Identity.Name;
                    applicConf.Modified = DateTime.Now;
                    db.Entry(applicConf).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(applicConf);
        }

        // GET: ApplicConfs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicConf applicConf = await db.ApplicConfs.FindAsync(id);
            if (applicConf == null)
            {
                return HttpNotFound();
            }
            return View(applicConf);
        }

        // POST: ApplicConfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ApplicConf applicConf = await db.ApplicConfs.FindAsync(id);
            db.ApplicConfs.Remove(applicConf);
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