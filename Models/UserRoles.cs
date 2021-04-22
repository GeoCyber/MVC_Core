using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class UserRoles
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("ApplicationRoles")]
        public string RoleId { get; set; }
    }
}
