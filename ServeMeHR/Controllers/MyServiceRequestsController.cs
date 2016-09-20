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
    public class MyServiceRequestsController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        // GET: MyServiceRequests
        public async Task<ActionResult> Index(string StatusType, string searchString)
        {
            IEnumerable<SelectListItem> statusitems = db.StatusTypes.Select(c => new SelectListItem
            {
                Selected = c.Id == 1,
                Value = c.StatusTypeDescription,
                Text = c.StatusTypeDescription
            });
            ViewBag.StatusType = statusitems;

            ViewBag.CurrentFilter = searchString;
            if (StatusType == null)
                StatusType = "Open";

            var serviceRequests = db.ServiceRequests.Include(s => s.Member1).Include(s => s.Priority1).Include(s => s.RequestType1).Include(s => s.RequestTypeStep1).Include(s => s.StatusSet).Include(s => s.Team1);
            serviceRequests = from sr in db.ServiceRequests
                              join ss in db.StatusSets on sr.Status equals ss.Id
                              join st in db.StatusTypes on ss.StatusType equals st.Id
                              where sr.RequestorID.Contains(User.Identity.Name) & st.StatusTypeDescription == StatusType
                              orderby sr.DateTimeSubmitted descending
                              select sr;

            if (!String.IsNullOrEmpty(searchString))
            {
                serviceRequests = serviceRequests.Where(s => s.RequestHeading.Contains(searchString)
                                       || s.RequestDescription.Contains(searchString));
            }

            return View(await serviceRequests.ToListAsync());
        }

        // GET: ServiceRequests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.FileUp = db.ApplicConfs.Select(s => s.FileSystemUpload).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = await db.ServiceRequests.FindAsync(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Create
        public ActionResult Create()
        {
            List<Team> lstTeam = db.Teams.ToList();
            //lstTeam.Insert(0, new Models.Team { Id = 0, TeamDescription = "--Select Team--" });
            ViewBag.Team = new SelectList(lstTeam, "Id", "TeamDescription");
            ViewBag.Member = new SelectList(db.Members, "Id", "MemberFullName");
            ViewBag.Priority = new SelectList(db.Priorities, "Id", "PriorityDescription");
            ViewBag.RequestType = new SelectList(db.RequestTypes, "Id", "RequestTypeDescription");
            ViewBag.RequestTypeStep = new SelectList(db.RequestTypeSteps, "Id", "StepDescription");
            ViewBag.Status = new SelectList(db.StatusSets, "Id", "StatusDescription");

            //var components = User.Identity.Name.Split('\\');
            //var username = components.Last();
            //// create LDAP connection object
            //DirectoryEntry myLdapConnection = createDirectoryEntry();

            //search for user
            string homePhone;
            string givenName;
            string surname;
            string email;
            //DirectorySearcher search = new DirectorySearcher(myLdapConnection);
            //search.Filter = "(cn=" + username + ")";
            //SearchResult result = search.FindOne();
            //DirectoryEntry dsresult = result.GetDirectoryEntry();
            //givenName = dsresult.Properties["givenName"][0].ToString();
            //surname = dsresult.Properties["sn"][0].ToString();
            //email = dsresult.Properties["mail"][0].ToString();
            //homePhone = dsresult.Properties["homePhone"][0].ToString();

            var model = new ServiceRequest();
            model.RequestorID = User.Identity.Name;

            Boolean ADconf = db.ApplicConfs.Select(s => s.ADActive).FirstOrDefault();

            ViewBag.FileUp = db.ApplicConfs.Select(s => s.FileSystemUpload).FirstOrDefault();

            if (ADconf)
            {
                GetADinfo(out givenName, out surname, out homePhone, out email);

                model.RequestorFirstName = givenName;
                model.RequestorLastName = surname;
                model.RequestorEmail = email;
                model.RequestorPhone = homePhone;
                model.RequestType = 1;
            }

            return View(model);
        }

        // POST: ServiceRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,RequestHeading,RequestDescription,RequestorID,RequestorFirstName,RequestorLastName,RequestorPhone,RequestorEmail,DateTimeSubmitted,DateTimeStarted,DateTimeCompleted,Priority,RequestType,RequestTypeStep,Member,Status,Team,RowVersion")] ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                string homePhone;
                string givenName;
                string surname;
                string email;
                ////=============================================================================
                ViewBag.RequestTypeStep = new SelectList(db.RequestTypeSteps, "Id", "StepDescription");
                ViewBag.Status = new SelectList(db.StatusSets, "Id", "StatusDescription");

                Boolean ADconf = db.ApplicConfs.Select(s => s.ADActive).FirstOrDefault();

                if (ADconf)
                {
                    GetADinfo(out givenName, out surname, out homePhone, out email);
                    ViewBag.RequestorFirstName = givenName;
                    ViewBag.RequestorLastName = surname;
                    ViewBag.RequestorPhone = homePhone;
                    ViewBag.RequestorEmail = email;
                }

                //Attached File Processing========================================================

                List<FileDetail> fileDetails = new List<FileDetail>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);

                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }

                serviceRequest.FileDetails = fileDetails;

                //================================================================================

                //================================================================================
                // add service request
                db.ServiceRequests.Include(i => i.IndividualAssignmentHistories).Include(i => i.TeamAssignmentHistories).Include(i => i.FileDetails);
                db.ServiceRequests.Add(serviceRequest);
                serviceRequest.DateTimeSubmitted = DateTime.Now;
                serviceRequest.Status = 1;
                serviceRequest.RequestorID = User.Identity.Name;
                //serviceRequest.RequestorFirstName = givenName;
                //serviceRequest.RequestorLastName = surname;
                //serviceRequest.RequestorPhone = homePhone;
                //serviceRequest.RequestorEmail = email;

                //================================================================================
                //Create a history record for team assignment

                serviceRequest.TeamAssignmentHistories.Add(new TeamAssignmentHistory()
                {
                    DateAssigned = DateTime.Now,
                    AssignedBy = User.Identity.Name,
                    ServiceRequest = serviceRequest.Id,
                    Team = serviceRequest.Team
                });
                //================================================================================

                //================================================================================
                //Create history record for individual assignment
                serviceRequest.IndividualAssignmentHistories.Add(new IndividualAssignmentHistory()
                {
                    DateAssigned = DateTime.Now,
                    AssignedBy = User.Identity.Name,
                    //AssignedTo = "A3HR.Lyndon",
                    //AssignedTo = serviceRequest.Member.Value,
                    AssignedTo = 1,

                    ServiceRequest = serviceRequest.Id,
                });
                //================================================================================

                //Create request step history record===============================================
                serviceRequest.StepHistories.Add(new StepHistory()
                {
                    LastUpdated = DateTime.Now,
                    //RequestTypeStep = serviceRequest.RequestTypeStep.Value,
                    RequestTypeStep = 1,

                    ServiceRequest = serviceRequest.Id
                });

                //=================================================================================

                await db.SaveChangesAsync();
                //Check and see if in application configuration email confirmation is set on========
                Boolean emailconf = db.ApplicConfs.Select(s => s.EmailConfirmation).FirstOrDefault();

                if (emailconf)
                {
                    SendStatusConfirmation(1, serviceRequest.RequestorEmail);
                }
                //=================================================================================

                return RedirectToAction("Index");
            }

            ViewBag.Member = new SelectList(db.Members, "Id", "MemberUserid", serviceRequest.Member);
            ViewBag.Priority = new SelectList(db.Priorities, "Id", "PriorityDescription", serviceRequest.Priority);
            ViewBag.RequestType = new SelectList(db.RequestTypes, "Id", "RequestTypeDescription", serviceRequest.RequestType);
            ViewBag.RequestTypeStep = new SelectList(db.RequestTypeSteps, "Id", "StepDescription", serviceRequest.RequestTypeStep);
            ViewBag.Status = new SelectList(db.StatusSets, "Id", "StatusDescription", serviceRequest.Status);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", serviceRequest.Team);

            return View(serviceRequest);
        }

        // GET: ServiceRequests/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ServiceRequest serviceRequest = await db.ServiceRequests.FindAsync(id);
            ServiceRequest serviceRequest = await db.ServiceRequests.Include(s => s.FileDetails).SingleOrDefaultAsync(x => x.Id == id);

            if (serviceRequest == null)
            {
                return HttpNotFound();
            }

            ViewBag.Member = new SelectList(db.Members, "Id", "MemberUserid", serviceRequest.Member);
            ViewBag.Priority = new SelectList(db.Priorities, "Id", "PriorityDescription", serviceRequest.Priority);
            ViewBag.RequestType = new SelectList(db.RequestTypes, "Id", "RequestTypeDescription", serviceRequest.RequestType);
            ViewBag.RequestTypeStep = new SelectList(db.RequestTypeSteps, "Id", "StepDescription", serviceRequest.RequestTypeStep);
            ViewBag.Status = new SelectList(db.StatusSets, "Id", "StatusDescription", serviceRequest.Status);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", serviceRequest.Team);

            //var lastTeam = db.TeamAssignmentHistories.Include(t => t.ServiceRequest1).Include(t =>t.Team1);
            //lastTeam = from tah in db.TeamAssignmentHistories
            //           where tah.ServiceRequest == id
            //           orderby tah.DateAssigned descending
            //           select tah;
            //int lteam;
            //lteam = lastTeam.FirstOrDefault().Team;

            ViewBag.FileUp = db.ApplicConfs.Select(s => s.FileSystemUpload).FirstOrDefault();

            return View(serviceRequest);
        }

        // POST: ServiceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,RequestHeading,RequestDescription,RequestorID,RequestorFirstName,RequestorLastName,RequestorPhone,RequestorEmail,DateTimeSubmitted,DateTimeStarted,DateTimeCompleted,Priority,RequestType,RequestTypeStep,Member,Status,Team,RowVersion")] ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                //new files=======================================================================
                //New File Attachments
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            ServiceRequestID = serviceRequest.Id
                        };
                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }

                //=================================================================================

                //db.Entry(serviceRequest).State = EntityState.Modified;

                // If Status changes then populate the datetime started and completed fields and send email confirmation=========

                if (serviceRequest.Status == 2 && serviceRequest.DateTimeStarted == null)
                {
                    serviceRequest.DateTimeStarted = DateTime.Now;
                    SendStatusConfirmation(2, serviceRequest.RequestorEmail);
                }

                if (serviceRequest.Status == 3 && serviceRequest.DateTimeCompleted == null)
                {
                    serviceRequest.DateTimeCompleted = DateTime.Now;
                    SendStatusConfirmation(3, serviceRequest.RequestorEmail);
                }

                db.Entry(serviceRequest).State = EntityState.Modified;

                //===================================================================================

                // If team is modified create new team history record==========================

                var lastTeam = db.TeamAssignmentHistories.Include(t => t.ServiceRequest1).Include(t => t.Team1);
                lastTeam = from tah in db.TeamAssignmentHistories
                           where tah.ServiceRequest == serviceRequest.Id
                           orderby tah.DateAssigned descending
                           select tah;
                int lteam;
                lteam = lastTeam.FirstOrDefault().Team;

                if (serviceRequest.Team != lteam)

                {
                    serviceRequest.TeamAssignmentHistories.Add(new TeamAssignmentHistory()
                    {
                        DateAssigned = DateTime.Now,
                        AssignedBy = User.Identity.Name,
                        ServiceRequest = serviceRequest.Id,
                        Team = serviceRequest.Team
                    });
                }
                //=========================================================================================

                // if individual assigned has changed add individual history record========================
                var lastMember = db.IndividualAssignmentHistories.Include(t => t.ServiceRequest1);
                lastMember = from lm in db.IndividualAssignmentHistories
                             where lm.ServiceRequest == serviceRequest.Id
                             orderby lm.DateAssigned descending
                             select lm;
                int lmember;
                lmember = lastMember.FirstOrDefault().Member.Id;

                if (serviceRequest.Member != lmember)

                {
                    serviceRequest.IndividualAssignmentHistories.Add(new IndividualAssignmentHistory()
                    {
                        DateAssigned = DateTime.Now,
                        AssignedBy = User.Identity.Name,
                        ServiceRequest = serviceRequest.Id,
                        //AssignedTo = serviceRequest.Member.Value,
                        AssignedTo = 1
                    });
                }
                //===========================================================================================

                //===== If request step changes then  create request step history record=====================

                var lastStep = db.StepHistories.Include(t => t.ServiceRequest1);
                lastStep = from ls in db.StepHistories
                           where ls.ServiceRequest == serviceRequest.Id
                           orderby ls.LastUpdated descending
                           select ls;
                int lstep;
                lstep = lastStep.FirstOrDefault().RequestTypeStep;

                if (serviceRequest.RequestTypeStep != lstep)
                {
                    serviceRequest.StepHistories.Add(new StepHistory()
                    {
                        LastUpdated = DateTime.Now,
                        RequestTypeStep = serviceRequest.RequestTypeStep.Value,
                        ServiceRequest = serviceRequest.Id
                    });
                }
                //===========================================================================================

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Member = new SelectList(db.Members, "Id", "MemberUserid", serviceRequest.Member);
            ViewBag.Priority = new SelectList(db.Priorities, "Id", "PriorityDescription", serviceRequest.Priority);
            ViewBag.RequestType = new SelectList(db.RequestTypes, "Id", "RequestTypeDescription", serviceRequest.RequestType);
            ViewBag.RequestTypeStep = new SelectList(db.RequestTypeSteps, "Id", "StepDescription", serviceRequest.RequestTypeStep);
            ViewBag.Status = new SelectList(db.StatusSets, "Id", "StatusDescription", serviceRequest.Status);
            ViewBag.Team = new SelectList(db.Teams, "Id", "TeamDescription", serviceRequest.Team);
            return View(serviceRequest);
        }

        // GET: ServiceRequests/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = await db.ServiceRequests.FindAsync(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServiceRequest serviceRequest = await db.ServiceRequests.FindAsync(id);
            db.ServiceRequests.Remove(serviceRequest);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public JsonResult GetRequestTypeByTeam(int id)
        {
            List<RequestType> requestTypes = new List<RequestType>();
            if (id > 0)
            {
                requestTypes = db.RequestTypes.Where(p => p.Team == id).ToList();
            }
            else
            {
                requestTypes.Insert(0, new RequestType { Id = 0, RequestTypeDescription = "--Select a Team first--" });
            }
            var result = (from r in requestTypes
                          select new
                          {
                              id = r.Id,
                              name = r.RequestTypeDescription
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPrioritiesByTeam(int id)
        {
            List<Priority> priorities = new List<Priority>();
            if (id > 0)
            {
                priorities = db.Priorities.Where(p => p.Team == id).ToList();
            }
            else
            {
                priorities.Insert(0, new Priority { Id = 0, PriorityDescription = "--Select a Team first--" });
            }
            var result = (from r in priorities
                          select new
                          {
                              id = r.Id,
                              name = r.PriorityDescription
                              //}).ToList().OrderBy(x=> x.name);
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMembersByTeam(int id)
        {
            List<Member> members = new List<Member>();

            if (id > 0)
            {
                //members = db.Teams.Where(p => p.Id == id)
                //    .SelectMany(e => e.TeamMembers)
                //    .Select(e => e.Member)
                //    .ToList();

                members = db.TeamMembers.Where(p => p.Team == id)
    .Select(e => e.Member1)
    .ToList();
            }
            else
            {
                members.Insert(0, new Member { Id = 0, MemberFullName = "--Select a Team first--" });
            }
            var result = (from m in members

                          select new
                          {
                              id = m.Id,
                              name = m.MemberFullName
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRequestTypeStepsByRequestType(int id)
        {
            List<RequestTypeStep> requestTypeSteps = new List<RequestTypeStep>();
            if (id > 0)
            {
                requestTypeSteps = db.RequestTypeSteps.Where(p => p.RequestType == id).ToList();
            }
            else
            {
                requestTypeSteps.Insert(0, new RequestTypeStep { Id = 0, StepDescription = "--Select a Request Type first--" });
            }
            var result = (from r in requestTypeSteps
                          select new
                          {
                              id = r.Id,
                              name = r.StepDescription
                              //}).ToList().OrderBy(x=> x.name);
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public DirectoryEntry createDirectoryEntry()
        {
            // create and return new LDAP connection with desired settings

            string ADconn = db.ApplicConfs.Select(s => s.LDAPConn).FirstOrDefault();
            string LDAPConn = db.ApplicConfs.Select(s => s.LDAPPath).FirstOrDefault();

            //string ADconn;
            //ADconn = "SERVER.A3HR.local";
            //string LDAPConn;
            //LDAPConn = "LDAP://SERVER.A3HR.local";
            //DirectoryEntry ldapConnection = new DirectoryEntry("SERVER.A3HR.local");

            //ldapConnection.Path = "LDAP://OU=staffusers,DC=leeds-art,DC=ac,DC=uk";
            //ldapConnection.Path = "LDAP://SERVER.A3HR.local";

            DirectoryEntry ldapConnection = new DirectoryEntry(ADconn);

            ldapConnection.Path = LDAPConn;

            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

            return ldapConnection;
        }

        public void SendStatusConfirmation(int statusstep, string email)
        {
            //=============Send off email confirmation for status=====================
            MailMessage mail = new MailMessage();
            //mail.To.Add(serviceRequest.Team1.TeamEmailAddress);
            //mail.From = new MailAddress(serviceRequest.RequestorEmail);
            //mail.Subject = serviceRequest.RequestHeading;
            //string Body = serviceRequest.RequestDescription;

            string mailFrom = db.ApplicConfs.Select(s => s.ManageHREmail).FirstOrDefault();
            string fromPass = db.ApplicConfs.Select(s => s.ManageHREmailPass).FirstOrDefault();
            string smtpHost = db.ApplicConfs.Select(s => s.SMTPHost).FirstOrDefault();
            int smtpPort = db.ApplicConfs.Select(s => s.SMTPPort).FirstOrDefault().Value;
            Boolean enabSSL = db.ApplicConfs.Select(s => s.EnableSSL).FirstOrDefault().Value;
            mail.To.Add(email);
            //mail.From = new MailAddress("lyndon.sundmark@gmail.com");
            mail.From = new MailAddress(mailFrom);

            switch (statusstep)
            {
                case 1:
                    mail.Subject = "Request received";
                    mail.Body = "Your request has been received";
                    break;

                case 2:
                    mail.Subject = "Request started";
                    mail.Body = "Your request has been started";
                    break;

                case 3:
                    mail.Subject = "Request completed";
                    mail.Body = "Your request has been completed";
                    break;
            }
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            smtp.Host = smtpHost;

            //smtp.Port = 587;
            smtp.Port = smtpPort;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (mailFrom, fromPass);
            //smtp.EnableSsl = true;
            smtp.EnableSsl = enabSSL;

            smtp.Send(mail);
            //===========================================================================================
        }

        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }

        [HttpPost]
        public JsonResult DeleteFile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);
                FileDetail fileDetail = db.FileDetails.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FileDetails.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public void GetADinfo(out string givenName, out string surname, out string homePhone, out string email)
        {
            //===========================================================
            //Go and get AD info for the current user or equivalent
            var components = User.Identity.Name.Split('\\');
            var username = components.Last();
            // create LDAP connection object
            DirectoryEntry myLdapConnection = createDirectoryEntry();

            //search for user
            //string homePhone;
            //string givenName;
            //string surname;
            //string email;
            DirectorySearcher search = new DirectorySearcher(myLdapConnection);
            search.Filter = "(cn=" + username + ")";
            SearchResult result = search.FindOne();
            DirectoryEntry dsresult = result.GetDirectoryEntry();
            givenName = dsresult.Properties["givenName"][0].ToString();
            surname = dsresult.Properties["sn"][0].ToString();
            email = dsresult.Properties["mail"][0].ToString();
            homePhone = dsresult.Properties["homePhone"][0].ToString();
            //=============================================================================
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