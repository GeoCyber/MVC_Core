using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Display(Name = "Chart Of Account")]
        public string ChartOfAccount { get; set; }
        public string Remark { get; set; }
        public string RegistrationNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
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

        [DataType(DataType.Date)]
        public DateTime TaxExpiryDate { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
        public bool IsUsed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
    }

    public class SupplierAdd : Supplier
    {
        public List<int> StatusId { get; set; }
    }

}
