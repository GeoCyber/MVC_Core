using FixedModules.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class UserData
    {
        public List<UserViewModel> PUsers { get; set; }
        public IEnumerable<PUserRoles> PUserRoles { get; set; }
        public List<RoleViewModel> PRoles { get; set; }
        
    }
}
