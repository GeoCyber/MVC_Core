using FixedAssets.Models;
using FixedModules.Models;
using FixedModules.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Data
{
    public class MyDbContext : IdentityDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        //public DbSet<RolesNavMenuViewModel> RolesNavMenuViewModel { get; set; }
        public DbSet<RoleMenuPermission> RoleMenuPermission { get; set; }
        public DbSet<NavigationMenu> NavigationMenu { get; set; }
        public DbSet<ApplicationRoles> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        //public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public DbSet<RolesStatus> RolesStatus { get; set; }
        public DbSet<MasterDepartment> MasterDepartment { get; set; }
        public DbSet<MasterAssetLocation> MasterAssetLocation { get; set; }
        public DbSet<MasterAssetCategory> MasterAssetCategory { get; set; }
        public DbSet<MasterAssetModel> MasterAssetModel { get; set; }
        public DbSet<MasterAssetBrand> MasterAssetBrand { get; set; }
        public DbSet<ChartOfAccounts> ChartOfAccounts { get; set; }
        public DbSet<ChartOfAccountAnalysisSetting> ChartOfAccountAnalysisSetting { get; set; }
        public DbSet<ChartOfAccountAnalysisSetting_Mapping> ChartOfAccountAnalysisSetting_Mapping { get; set; }
        public DbSet<PaymentMode> PaymentMode { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<AnalysisCode> AnalysisCode { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<TaxCode> TaxCode { get; set; }
        public DbSet<Setup> Setup { get; set; }
        //public DbSet<FixedModules.Models.UserRoles> AspNetUserRoles { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Country> tblCountry { get; set; }
        public DbSet<State> tblState { get; set; }
        //public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserInvited> UserInvited { get; set; }
        public DbSet<PUserRoles> PUserRoles { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }
        public DbSet<AuditTrailTemp> AuditTrailTemp { get; set; }
        public DbSet<AllModuleFormSub> AllModuleFormSub { get; set; }
        public DbSet<FixedAssetRegisterAttachment> FixedAssetRegisterAttachment { get; set; }
        public DbSet<MonthlyDepreciationSubForm> monthlyDepreciationSubForms { get; set; }
        public DbSet<FixedAssetWriteOffSubForm> FixedAssetWriteOffSubForm { get; set; }



        #region AdditionalTables


        public DbSet<Fixed_Asset_Register> FixedAssetRegisters { get; set; }
        public DbSet<FixedAssetMDepreciation> FixedAssetMDepreciation { get; set; }
        public DbSet<FixedAssetWriteOff> FixedAssetWriteOff { get; set; }
        public DbSet<FixedAssetDisposal> FixedAssetDisposal { get; set; }
        public DbSet<FixedAssetTransfer> FixedAssetTransfer { get; set; }
        //public DbSet<FixedAssetWriteOff> FixedAssetWriteOff { get; set; }
       
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RoleMenuPermission>()
            .HasKey(c => new { c.RoleId, c.NavigationMenuId });

            builder.Entity<ChartOfAccountAnalysisSetting_Mapping>()
                .HasKey(c => new { c.ChartOfAccountAnalysisSetting_Id, c.AnalysisCode_Id });

            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUser>().ToTable("Users");

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
                //in case you chagned the TKey type
                //  entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
                //in case you chagned the TKey type
                //  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
                //in case you chagned the TKey type
                // entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

            });
        }
        //public DbSet<FixedAssetWriteOff> FixedAssetWriteOff { get; set; }
 
        public DbSet<Format> Format { get; set; }
        //public DbSet<FixedAssetWriteOff> FixedAssetWriteOff { get; set; }
 
        public DbSet<FixedAssets.Models.FixedAssetProfileEditor> FixedAssetProfileEditor { get; set; }
        //public DbSet<FixedAssetWriteOff> FixedAssetWriteOff { get; set; }
       
  
       

    }



}
