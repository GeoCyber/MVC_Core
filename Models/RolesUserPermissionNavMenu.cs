using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class RolesUserPermissionNavMenu
    {
        public string RoleId { get; set; }
        public int StatusId { get; set; }
        public Guid NavId { get; set; }
        public string NavName { get; set; }
        public Guid? NavParentMenuId { get; set; }
        public string NavControllerName { get; set; }
        public string NavActionName { get; set; }
    }
}
