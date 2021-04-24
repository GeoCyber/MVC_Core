using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class Format
    {
        [Key]
        public int Id { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string FAMonthDepreciationFormat { get; set; }
        public string FAWriteOffFormat { get; set; }
        public string FADisposalFormat { get; set; }
        public string FATransferFormat { get; set; }
        public string FAProfileEditorFormat { get; set; }
        public string FARelavueFormat { get; set; }
        public string FAAdjustmentFormat { get; set; }
        public string FAReversalFormat { get; set; }
        public string Createdby { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDatetime { get; set; }

    }
}
