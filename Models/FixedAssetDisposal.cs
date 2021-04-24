using FixedModules.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Remark { get; set; }
        public int CompanyId { get; set; }
        public string CreationStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
        public IEnumerable<FixedAssetDisposalSubForm> FixedAssetDisposalSubForm { get; set; }
        [NotMapped]
        public string MultiValuesDisposal { get; set; }
        [NotMapped]
        public FixedAssetMDepreciation asset_subCode { get; set; }
        [NotMapped]
        public List<MasterAssetCategory> asset_subCodes { get; set; }
        [NotMapped]
        public List<Fixed_Asset_Register> asset_subCodesFa { get; set; }
        [NotMapped]
        public List<PaymentMode> paymentMode { get; set; }
        [NotMapped]
        public List<TaxCode> taxCode { get; set; }
        [NotMapped]
        public List<Supplier> supplier { get; set; }
        [NotMapped]
        public List<Customer> customer { get; set; }
        [NotMapped]
        public List<ChartOfAccounts> chartOfAccounts { get; set; }

    }

    public class FixedAssetDisposalSubForm
    {
        [Key]
        public int Id { get; set; }
        public int FixedAssetDisposalId { get; set; }
        public string AssetSubCode { get; set; }
        public string SerialNumber { get; set; }
        public string Supplier { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentReference { get; set; }
        public decimal SellAmount { get; set; }
        public string TaxCode { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal AccumatedDepreciation { get; set; }
        public decimal NetBookValue { get; set; }
        public decimal GainLoss { get; set; }
        public string DetailRemark { get; set; }

    }
}
