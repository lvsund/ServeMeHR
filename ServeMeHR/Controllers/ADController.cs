using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using ActiveDirectoryLookup;
using System.DirectoryServices.AccountManagement;
using System.Configuration;

namespace ServeMeHR.Controllers
{
    public class ADController : ApiController
    {

        //string domain = ConfigurationManager.AppSettings("LDAPDirectory");
        //Boolean ActiveDirectoryAvailable = Convert.ToBoolean(ConfigurationManager.AppSettings("LDAPAVAILABLE"));
        string Name = string.Empty;
        string EmailAddress = string.Empty;
        string Phone = string.Empty;

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            // enter AD settings
            PrincipalContext AD = new PrincipalContext(ContextType.Domain, "SERVER.A3HR.local");
            // create search user and add criteria 
            UserPrincipal u = new UserPrincipal(AD);
            var user = u.UserPrincipalName;
            var components = User.Identity.Name.Split('\\');
            var username = components.Last();
            u.SamAccountName = username;
            // search for user  
            PrincipalSearcher search = new PrincipalSearcher(u);
            UserPrincipal result = (UserPrincipal)search.FindOne();
            search.Dispose();

            return new string[] { result.DisplayName, result.EmailAddress,username};
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}