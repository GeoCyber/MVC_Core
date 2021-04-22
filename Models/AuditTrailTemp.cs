using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class AuditTrailTemp
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
