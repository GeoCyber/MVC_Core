using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Core.Models
{
    public class Func_Try
    {
        [NotMapped]
        [DisplayName("No")]
        [Key]
        public int ID { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public string LastName { get; set; }

    }
}
