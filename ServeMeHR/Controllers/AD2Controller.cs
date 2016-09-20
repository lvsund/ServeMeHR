using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using ActiveDirectoryLookup;
using System.DirectoryServices;
using System.Configuration;

namespace ServeMeHR.Controllers
{
    public class AD2Controller : ApiController
    {
        // GET: api/AD2
        public IEnumerable<string> Get()
        {
            var components = User.Identity.Name.Split('\\');
            var username = components.Last();
            // create LDAP connection object 
            DirectoryEntry myLdapConnection = createDirectoryEntry();

            //search for user
            string homePhone;
            string givenName;
            string surname;
            string email;
            DirectorySearcher search = new DirectorySearcher(myLdapConnection);
            search.Filter = "(cn=" + username + ")";
            SearchResult result = search.FindOne();
            DirectoryEntry dsresult = result.GetDirectoryEntry();
            givenName= dsresult.Properties["givenName"][0].ToString();
            surname = dsresult.Properties["sn"][0].ToString();
            email= dsresult.Properties["mail"][0].ToString();
            homePhone = dsresult.Properties["homePhone"][0].ToString();

           
            return new string[] {givenName,surname,email, homePhone };
        }


        static DirectoryEntry createDirectoryEntry()
        {
            // create and return new LDAP connection with desired settings  

            DirectoryEntry ldapConnection = new DirectoryEntry("SERVER.A3HR.local");
            //ldapConnection.Path = "LDAP://OU=staffusers,DC=leeds-art,DC=ac,DC=uk";
            ldapConnection.Path = "LDAP://SERVER.A3HR.local";
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

            return ldapConnection;
        }
        // GET: api/AD2/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AD2
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AD2/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AD2/5
        public void Delete(int id)
        {
        }
    }
}
