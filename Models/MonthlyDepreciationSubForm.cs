using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class MonthlyDepreciationSubForm
    {
        [Key]
        public int MDepreciationSubFormID { get; set; }
        public int FixedAssetMDepreciationId { get; set; }
        public string asssubcode { get; set; }
        public string damount { get; set; }
        public string bsamount { get; set; }
        public string plamount { get; set; }
    }
}
