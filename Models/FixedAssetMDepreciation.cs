using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class FixedAssetMDepreciation
    {
        [Key]
        public int Id { get; set; }
        public DateTime TransDate { get; set; }
        public string DocumentCode { get; set; }
        public bool Status { get; set; }
        public string Remark { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Period { get; set; }
        public string AssetSubCode { get; set; }
        public decimal DepreciationAmount { get; set; }
        public string BSDepreciationAmount { get; set; }
        public string PLDepreciationAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
    }
}
