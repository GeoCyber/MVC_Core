#pragma checksum "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d48f7ad3404dceb7aac2a7a10fccb50cd415f349"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FixedAssetProfileEditor_Index), @"mvc.1.0.view", @"/Views/FixedAssetProfileEditor/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d48f7ad3404dceb7aac2a7a10fccb50cd415f349", @"/Views/FixedAssetProfileEditor/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6c5dc3e7f40a24cfa3674cd826f0b83719fd9bd", @"/Views/_ViewImports.cshtml")]
    public class Views_FixedAssetProfileEditor_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FixedAssets.Models.FixedAssetProfileEditor>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/files/FixedAssets.csv"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Import", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "POST", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<style>
    #searchData .rotated {
        width: 100%;
        color: white;
        margin: 0 auto;
        background: #398fea;
        text-align: center;
    }

    tbody tr:hover {
        cursor: pointer;
    }
</style>

<style type=""text/css"" media=""only screen and (min-width: 1050px)"">
    #searchData .triangle-topleft {
        width: 0;
        height: 0;
        border-top: 90px solid #398fea;
        border-right: 90px solid transparent;
        position: absolute;
    }

    #searchData .rotated {
        transform: rotate(-45deg);
        -webkit-transform: rotate(-45deg);
        -moz-transform: rotate(-45deg);
        -ms-transform: rotate(-45deg);
        -o-transform: rotate(-45deg);
        width: 120px;
        color: white;
        font-weight: bold;
        background-color: #398fea;
        text-align: center;
        position: absolute;
        margin-top: 26px;
        margin-left: -30px;
        background: transparent;
    }
</style>


");
            WriteLiteral(@"<div class=""content-i"">
    <div class=""content-box"">
        <div class=""element-wrapper"">
            <h6 class=""element-header"">
                Fixed Asset Register
            </h6>

            <div class=""element-box"" id=""searchData"" style=""display: none; padding: 3px; margin: 5px"">
                <div class=""row"">
                    <div class=""col-sm-12"">
                        <div>

                            <div class=""triangle-topleft""></div>
                            <div class=""rotated"">Filters</div>

                            <div class=""card-body"">

                                <div class=""row"" style="" max-width: 600px; margin: 0 auto;"">
                                    <div class=""col-sm-12"">
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">Code</label>
                                            <div class=""col-sm-12"">
                   ");
            WriteLiteral("                             <input autocomplete=\"off\" class=\"form-control\" id=\"searchCode\" name=\"Code\" type=\"text\"");
            BeginWriteAttribute("value", " value=\"", 2270, "\"", 2278, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                            </div>
                                        </div>
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">Name</label>
                                            <div class=""col-sm-12"">
                                                <input autocomplete=""off"" class=""form-control"" id=""searchName"" name=""Name"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 2767, "\"", 2775, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                            </div>
                                        </div>
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">Purchase Date</label>
                                            <div class=""col-sm-12"">
                                                <input class=""form-control"" id=""searchPurchaseDate"" name=""PurchaseDate"" type=""date""");
            BeginWriteAttribute("value", " value=\"", 3270, "\"", 3278, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                            </div>
                                        </div>
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">AssetCategory<span class=""value-mandatory""> *<i class=""mdi mdi-value""></i></span></label>
                                            <div class=""col-sm-12"">

");
            WriteLiteral(@"                                            </div>
                                        </div>
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">Status<span class=""value-mandatory""> *<i class=""mdi mdi-value""></i></span></label>
                                            <div class=""col-sm-12"">

");
            WriteLiteral(@"                                            </div>
                                        </div>
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">AssetType<span class=""value-mandatory""> *<i class=""mdi mdi-value""></i></span></label>
                                            <div class=""col-sm-12"">
");
            WriteLiteral(@"                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class=""clearfix""></div>
                            <div class=""btn-container text-center"" style=""padding-bottom: 10px;"">
                                <button onclick=""searchItem()"" type=""submit"" class=""btn btn-primary btn-fix-width"" id=""btn-search"">Search</button>
                                <button onclick=""toggleSearch()"" class=""btn btn-outline-primary btn-fix-width close-filter"" id=""btn-close"">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class=""element-box"">
                <div class=""row"">
                    <input class=""box btn-primary card-hover card-sm"" style=""border-radius:5px; cursor:pointer; margin-left:1%");
            WriteLiteral("\" type=\"button\" value=\"Create\"");
            BeginWriteAttribute("onclick", " onclick=\"", 6730, "\"", 6797, 3);
            WriteAttributeValue("", 6740, "location.href=\'", 6740, 15, true);
#nullable restore
#line 133 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
WriteAttributeValue("", 6755, Url.Action("Create", "Master_AllModule"), 6755, 41, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6796, "\'", 6796, 1, true);
            EndWriteAttribute();
            WriteLiteral(@" />
                    <button class=""box btn-outline-secondary"" style=""border-radius:5px; cursor:pointer; margin-left:1%""> Listing</button>
                    <button id=""searchButton"" onclick=""toggleSearch()"" class=""box search-filter btn-outline-primary"" style=""border-radius:5px; cursor:pointer; margin-left:1%"">
                        <i class=""mdi mdi-file-find""></i><span");
            BeginWriteAttribute("class", " class=\"", 7181, "\"", 7189, 0);
            EndWriteAttribute();
            WriteLiteral(@">Search</span>
                    </button>
                    <button class=""box btn-outline-secondary"" style=""border-radius:5px; cursor:pointer; margin-left:1%"" data-target=""#Importmodal"" data-toggle=""modal""> Import</button>
                    <button class=""box btn-secondary"" style=""border-radius:5px; cursor:pointer; margin-left:1%"" data-target=""#Exportmodal"" data-toggle=""modal""> Export</button>
                </div>
                <br>


                <!--Table Open-->
                <div class=""table-responsive"">
");
#nullable restore
#line 146 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                     if (ViewBag.error == "Code exists")
                    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 148 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
   Write(Html.Raw("<div class=\"alert alert-danger\" role=\"alert\">Code already exists!</div>"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 148 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                                                                                                ;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <table id=""zero_config"" class=""table table-bordered table-lg table-v2 table-striped"" style=""width:100%"">
                        <thead>
                            <tr>
                                <th>
                                    ");
#nullable restore
#line 154 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.TransactionDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    Document No??\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 160 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n\r\n                                <th>\r\n                                    ");
#nullable restore
#line 164 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.AssetCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 167 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.AssetName));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </th>
                                <th>
                                   Created By??
                                </th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 176 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n\r\n                                <td>\r\n                                    ");
#nullable restore
#line 181 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.TransactionDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    Document No??\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 187 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 190 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.AssetCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    ");
#nullable restore
#line 193 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                               Write(Html.DisplayFor(modelItem => item.AssetName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </td>\r\n                                <td>\r\n                                    Created By??\r\n                                </td>\r\n                                <td>\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d48f7ad3404dceb7aac2a7a10fccb50cd415f34919819", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 199 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                                                           WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d48f7ad3404dceb7aac2a7a10fccb50cd415f34922000", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 200 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                                                              WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d48f7ad3404dceb7aac2a7a10fccb50cd415f34924187", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 201 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                                                             WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </td>\r\n                            </tr>");
#nullable restore
#line 203 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                                 }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </tbody>
                    </table>
                    <!--Table Close-->
                </div>

                <!--Model Open-->
                <div aria-hidden=""true"" aria-labelledby=""exampleModalLabel"" class=""modal fade bd-example-modal-lg"" id=""Exportmodal"" role=""dialog"" tabindex=""-1"">
                    <div class=""modal-dialog"" role=""document"">
                        <div class=""modal-content"">
                            <div class=""modal-header"">
                                <h5 class=""modal-title"" id=""exampleModalLabel"">
                                    Export
                                </h5>
                                <button aria-label=""Close"" class=""close"" data-dismiss=""modal"" type=""button""><span aria-hidden=""true""> &times;</span></button>
                            </div>
                            <div class=""modal-body"">

                                <p>
                                    Please click on Export to download A");
            WriteLiteral("ll Inovoices<br>\r\n                                </p>\r\n                            </div>\r\n                            <div class=\"modal-footer\">\r\n");
#nullable restore
#line 226 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                                 using (Html.BeginForm("Export", "Master_AllModule", FormMethod.Post))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button class=\"btn btn-secondary\" data-dismiss=\"modal\" type=\"button\"> Close</button>\r\n                                    <button class=\"btn btn-primary\" type=\"submit\" value=\"Export\"> Export</button>");
#nullable restore
#line 229 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetProfileEditor/Index.cshtml"
                                                                                                                 }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </div>
                        </div>
                    </div>
                </div>

                <div aria-hidden=""true"" aria-labelledby=""exampleModalLabel"" class=""modal fade bd-example-modal-lg"" id=""Importmodal"" role=""dialog"" tabindex=""-1"">
                    <div class=""modal-dialog"" role=""document"">
                        <div class=""modal-content"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d48f7ad3404dceb7aac2a7a10fccb50cd415f34929202", async() => {
                WriteLiteral(@"
                                <div class=""modal-header"">
                                    <h5 class=""modal-title"" id=""exampleModalLabel"">
                                        Import
                                    </h5>
                                    <button aria-label=""Close"" class=""close"" data-dismiss=""modal"" type=""button""><span aria-hidden=""true""> &times;</span></button>
                                </div>
                                <div class=""modal-body"">
                                    <p>
                                        Please select your updated csv file and click on import.<br>If you do not have the csv format, you may download it from here.
                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d48f7ad3404dceb7aac2a7a10fccb50cd415f34930221", async() => {
                    WriteLiteral("Download");
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                    </p>
                                    <div class=""form-group row"">
                                        <label class=""col-sm-1 text-left control-label col-form-label"">File</label>
                                        <div class=""col-sm-11"">
                                            <input type=""file"" id=""importfilesetting"" name=""importfilesetting"" class=""form-control"">
                                        </div>
                                    </div>
                                </div>
                                <div class=""modal-footer"">
                                    <button class=""btn btn-secondary"" data-dismiss=""modal"" type=""button""> Close</button>
                                    <button class=""btn btn-primary"" type=""submit"" value=""Create""> Import</button>
                                </div>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n                <!--Model Open-->\r\n\r\n            </div>\r\n        </div>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"

            <script>$('#zero_config').dataTable({
                    ""bAutoWidth"": false,
                    ""searching"": false
                });

                var table = $('#zero_config').dataTable();
                var tableD = $('#zero_config').DataTable();

                $(document).ready(function () {
                    $('#tableItems').on('click', 'tr', function () {


                        var row = tableD.row(this).node();
                        if (row == null) {
                            var id = $(this).attr(""id"");
                            var iseditable = $(this).attr(""editable"");


                            if (iseditable == 3) $(""#target input select"").prop(""disabled"", true);
                            window.location.href = ""/Master_AllModule/Edit?CreditTermid="" + id;
                        } else {

                            var id = (tableD.row(this).node()).id;
                            var iseditable = $(row).attr(""editable"");



  ");
                WriteLiteral(@"                          if (iseditable == 3) $(""#target input select"").prop(""disabled"", true);
                            window.location.href = ""/Master_AllModule/Edit?CreditTermid="" + id;
                        }
                    });
                });

                function searchItem() {

                    $(""#zero_config"").hide();
                    $(""#zero_config2"").show();

                    let code = $('#searchData #searchCode').val();
                    let name = $('#searchData #searchName').val();
                    let date = $('#searchData #searchPurchaseDate').val();
                    let category = $('#searchData #assetCategory').val();
                    let cstatus = $('#searchData #status').val();
                    let type = $('#searchData #assettype').val();




                    var data = new FormData();
                    data.append(""Code"", code);
                    data.append(""Name"", name);
                    data.append(""Date"", d");
                WriteLiteral(@"ate);
                    data.append(""Category"", category);
                    data.append(""CStatus"", cstatus);
                    data.append(""Type"", type);



                    $.ajax({
                        url: ""/Master_AllModule/SearchFixedAssetRegister"",
                        type: 'POST',
                        data: data,
                        processData: false,
                        contentType: false,
                        success: function (data) {
                            var html = """";
                            $(""#tableItems2"").html("""");
                            $.each(data, function (index, record) {
                                html += '<tr id=' + record.id + ' editable=' + record.creationStatus + '>';
                                html += '<td>' + record.assetCode + '</td>';
                                html += '<td>' + record.assetName + '</td>';
                                html += '<td>' + record.purchaseDate + '</td>';
             ");
                WriteLiteral(@"                   html += '<td>' + record.assetCategory + '</td>';
                                html += '<td>' + record.statusValue + '</td>';
                                html += '<td>' + record.assetType + '</td>';
                                html += '<td>' + record.createdBy + '</td>';
                                html += ""</tr>"";
                            });

                            $(""#tableItems2"").html(html);
                        },
                        error: function () {
                            alert(""Server error. Please contact your adminisrator!"");
                        }
                    });


                }

                function toggleSearch() {
                    $(""#zero_config"").show();
                    $(""#zero_config2"").hide();


                    var search = document.getElementById(""searchData"");
                    var searchBtn = document.getElementById(""searchButton"");
                    if (search.style.display");
                WriteLiteral(@" === ""none"") {
                        search.style.display = ""block"";
                        searchBtn.style.display = ""none"";
                    } else {
                        search.style.display = ""none"";
                        searchBtn.style.display = ""block"";
                    }
                }</script>
        ");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FixedAssets.Models.FixedAssetProfileEditor>> Html { get; private set; }
    }
}
#pragma warning restore 1591
