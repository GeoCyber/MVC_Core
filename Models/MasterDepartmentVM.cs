using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class MasterDepartmentVM : MasterDepartment
    {
        public List<int> StatusId { get; set; }
    }
}
