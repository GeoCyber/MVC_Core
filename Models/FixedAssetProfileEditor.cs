using FixedModules.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class FixedAssetProfileEditor
    {
		[Key]
		public int Id { get; set; }
		[DisplayName("Code")]

		public string AssetCode { get; set; }

		[DisplayName("Name")]

		public string AssetName { get; set; }

		[DisplayName("Asset Type")]

		public int AssetType { get; set; }

		[NotMapped]
		public string MultiValuesForm { get; set; }


		[DisplayName("Asset Category")]
		public int AssetCategory { get; set; }

		[DisplayName("Asset Model")]
		public int AssetModel { get; set; }
		[DisplayName("Asset Brand")]
		public int AssetBrand { get; set; }
		public DateTime TransactionDate { get; set; }

		[DisplayName("Remark")]
		public string Remarks { get; set; }


		[DisplayName("Supplier")]
		public int SupplierID { get; set; }

		[DisplayName("Purchase Date")]
		public DateTime PurchaseDate { get; set; }

		[DisplayName("Invoice Number")]
		public string InvoiceNumber { get; set; }

		[DisplayName("Total Unit")]

		public string TotalUnit { get; set; }

		[DisplayName("Unit Price")]

		public string UnitPrice { get; set; }
		[DisplayName("Capitalize Amount")]
		public string CapitalizeAmount { get; set; }

		[DisplayName("Depreciation Start Date")]

		public DateTime DepreciationStartDate { get; set; }

		[DisplayName("Residual Amount")]
		public string ResidualAmount { get; set; }
		[DisplayName("Net Book Value")]
		public string NetBookValue { get; set; }


		[DisplayName("Utilize Life")]

		public string UtilizeLife { get; set; }

		[DisplayName("Depreciation Percentage Per")]
		public string DepreciationpercentagePer { get; set; }

		[DisplayName("Depreciation End Date")]
		public DateTime DepreciationEndDate { get; set; }


		[DisplayName("FixedAsset Account")]

		public int FixedAssetAccountID { get; set; }

		[DisplayName("P/L Depreciation Account")]

		public int PL_DepreciationAccount { get; set; }

		[DisplayName("B/S Depreciation Account")]

		public int PS_DepreciationAccount { get; set; }

		[DisplayName("Disposal Gain Account")]

		public int DisposalGainAccount { get; set; }

		[DisplayName("Disposal Loss Account")]

		public int DisposalLossAccount { get; set; }

		[DisplayName("Write Off Account")]

		public int writeOfAccount { get; set; }

		

		[DisplayName("Asset Sub Code")]
		public string AssetSubCode { get; set; }
		[DisplayName("Registration Number")]
		public string RegistrationNumber { get; set; }
		[DisplayName("Serial Number")]
		public string SerialNumber { get; set; }

		[DisplayName("Department")]
		public int Department { get; set; }

		[DisplayName("Location")]
		public int Location { get; set; }
		[DisplayName("Asset Unit Price")]
		public string Asset_UnitPrice { get; set; }

		[DisplayName("Allocation Value")]
		public string AllocationValue { get; set; }

		public bool Status { get; set; }

		[NotMapped]
		[DisplayName("Status")]
		public string StatusValue { get { return Status ? "Yes" : "No"; } }

		public short CreationStatus { get; set; }

		[NotMapped]

		public List<SelectListItem> StaticList { get; set; }

		[NotMapped]
		public List<FixedAssetRegisterAttachment> FixedAssetRegisterAttachment { get; set; }
		[NotMapped]

		public List<Fixed_Asset_Register> Fixed_Asset_Registers { get; set; }

		[NotMapped]
		public List<SelectListItem> static_list { get; set; }
		[NotMapped]
		public List<MasterAssetCategory> asset_Category { get; set; }
		[NotMapped]
		public List<MasterAssetModel> asset_model { get; set; }
		[NotMapped]
		public List<MasterAssetBrand> asset_brand { get; set; }
		[NotMapped]
		public List<SelectListItem> asset_type { get; set; }
		[NotMapped]
		public List<Supplier> supplier { get; set; }

		[NotMapped]
		public List<ChartOfAccounts> fixed_asset_account { get; set; }
		[NotMapped]
		public List<ChartOfAccounts> pl_description_account { get; set; }
		[NotMapped]
		public List<ChartOfAccounts> bs_description_account { get; set; }
		[NotMapped]
		public List<ChartOfAccounts> disposal_gain_account { get; set; }
		[NotMapped]
		public List<ChartOfAccounts> disposal_loss_account { get; set; }
		[NotMapped]
		public List<ChartOfAccounts> write_off_account { get; set; }
		[NotMapped]
		public List<MasterDepartment> department_master { get; set; }
		[NotMapped]
		public List<MasterAssetLocation> asset_location_master { get; set; }

		[NotMapped]
		public List<string> code_format { get; set; }
		[NotMapped]
		public List<string> utilize_life { get; set; }
		public IEnumerable<AllModuleFormSub> MultiFormData { get; set; }


		[NotMapped]

		public List<AnalysisCode> analysis_Codes { get; set; }


		[NotMapped]

		public string[] savepopupdata { get; set; }
		[NotMapped]

		public string[] savepopupdata1 { get; set; }
		[NotMapped]

		public string[] savepopupdata2 { get; set; }
		[NotMapped]

		public string[] savepopupdata3 { get; set; }
		[NotMapped]

		public string[] savepopupdata4 { get; set; }
		[NotMapped]

		public string[] savepopupdata5 { get; set; }


	}
}
