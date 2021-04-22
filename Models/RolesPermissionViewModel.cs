using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class RolesPermissionViewModel
    {
        public Guid Id { get; set; }
        public string RolesName { get; set; }
        public string NavName { get; set; }
        public Guid NavId { get; set; }
        public string RolesId { get; set; }
        public int Seq { get; set; }
    }
}
