using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string ActionModuleId { get; set; }
        public string ActionTypeId { get; set; }
        public int? CompanyId { get; set; }
        public int ObjectId { get; set; }
        public string Identifier { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string UserId { get; set; }
    
        [NotMapped]
        public List<AuditDelta> AuditDeltas { get; set; }
    }

    public class AuditDelta
    {
        public AuditDelta(string fieldName, string oldValue, string newValue)
        {
            FieldName = fieldName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }

    public class ActionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ActionModule
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class OldData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
    }

    [Serializable]
    public class NewData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
    }

    [Serializable]
    public class OldDataAnalysis
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int? AnalysisNumber { get; set; }
        public bool Status { get; set; }
    }

    [Serializable]
    public class NewDataAnalysis
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int? AnalysisNumber { get; set; }
        public bool Status { get; set; }
    }

    [Serializable]
    public class OldDataSupplier
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ChartOfAccount { get; set; }
        public string Remark { get; set; }
        public string RegistrationNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxCode { get; set; }
        public string TaxNumber { get; set; }
        public DateTime? TaxExpiryDate { get; set; }
        public bool Status { get; set; }
    }

    [Serializable]
    public class NewDataSupplier
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ChartOfAccount { get; set; }
        public string Remark { get; set; }
        public string RegistrationNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxCode { get; set; }
        public string TaxNumber { get; set; }
        public DateTime? TaxExpiryDate { get; set; }
        public bool Status { get; set; }
    }

    [Serializable]
    public class OldDataAnalysisSetting
    {
        public string AnalysisNumber { get; set; }
        public string FromAnalysisCode { get; set; }
        public string ToAnalysisCode { get; set; }
        public bool Enabled { get; set; }
    }

    [Serializable]
    public class NewDataAnalysisSetting
    {
        public string AnalysisNumber { get; set; }
        public string FromAnalysisCode { get; set; }
        public string ToAnalysisCode { get; set; }
        public bool Enabled { get; set; }
    }

    [Serializable]
    public class OldDataTax
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ChartOfAccount { get; set; }
        public string Rate { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
    }

    [Serializable]
    public class NewDataTax
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ChartOfAccount { get; set; }
        public string Rate { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; }
    }
}
