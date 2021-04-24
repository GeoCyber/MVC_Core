using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class PaymentMode
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are not  allowed.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Code field is required.")]
       
        [Remote(action: "CheckIfExists", controller: "Payment_Mode", AdditionalFields = "Id")]
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
