using ServeMeHR.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServeMeHR.Controllers
{
    public class HomeController : Controller
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        public ActionResult Index()
        {
            var tMems = db.Members.Include(s => s.TeamMembers);

            tMems = from mb in db.Members
                    where mb.MemberUserid == User.Identity.Name
                    select mb;
            if (tMems.FirstOrDefault() == null)
            { ViewBag.IsMember = false; }
            else
            { ViewBag.IsMember = true; }
            //ViewBag.IsMember = true;

            string admin = db.ApplicConfs.Select(s => s.AppAdmin).FirstOrDefault();
            string badmin = db.ApplicConfs.Select(s => s.BackAdmin).FirstOrDefault();
            ViewBag.uadmin = admin;
            ViewBag.ubadmin = badmin;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}