using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace FixedModules.Models
{
    public class MasterDepartment
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are not  allowed.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Code field is required.")]
        //[StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "The field Code must be a string with a maximum length of 30.")]
        // The "Remote" attribute parameters specify that the validation of the "Email" field should be performed in the "CheckIfExists" action method of the "Department_Master" controller.
        //[Remote("CheckIfExists", "Department_Master", ErrorMessage = "Code Already Exist!")]
        [Remote(action: "CheckIfExists", controller: "Department_Master", AdditionalFields="Id")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
        public bool IsUsed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
    }
}
