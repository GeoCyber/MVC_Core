using FixedModules.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class FixedAssetWriteOff
    {
        [Key]
        public int Id { get; set; }
        public DateTime TransDate { get; set; }
        public string DocumentCode { get; set; }
        public string CreationStatus { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
        public IEnumerable<FixedAssetWriteOffSubForm> FixedAssetWriteOffSubForm { get; set; }
        [NotMapped]
        public string MultiValuesWriteOff { get; set; }
        [NotMapped]
        public FixedAssetMDepreciation asset_subCode { get; set; }
        [NotMapped]
        public List<MasterAssetCategory> asset_subCodes { get; set; }
        [NotMapped]
        public List<Fixed_Asset_Register> asset_subCodesFa { get; set; }
        //public List<Fixed_Asset_Register> asset_subCode { get; set; }
        
    }

    public class FixedAssetWriteOffSubForm
    {   
        public int Id { get; set; }
        public int FixedAssetWriteOffId { get; set; }
        public string AssetSubCode { get; set; }
        public string SerialNumber { get; set; }
        public string Remark { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal AccumulatedDepreciation { get; set; }
        public decimal NetBookValue { get; set; }

    }
}

