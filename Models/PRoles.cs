using System.ComponentModel.DataAnnotations;

namespace FixedModules.Models
{
    public class PRoles
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
