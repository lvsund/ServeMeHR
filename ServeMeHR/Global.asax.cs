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
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ServeMeHR
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ServeMeHREntities db = new ServeMeHREntities();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var StatusTypeExists = db.StatusTypes.FirstOrDefault();

            if (StatusTypeExists == null)

            {
                db.StatusTypes.Add(new StatusType()
                {
                    StatusTypeDescription = "Open"
                });
                db.StatusTypes.Add(new StatusType()
                {
                    StatusTypeDescription = "Closed"
                });
                db.SaveChanges();
            }

            var StatusExists = db.StatusSets.FirstOrDefault();

            if (StatusExists == null)
            {
                db.StatusSets.Add(new StatusSet()
                {
                    StatusDescription = "Not Started",
                    StatusType = 1,
                    LastUpdated = DateTime.Now,
                    Active = true
                });
                db.StatusSets.Add(new StatusSet()
                {
                    StatusDescription = "In Progress",
                    StatusType = 1,
                    LastUpdated = DateTime.Now,
                    Active = true
                });
                db.StatusSets.Add(new StatusSet()
                {
                    StatusDescription = "Completed",
                    StatusType = 2,
                    LastUpdated = DateTime.Now,
                    Active = true
                });
                db.SaveChanges();
            }

            var TeamExists = db.Teams.FirstOrDefault();

            if (TeamExists == null)
            {
                db.Teams.Add(new Team()
                {
                    TeamDescription = "--UnAssigned--",
                    TeamEmailAddress = "unassigned@unassigned.com"
                });
                db.SaveChanges();
                db.Priorities.Add(new Priority()
                {
                    PriorityDescription = "--UnAssigned--",
                    LastUpdated = DateTime.Now,
                    Active = true,
                    Team = 1
                });
                db.RequestTypes.Add(new RequestType()
                {
                    RequestTypeDescription = "--UnAssigned--",
                    LastUpdated = DateTime.Now,
                    Active = true,
                    Team = 1
                });
                db.SaveChanges();
                db.RequestTypeSteps.Add(new RequestTypeStep()
                {
                    RequestType = 1,
                    StepNumber = 1,
                    StepDescription = "--Start--",
                    LastUpdated = DateTime.Now,
                    Active = true
                });
                db.SaveChanges();

                db.Members.Add(new Member()
                {
                    MemberFirstName = "UnAssigned",
                    MemberLastName = "UnAssigned",
                    MemberFullName = "UnAssigned",
                    MemberUserid = "UnAssigned",
                    MemberEmail = "Unassigned@Unassigned.com",
                    MemberPhone = "000 000 0000"
                });
                db.SaveChanges();
                db.TeamMembers.Add(new TeamMember()
                {
                    Member = 1,
                    Team = 1
                });
                db.SaveChanges();
            }
        }
    }
}