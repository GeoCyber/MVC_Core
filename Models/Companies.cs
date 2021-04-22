using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class Companies
    {
        [Key]
        public int Id { get; set; }
        public string Group { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TimeZone { get; set; }
        public bool Status { get; set; }
        public string RegistrationNumber { get; set; }
        public Nullable<int> GST { get; set; }
        public Nullable<int> TinNo { get; set; }
        public int SST { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
    }
}
