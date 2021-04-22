using FixedModules.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FixedModules.Models
{
    public class UserLogin
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public string CompanyId { get; set; }
        public string UserType { get; set; }
        public bool IsLogin { get; set; }
        public bool IsReset { get; set; }
        public bool IsSuspend { get; set; }
        public bool IsInvited { get; set; }
        public string PasswordHash { get; set; }
        public int LoginAttempt { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string SessionID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDatetime { get; set; }
        public string DeclinedBy { get; set; }
        public DateTime? DeclinedDatetime { get; set; }
    }

    public class UserParam
    {
        public string Email { get; set; }
        public List<Guid> RoleId { get; set; }
    }

}
