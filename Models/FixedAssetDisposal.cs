using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class FixedAssetDisposal
    {
        [Key]
        public int Id { get; set; }
        public DateTime TransDate { get; set; }
        public string DocumentCode { get; set; }
        public bool Status { get; set; }
        public string Remark { get; set; }
        public string Category { get; set; }
        public string AssetSubCode { get; set; }
        public string SerialNumber { get; set; }
        public string Supplier { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentReference { get; set; }
        public decimal SellAmount { get; set; }
        public string TaxCode { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal AccumatedDepreciation { get; set; }
        public decimal NetBookValue { get; set; }
        public decimal GainLoss { get; set; }
        public string DetailRemark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
    }
}
