using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class FixedAssetRegisterAttachment
    {
        [Key]
        public int Id { get; set; }
        public string AttachmentPath { get; set; }
        public int FixedAssetRegisterId { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}
