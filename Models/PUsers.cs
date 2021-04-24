using System.ComponentModel.DataAnnotations;


namespace FixedModules.Models
{
    public class PUsers
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsInvited { get; set; }
    }
}
