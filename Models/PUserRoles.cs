using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FixedModules.Models
{
    public class PUserRoles
    {
        [Key]
        public int UserRoleId { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
    }
}
