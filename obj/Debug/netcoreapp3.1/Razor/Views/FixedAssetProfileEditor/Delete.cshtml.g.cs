#pragma checksum "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea094da0b07538a72c04a2b9fcc64e0be26d4d92"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FixedAssetProfileEditor_Delete), @"mvc.1.0.view", @"/Views/FixedAssetProfileEditor/Delete.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Volumes/MyStore/Collection Code/MVC_Core/Views/_ViewImports.cshtml"
using FixedModules;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Volumes/MyStore/Collection Code/MVC_Core/Views/_ViewImports.cshtml"
using FixedModules.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea094da0b07538a72c04a2b9fcc64e0be26d4d92", @"/Views/FixedAssetProfileEditor/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6c5dc3e7f40a24cfa3674cd826f0b83719fd9bd", @"/Views/_ViewImports.cshtml")]
    public class Views_FixedAssetProfileEditor_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FixedAssets.Models.FixedAssetProfileEditor>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Delete</h1>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <h4>FixedAssetProfileEditor</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 15 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AssetCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 18 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.AssetCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 21 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AssetName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 24 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.AssetName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 27 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AssetType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 30 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.AssetType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 33 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AssetCategory));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 36 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.AssetCategory));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 39 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AssetModel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 42 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.AssetModel));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 45 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AssetBrand));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 48 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.AssetBrand));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 51 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.TransactionDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 54 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.TransactionDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 57 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Remarks));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 60 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.Remarks));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 63 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.SupplierID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 66 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.SupplierID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 69 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.PurchaseDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 72 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.PurchaseDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 75 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.InvoiceNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 78 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.InvoiceNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 81 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.TotalUnit));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 84 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.TotalUnit));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 87 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.UnitPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 90 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.UnitPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 93 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.CapitalizeAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 96 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.CapitalizeAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 99 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.DepreciationStartDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 102 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.DepreciationStartDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 105 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.ResidualAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 108 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.ResidualAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 111 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.NetBookValue));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 114 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.NetBookValue));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 117 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.UtilizeLife));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 120 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.UtilizeLife));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 123 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.DepreciationpercentagePer));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 126 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.DepreciationpercentagePer));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 129 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.DepreciationEndDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 132 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.DepreciationEndDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 135 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.FixedAssetAccountID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 138 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.FixedAssetAccountID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 141 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.PL_DepreciationAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 144 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.PL_DepreciationAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 147 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.PS_DepreciationAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 150 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.PS_DepreciationAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 153 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.DisposalGainAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 156 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.DisposalGainAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 159 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.DisposalLossAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 162 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.DisposalLossAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 165 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.writeOfAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 168 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.writeOfAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 171 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AssetSubCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 174 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.AssetSubCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 177 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.RegistrationNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 180 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.RegistrationNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 183 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.SerialNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 186 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.SerialNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 189 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Department));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 192 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.Department));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 195 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Location));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 198 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.Location));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 201 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Asset_UnitPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 204 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.Asset_UnitPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 207 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.AllocationValue));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 210 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.AllocationValue));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 213 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 216 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 219 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.CreationStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 222 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
       Write(Html.DisplayFor(model => model.CreationStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    \r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea094da0b07538a72c04a2b9fcc64e0be26d4d9227631", async() => {
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ea094da0b07538a72c04a2b9fcc64e0be26d4d9227896", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 227 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" /> |\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea094da0b07538a72c04a2b9fcc64e0be26d4d9229667", async() => {
                    WriteLiteral("Back to List");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FixedAssets.Models.FixedAssetProfileEditor> Html { get; private set; }
    }
}
#pragma warning restore 1591
