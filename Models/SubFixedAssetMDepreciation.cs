using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class SubFixedAssetMDepreciation
    {
        [Key]
        public int Id { get; set; }
        public int FixedAssetMDepreciationId { get; set; }
        public string AssetSubCode { get; set; }
        public double DepreciationAmount { get; set; }
        public string BSDepreciationAccount { get; set; }
        public string PLDepreciationAccount { get; set; }
        
    }
}
