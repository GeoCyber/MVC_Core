using FixedModules.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class FixedAssetTransfer
    {
        [Key]
        public int Id { get; set; }
        public DateTime TransDate { get; set; }
        public string DocumentCode { get; set; }
        public string Remark { get; set; }
        public string AssetSubCode { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string CreationStatus { get; set; }
        public int CompanyId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
        [NotMapped]
        public string MultiValuesTransfer { get; set; }
        public IEnumerable<FixedAssetTransferSubForm> FixedAssetTransferSubForm { get; set; }

        [NotMapped]
        public FixedAssetMDepreciation asset_subCode { get; set; }
        [NotMapped]
        public List<MasterAssetCategory> asset_subCodes { get; set; }
        [NotMapped]
        public List<Fixed_Asset_Register> asset_subCodesFa { get; set; }
        [NotMapped]
        public List<MasterDepartment> masterDepartment { get; set; }
        [NotMapped]
        public List<MasterAssetLocation> masterAssetLocation { get; set; }

    }

    public class FixedAssetTransferSubForm
    {
        public int Id { get; set; }
        public int FixedAssetTransferId { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string AssetSubCode { get; set; }

    }
}
