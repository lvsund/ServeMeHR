using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeMeHR.Models
{
    internal class ServiceRequestMetadata
    {
        [Required]
        public string RequestHeading;

        [Required]
        public string RequestDescription;

        [Required]
        public string RequestorID;

        [Required]
        public string RequestorFirstName;

        [Required]
        public string RequestorLastName;

        [Required]
        public string RequestorPhone;

        [Required]
        [EmailAddress]
        public string RequestorEmail;
    }
}