using System.ComponentModel.DataAnnotations;

namespace FixedAssets.Models
{
    public class AllModuleFormSub
    {
        [Key]

        public int FuckKey { get; set; }
        public string asssubcode { get; set; }
        public string registrationno { get; set; }
        public string serialno { get; set; }
        public string departmnt { get; set; }
        public string location { get; set; }
        public string unitprc { get; set; }
        public string allocationval { get; set; }
    }
}
