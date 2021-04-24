using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class TaxCode
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are not  allowed.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Code field is required.")]
        [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "The field Code must be a string with a maximum length of 30.")]
        [Remote(action: "CheckIfExists", controller: "Asset_Location_Master", AdditionalFields = "Id")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        public string ChartOfAccount { get; set; }
        public decimal Rate { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
        public bool IsUsed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
    }

    public class TaxCodeVM : TaxCode
    {
        public List<int> StatusId { get; set; }
    }
}
