using FixedModules.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Display(Name = "ChartOfAccount")]
        public string ChartOfAccount { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
        public string RegistrationNo { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxCode { get; set; }
        public string TaxNumber { get; set; }
        public DateTime TaxExpiry { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }



        public class CustomerVM
        {

        }
    }
}
