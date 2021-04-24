using FixedAssets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class ChartOfAccounts
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are not  allowed.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Code field is required.")]
        [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "The field Code must be a string with a maximum length of 30.")]
        // The "Remote" attribute parameters specify that the validation of the "Email" field should be performed in the "CheckIfExists" action method of the "Department_Master" controller.
        //[Remote("CheckIfExists", "Department_Master", ErrorMessage = "Code Already Exist!")]
        [Remote(action: "CheckIfExists", controller: "ChartOfAccounts", AdditionalFields = "Id")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
        public bool IsUsed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
        public IEnumerable<ChartOfAccountAnalysisSetting> ChartOfAccountAnalysisSetting { get; set; }
    }

    public class ChartOfAccountsVM : ChartOfAccounts
    {
        public List<int> StatusId { get; set; }
    }

    public class ChartOfAccountRuleList
    {
        public int Id { get; set; }

        public string AnalysisNumber { get; set; }

        //public int? FromAnalysisCodeId { get; set; }

        public string FromAnalysisCode { get; set; }

        //public int? ToAnalysisCodeId { get; set; }

        public string ToAnalysisCode { get; set; }

        public bool Enabled { get; set; }

        public bool? ReadOnly { get; set; }
    }
    
    public class AddChartOfAccountSettingModel
    {
        [Required]
        public bool MapFull { get; set; }
        public int? AnalysisCodeFrom { get; set; }
        public int? AnalysisCodeTo { get; set; }

        [Required]
        public int? SelectedAnalysisNumber { get; set; }
    }

    public class AddChartOfAccountSettingEditModel
    {
        [Required]
        public bool MapFullEdit { get; set; }
        public int? AnalysisCodeFromEdit { get; set; }
        public int? AnalysisCodeToEdit { get; set; }

        [Required]
        public int? SelectedAnalysisNumberEdit { get; set; }
    }

    public class ChartOfAccountAnalysisSetting
    {
        [Key]
        public int Id { get; set; }
        public int ChartOfAccountId { get; set; }
        public int AnalysisNumber { get; set; }
        public bool Enabled { get; set; }
        public int AnalysisCodeSelectionModeId { get; set; }
        public int? AnalysisCodeIdFrom { get; set; }
        public int? AnalysisCodeIdTo { get; set; }
        public string CreatedBy { get; set; }
        public string AnalysisName { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
        public virtual ChartOfAccounts ChartOfAccount { get; set; }
        [NotMapped]
        public List <AnalysisCode> AnalysisCode { get; set; }


    }

    public class ChartOfAccountAnalysisSetting_Mapping
    {
        [Key]
        public int ChartOfAccountAnalysisSetting_Id { get; set; }
        [Key]
        public int AnalysisCode_Id { get; set; }
    }

    public enum AnalysisCodeSelectionMode
    {
        Full = 1,
        Range = 2,
    }
}
