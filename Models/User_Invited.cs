using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models.Users
{
    public class UserInvited
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}
