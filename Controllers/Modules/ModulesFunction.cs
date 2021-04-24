using FixedModules.Models;
using Newtonsoft.Json;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Controllers.Modules
{
    public class ModulesFunction
    {
        public static AuditTrail ToModel(AuditTrail entity)
        {
            AuditTrail auditTrail = new AuditTrail();
            List<AuditDelta> auditDeltas = new List<AuditDelta>();
            IEnumerable<Difference> differences = null;

            switch (entity.ActionModuleId)
            {
                case ModulesParams.MasterAssetBrand:
                    differences = CompareEntityWithoutList<MasterAssetBrand>(entity.OldData, entity.NewData);
                    if (!string.IsNullOrEmpty(entity.NewData))
                        entity.Identifier = "Code : " + InitializeEntity<MasterAssetBrand>(entity.NewData).Code;
                    else
                        entity.Identifier = "Code : " + InitializeEntity<MasterAssetBrand>(entity.NewData).Code;
                    break;

                case ModulesParams.MasterAssetCategory:
                    differences = CompareEntityWithoutList<MasterAssetCategory>(entity.OldData, entity.NewData);
                    if (!string.IsNullOrEmpty(entity.OldData))
                        entity.Identifier = "Code : " + InitializeEntity<MasterAssetCategory>(entity.NewData).Code;
                    else
                        entity.Identifier = "Code : " + InitializeEntity<MasterAssetCategory>(entity.NewData).Code;
                    break;
                case ModulesParams.MasterAssetLocation:
                    differences = CompareEntityWithoutList<MasterAssetLocation>(entity.OldData, entity.NewData);
                    if (!string.IsNullOrEmpty(entity.OldData))
                        entity.Identifier = "Code : " + InitializeEntity<MasterAssetLocation>(entity.NewData).Code;
                    else
                        entity.Identifier = "Code : " + InitializeEntity<MasterAssetLocation>(entity.NewData).Code;
                    break;
                case ModulesParams.MasterAssetModel:
                    differences = CompareEntityWithoutList<MasterAssetModel>(entity.OldData, entity.NewData);
                    if (!string.IsNullOrEmpty(entity.OldData))
                        entity.Identifier = "Code : " + InitializeEntity<MasterAssetModel>(entity.NewData).Code;
                    else
                        entity.Identifier = "Code : " + InitializeEntity<MasterAssetModel>(entity.NewData).Code;
                    break;
                case ModulesParams.MasterDepartment:
                    differences = CompareEntityWithoutList<MasterDepartment>(entity.OldData, entity.NewData);
                    if (!string.IsNullOrEmpty(entity.OldData))
                        entity.Identifier = "Code : " + InitializeEntity<MasterDepartment>(entity.NewData).Code;
                    else
                        entity.Identifier = "Code : " + InitializeEntity<MasterDepartment>(entity.NewData).Code;
                    break;

                    // throw new Exception(entity..ToString());
            }



            foreach (var diff in differences)
            {
                if (diff.Value1 != diff.Value2)
                {

                    string fieldName = diff.MemberPath;
                    string oldValue = diff.Value1;
                    string newValue = diff.Value2;

                    
                    auditDeltas.Add(new AuditDelta(fieldName, oldValue, newValue));

                    entity.AuditDeltas = auditDeltas;

                }
            }
            return entity;

        }

        private static IEnumerable<Difference> CompareEntityWithoutList<T>(string oldData, string newData)
        {
            var comparer = new ObjectsComparer.Comparer<T>();

            comparer.Compare(InitializeEntity<T>(oldData), InitializeEntity<T>(newData), out IEnumerable<Difference> differences);

            return differences;
        }


        private static T InitializeEntity<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
            { 
            return string.IsNullOrEmpty(data) ? (T)Activator.CreateInstance(typeof(T)) : JsonConvert.DeserializeObject<T>(data);
            }
            string s = data.Replace(@"\", string.Empty);
            string final = s.Trim().Substring(1, (s.Length) - 2);
            return string.IsNullOrEmpty(final) ? (T)Activator.CreateInstance(typeof(T)) : JsonConvert.DeserializeObject<T>(final);

        }
    }
}
