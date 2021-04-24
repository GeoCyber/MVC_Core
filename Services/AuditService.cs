using FixedModules.Data;
using FixedModules.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Services
{
    public class AuditService
    {
        public static void InsertActionLog(int? companyId, string userId, string actionType, string actionModule, string oldData,
            string newData)
        {
            string connectionString = Startup.StaticConfig.GetConnectionString("DefaultConnection");
            var optionBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            using var context = new MyDbContext(optionBuilder.Options);
            if (oldData == null && newData == null)
                throw new Exception("no data");

            //only insert if olddata and newdata has difference
            var serializeOldData = oldData != null ? JsonConvert.SerializeObject(oldData) : string.Empty;
            var serializeNewData = newData != null ? JsonConvert.SerializeObject(newData) : string.Empty;

            if (serializeOldData != serializeNewData)
            {
                var actionLog = new AuditTrail()
                {
                    ActionModuleId = actionModule,
                    ActionTypeId = actionType,
                    OldData = serializeOldData,
                    NewData = serializeNewData,
                    CreatedDatetime = DateTime.UtcNow,
                    ObjectId = 1,
                    UserId = userId,
                    CompanyId = companyId
                };

                context.AuditTrail.Add(actionLog);
                context.AuditTrail.AddAsync(actionLog);
                context.SaveChanges();
                context.SaveChangesAsync();
            }
        }

       

        //private static IEnumerable<Difference> CompareEntityWithoutList<T>(string oldData, string newData)
        //{
        //    var comparer = new ObjectsComparer.Comparer<T>();

        //    comparer.Compare(InitializeEntity<T>(oldData), InitializeEntity<T>(newData), out IEnumerable<Difference> differences);

        //    return differences;
        //}

        private static T InitializeEntity<T>(string data)
        {
            return string.IsNullOrEmpty(data) ? (T)Activator.CreateInstance(typeof(T)) : JsonConvert.DeserializeObject<T>(data);
        }


        //public static AuditTrail ToModel(this ActionLog entity, User user, bool summarizeDelta)
        //{
        //    var result = entity.MapTo<ActionLog, AuditTrailListModel>();

        //    result.User = user.Username;

        //    #region Comparer

        //    IEnumerable<Difference> differences = null;

        //    switch (entity.ActionModule)
        //    {
        //        case ActionModule.AccountType:
        //            differences = CompareEntityWithoutList<AccountTypeLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<AccountTypeLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<AccountTypeLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.AdvancePayment:
        //            differences = GetAdvancePaymentLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<AdvancePaymentLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<AdvancePaymentLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.AnalysisCode:
        //            differences = CompareEntityWithoutList<AnalysisCodeLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<AnalysisCodeLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<AnalysisCodeLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.BankAccount:
        //            differences = CompareEntityWithoutList<BankAccountLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Chart Of Account : " + JsonConvert.DeserializeObject<BankAccountLog>(entity.OldData).ChartOfAccount;
        //            else
        //                result.Identifier = "Chart Of Account : " + JsonConvert.DeserializeObject<BankAccountLog>(entity.NewData).ChartOfAccount;
        //            break;
        //        case ActionModule.BatchPayment:
        //            differences = GetBatchPaymentLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<BatchPaymentLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<BatchPaymentLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.Allocation:
        //            differences = CompareEntityWithoutList<AllocationLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<AllocationLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<AllocationLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.ChartOfAccount:
        //            differences = CompareEntityWithoutList<ChartOfAccountLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<ChartOfAccountLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<ChartOfAccountLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.ChartOfAccountSetting:
        //            differences = CompareEntityWithoutList<ChartOfAccountAnalysisSettingLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<ChartOfAccountAnalysisSettingLog>(entity.OldData).ChartOfAccountCode;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<ChartOfAccountAnalysisSettingLog>(entity.NewData).ChartOfAccountCode;
        //            break;
        //        case ActionModule.Company:
        //            differences = CompareEntityWithoutList<CompanyLog>(entity.OldData, entity.NewData);
        //            break;
        //        case ActionModule.CompanyConfig:
        //            differences = CompareEntityWithoutList<CompanyConfigLog>(entity.OldData, entity.NewData);
        //            break;
        //        case ActionModule.CreditTerm:
        //            differences = CompareEntityWithoutList<CreditTermLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<CreditTermLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<CreditTermLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.Currency:

        //            if (entity.ActionType == ActionType.UpdateExchangeRate)
        //            {
        //                differences = CompareEntityWithoutList<CurrencyExchangeRateLog>(entity.OldData, entity.NewData);
        //                if (!string.IsNullOrEmpty(entity.OldData))
        //                    result.Identifier = "Code : " + JsonConvert.DeserializeObject<CurrencyExchangeRateLog>(entity.OldData).Code;
        //                else
        //                    result.Identifier = "Code : " + JsonConvert.DeserializeObject<CurrencyExchangeRateLog>(entity.NewData).Code;
        //            }
        //            else
        //            {
        //                differences = CompareEntityWithoutList<CurrencyLog>(entity.OldData, entity.NewData);
        //                if (!string.IsNullOrEmpty(entity.OldData))
        //                    result.Identifier = "Code : " + JsonConvert.DeserializeObject<CurrencyLog>(entity.OldData).Code;
        //                else
        //                    result.Identifier = "Code : " + JsonConvert.DeserializeObject<CurrencyLog>(entity.NewData).Code;
        //            }

        //            break;
        //        case ActionModule.Customer:
        //            differences = CompareEntityWithoutList<CustomerLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<CustomerLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<CustomerLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.FinancialPeriod:
        //            differences = CompareEntityWithoutList<FinancialPeriodLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Period : " + JsonConvert.DeserializeObject<FinancialPeriodLog>(entity.OldData).Period;
        //            else
        //                result.Identifier = "Period : " + JsonConvert.DeserializeObject<FinancialPeriodLog>(entity.NewData).Period;
        //            break;
        //        case ActionModule.Format:
        //            differences = CompareEntityWithoutList<FormatLog>(entity.OldData, entity.NewData);
        //            break;
        //        case ActionModule.Group:
        //            differences = CompareEntityWithoutList<GroupLog>(entity.OldData, entity.NewData);
        //            break;
        //        case ActionModule.JournalType:
        //            differences = CompareEntityWithoutList<JournalTypeLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<JournalTypeLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<JournalTypeLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.PaymentMode:
        //            differences = CompareEntityWithoutList<PaymentModeLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<PaymentModeLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<PaymentModeLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.PaymentTerm:
        //            differences = CompareEntityWithoutList<PaymentTermLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<PaymentTermLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<PaymentTermLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.PurchaseCreditNote:
        //            differences = GetBillingLogDifferences<PurchaseCreditNoteLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<PurchaseCreditNoteLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<PurchaseCreditNoteLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.PurchaseDebitNote:
        //            differences = GetBillingLogDifferences<PurchaseDebitNoteLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<PurchaseDebitNoteLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<PurchaseDebitNoteLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.PurchaseInvoice:
        //            differences = GetBillingLogDifferences<PurchaseInvoiceLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<PurchaseInvoiceLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<PurchaseInvoiceLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.Role:
        //            differences = GetRoleLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Name : " + JsonConvert.DeserializeObject<RoleLog>(entity.OldData).Name;
        //            else
        //                result.Identifier = "Name : " + JsonConvert.DeserializeObject<RoleLog>(entity.NewData).Name;
        //            break;
        //        case ActionModule.SalesCreditNote:
        //            differences = GetBillingLogDifferences<SalesCreditNoteLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<SalesCreditNoteLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<SalesCreditNoteLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.SalesDebitNote:
        //            differences = GetBillingLogDifferences<SalesDebitNoteLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<SalesDebitNoteLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<SalesDebitNoteLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.SalesInvoice:
        //            differences = GetBillingLogDifferences<SalesInvoiceLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<SalesInvoiceLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<SalesInvoiceLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.SalesReceipt:
        //            differences = GetSalesReceiptLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<SalesReceiptLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<SalesReceiptLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.Supplier:
        //            differences = CompareEntityWithoutList<SupplierLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<SupplierLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<SupplierLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.SupplierBankAccount:
        //            differences = CompareEntityWithoutList<SupplierBankAccountLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<SupplierBankAccountLog>(entity.OldData).Supplier;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<SupplierBankAccountLog>(entity.NewData).Supplier;
        //            break;
        //        case ActionModule.TaxCode:
        //            differences = CompareEntityWithoutList<TaxCodeLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<TaxCodeLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<TaxCodeLog>(entity.NewData).Code;
        //            break;
        //        case ActionModule.User:
        //            differences = GetCompanyUserLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Username : " + JsonConvert.DeserializeObject<CompanyUserLog>(entity.OldData).Email;
        //            else
        //                result.Identifier = "Username : " + JsonConvert.DeserializeObject<CompanyUserLog>(entity.NewData).Email;
        //            break;
        //        case ActionModule.VoidPayment:
        //            differences = GetVoidPaymentLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<VoidPaymentLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<VoidPaymentLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.Report:
        //            differences = CompareEntityWithoutList<CompanyReportLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Name : " + JsonConvert.DeserializeObject<CompanyReportLog>(entity.OldData).Name;
        //            else
        //                result.Identifier = "Name : " + JsonConvert.DeserializeObject<CompanyReportLog>(entity.NewData).Name;
        //            break;
        //        case ActionModule.JournalEntry:
        //            differences = GetJournalEntryLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<JournalEntryLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<JournalEntryLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.JournalTemplate:
        //            differences = GetJournalTemplateLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Name : " + JsonConvert.DeserializeObject<JournalTemplateLog>(entity.OldData).Name;
        //            else
        //                result.Identifier = "Name : " + JsonConvert.DeserializeObject<JournalTemplateLog>(entity.NewData).Name;
        //            break;
        //        case ActionModule.JournalImport:

        //            if (entity.ActionType == ActionType.Import)
        //            {
        //                differences = GetJournalImportDetailLogDifferences(entity.OldData, entity.NewData);
        //                if (!string.IsNullOrEmpty(entity.OldData))
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<JournalImportDetailLog>(entity.OldData).DocumentNumber;
        //                else
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<JournalImportDetailLog>(entity.NewData).DocumentNumber;
        //            }
        //            else
        //            {
        //                differences = CompareEntityWithoutList<JournalImportLog>(entity.OldData, entity.NewData);
        //                if (!string.IsNullOrEmpty(entity.OldData))
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<JournalImportLog>(entity.OldData).DocumentNumber;
        //                else
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<JournalImportLog>(entity.NewData).DocumentNumber;
        //            }
        //            break;
        //        case ActionModule.Statistics:
        //            if (entity.ActionType == ActionType.Import)
        //            {
        //                differences = GetStatisticsDetailLogDifferences(entity.OldData, entity.NewData);
        //                if (!string.IsNullOrEmpty(entity.OldData))
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<StatisticsDetailLog>(entity.OldData).DocumentNumber;
        //                else
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<StatisticsDetailLog>(entity.NewData).DocumentNumber;
        //            }
        //            else
        //            {
        //                differences = CompareEntityWithoutList<StatisticsDetailLog>(entity.OldData, entity.NewData);
        //                if (!string.IsNullOrEmpty(entity.OldData))
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<StatisticsLog>(entity.OldData).DocumentNumber;
        //                else
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<StatisticsLog>(entity.NewData).DocumentNumber;
        //            }
        //            break;
        //        case ActionModule.Budget:
        //            if (entity.ActionType == ActionType.Import)
        //            {
        //                differences = GetBudgetDetailLogDifferences(entity.OldData, entity.NewData);
        //                if (!string.IsNullOrEmpty(entity.OldData))
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<BudgetDetailLog>(entity.OldData).DocumentNumber;
        //                else
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<BudgetDetailLog>(entity.NewData).DocumentNumber;
        //            }
        //            else
        //            {
        //                differences = CompareEntityWithoutList<JournalImportLog>(entity.OldData, entity.NewData);
        //                if (!string.IsNullOrEmpty(entity.OldData))
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<BudgetLog>(entity.OldData).DocumentNumber;
        //                else
        //                    result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<BudgetLog>(entity.NewData).DocumentNumber;
        //            }
        //            break;
        //        case ActionModule.ForexRevaluation:
        //            differences = GetForexRevaluationLogDifferences(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<ForexRevaluationLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<ForexRevaluationLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.BankStatement:
        //            differences = CompareEntityWithoutList<BankStatementTransactionLog>(entity.OldData, entity.NewData);
        //            var bankAccount = EngineContext.Current.Resolve<IBankAccountService>().GetBankAccountById(entity.ObjectId);
        //            result.Identifier = "Bank Chart Of Account : " + bankAccount.ChartOfAccount.Code;
        //            break;
        //        case ActionModule.InitialData:
        //            differences = CompareEntityWithoutList<RefreshDataLog>(entity.OldData, entity.NewData);
        //            break;
        //        case ActionModule.Default:
        //            differences = CompareEntityWithoutList<CompanyDefaultLog>(entity.OldData, entity.NewData);
        //            result.Identifier = "Company : " + EngineContext.Current.Resolve<ICompanyContext>().Company.Code;
        //            break;
        //        case ActionModule.BankAccountTransaction:
        //            differences = CompareEntityWithoutList<BankAccountTransactionLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<BankAccountTransactionLog>(entity.OldData).DocumentNumber;
        //            else
        //                result.Identifier = "Document Number : " + JsonConvert.DeserializeObject<BankAccountTransactionLog>(entity.NewData).DocumentNumber;
        //            break;
        //        case ActionModule.WithholdingTaxCode:
        //            differences = CompareEntityWithoutList<WithholdingTaxCodeLog>(entity.OldData, entity.NewData);
        //            if (!string.IsNullOrEmpty(entity.OldData))
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<WithholdingTaxCodeLog>(entity.OldData).Code;
        //            else
        //                result.Identifier = "Code : " + JsonConvert.DeserializeObject<WithholdingTaxCodeLog>(entity.NewData).Code;
        //            break;

        //        default:
        //            throw new Exception(entity.ActionModule.ToString());
        //    }

        //    if (summarizeDelta)
        //    {
        //        if (differences.Count() > 50)
        //            result.HasMoreDelta = true;

        //        differences = differences.Take(50);
        //    }

        //    foreach (var diff in differences)
        //    {
        //        if (diff.Value1 != diff.Value2)
        //        {
        //            if ((entity.ActionType != ActionType.Allocated && entity.ActionType != ActionType.Unallocated)
        //                || diff.MemberPath == "AllocationNumber")
        //            {
        //                string fieldName = diff.MemberPath;
        //                string oldValue = diff.Value1;
        //                string newValue = diff.Value2;
        //                if (entity.ActionType == ActionType.Split && diff.MemberPath == "DocumentAmount")
        //                {
        //                    fieldName = "SplitAmount";
        //                    newValue = string.Format("Split amount from {0} to {1}", diff.Value1, diff.Value2);
        //                }
        //                else if (entity.ActionType == ActionType.Merged && diff.MemberPath == "DocumentAmount")
        //                {
        //                    fieldName = "MergedAmount";
        //                    oldValue = "";
        //                    newValue = string.Format("Amount merged to {0}", diff.Value2);
        //                }
        //                var delta = new AuditTrailListModel.AuditDelta();
        //                delta.FieldName = fieldName;
        //                delta.OldValue = oldValue;
        //                delta.NewValue = newValue;
        //                result.AuditDeltas.Add(delta);
        //            }

        //        }
        //    }

        //    #endregion

        //    return result;
        //}
    }
}
