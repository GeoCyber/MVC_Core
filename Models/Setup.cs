using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class Setup
    {
        [Key]
        public int Id { get; set; }
        public bool Monthy_UL { get; set; }
        public bool Daily_UL { get; set; }
        public bool Reducing_Balance_UL { get; set; }
        public string Value { get; set; }
        public int CompanyId { get; set; }
        public bool IsUsed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}
