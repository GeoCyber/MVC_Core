#pragma checksum "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "814abbbc6386b5cee461681634e3fa2768072387"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Supplier_Index), @"mvc.1.0.view", @"/Views/Supplier/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"814abbbc6386b5cee461681634e3fa2768072387", @"/Views/Supplier/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6c5dc3e7f40a24cfa3674cd826f0b83719fd9bd", @"/Views/_ViewImports.cshtml")]
    public class Views_Supplier_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FixedModules.Models.SupplierAdd>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("searchCoa"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "-1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/files/Supplier.csv"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Import", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "POST", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
  
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


<d");
            WriteLiteral(@"iv class=""content-i"">
    <div class=""content-box"">
        <div class=""element-wrapper"">
            <h6 class=""element-header"">
                Supplier
            </h6>
            <div class=""element-box"" id=""searchData"" style=""display: none; padding: 3px; margin: 5px"">
                <div class=""row"">
                    <div class=""col-sm-12"">
                        <div>
                            <div class=""triangle-topleft""></div>
                            <div class=""rotated"">Filters</div>
                            <div class=""card-body"">
                                <div class=""row"" style="" max-width: 600px; margin: 0 auto;"">
                                    <div class=""col-sm-6"">
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">Code</label>
                                            <div class=""col-sm-12"">
                                          ");
            WriteLiteral("      <input autocomplete=\"off\" class=\"form-control\" id=\"searchCode\" name=\"Code\" type=\"text\"");
            BeginWriteAttribute("value", " value=\"", 2236, "\"", 2244, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                            </div>
                                        </div>
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">Name</label>
                                            <div class=""col-sm-12"">
                                                <input autocomplete=""off"" class=""form-control"" id=""searchName"" name=""Name"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 2733, "\"", 2741, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""col-sm-6"">
                                        <div class=""form-group row"">
                                            <label class=""col-sm-12 control-label col-form-label"">Chart Of Account</label>
                                            <div class=""col-sm-12"">
                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "814abbbc6386b5cee461681634e3fa276807238711395", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 81 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.COAList;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                            </div>
                                        </div>
                                        <div class=""form-group row"" data-select2-id=""76"">
                                            <label class=""col-sm-12 control-label col-form-label"">Active</label><div class=""col-sm-12"" data-select2-id=""75"">
                                                <select class=""form-control"" id=""searchStatus"" name=""SelectedActiveId"">
                                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "814abbbc6386b5cee461681634e3fa276807238713446", async() => {
                WriteLiteral("- ALL -");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "814abbbc6386b5cee461681634e3fa276807238714951", async() => {
                WriteLiteral("Yes");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "814abbbc6386b5cee461681634e3fa276807238716144", async() => {
                WriteLiteral("No");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                                </select>
                                            </div>
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
");
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "814abbbc6386b5cee461681634e3fa276807238718310", async() => {
                WriteLiteral("<input class=\"box btn-primary card-hover card-sm\" style=\"border-radius:5px; cursor:pointer; margin-left:1%\" type=\"button\" value=\"Create\" />");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    <button class=""box btn-secondary"" style=""border-radius:5px; cursor:pointer; margin-left:1%"" data-target=""#Exportmodal"" data-toggle=""modal""> Export</button>
                    <button class=""box btn-outline-secondary"" style=""border-radius:5px; cursor:pointer; margin-left:1%"" data-target=""#Importmodal"" data-toggle=""modal""> Import</button>
                    <button id=""searchButton"" onclick=""toggleSearch()"" class=""box search-filter btn-outline-primary"" style=""border-radius:5px; cursor:pointer; margin-left:1%"">
                        <i class=""mdi mdi-file-find""></i><span");
            BeginWriteAttribute("class", " class=\"", 6382, "\"", 6390, 0);
            EndWriteAttribute();
            WriteLiteral(@">Search</span>
                    </button>
                </div>
                <br>

                <div class=""table-responsive"">
                    <table id=""zero_config"" class=""table table-bordered table-lg table-v2 table-striped"" style=""width:100%"">
                        <thead>
                            <tr>
                                <th>
                                    ");
#nullable restore
#line 132 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 135 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 138 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.ChartOfAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 141 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                               Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                            </tr>\r\n                        </thead>\r\n                        <tbody id=\"tableItems\">\r\n");
#nullable restore
#line 146 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr");
            BeginWriteAttribute("id", " id=\"", 7605, "\"", 7618, 1);
#nullable restore
#line 148 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
WriteAttributeValue("", 7610, item.Id, 7610, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 150 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 153 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 156 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.ChartOfAccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 159 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                                    Write(item.Status ? "Active" : "Inactive");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 162 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </tbody>
                    </table>
                </div>

                <br /><br />
            </div>

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
                                Please click on Export to download supplier<br>

                            </p>
                      ");
            WriteLiteral("  </div>\r\n                        <div class=\"modal-footer\">\r\n");
#nullable restore
#line 187 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"
                             using (Html.BeginForm("Export", "Supplier", FormMethod.Post))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <button class=\"btn btn-secondary\" data-dismiss=\"modal\" type=\"button\"> Close</button><button class=\"btn btn-primary\" type=\"submit\" value=\"Export\"> Export</button>\r\n");
#nullable restore
#line 190 "/Volumes/MyStore/Collection Code/MVC_Core/Views/Supplier/Index.cshtml"

                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </div>
                    </div>
                </div>
            </div>

            <div aria-hidden=""true"" aria-labelledby=""exampleModalLabel"" class=""modal fade bd-example-modal-lg"" id=""Importmodal"" role=""dialog"" tabindex=""-1"">
                <div class=""modal-dialog"" role=""document"">
                    <div class=""modal-content"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "814abbbc6386b5cee461681634e3fa276807238727100", async() => {
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "814abbbc6386b5cee461681634e3fa276807238728079", async() => {
                    WriteLiteral("Download");
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $('#zero_config').dataTable({
            ""bAutoWidth"": false,
            ""searching"": false
        });

        var table = $('#zero_config').dataTable();
        var tableD = $('#zero_config').DataTable();

        $(document).ready(function () {
            $('#tableItems').on('click', 'tr', function () {
                var id = (tableD.row(this).node()).id;
                window.location.href += ""/Edit/"" + id;
            });
        });

        function searchItem() {
            let code = $('#searchData #searchCode').val();
            let name = $('#searchData #searchName').val();
            let coa = $('#searchData #searchCoa').val();
            let status = $('#searchData #searchStatus').val();
            let url = ""Supplier/Search?code="" + code + ""&name="" + name + ""&coa="" + coa + ""&active="" + status;
            table.fnClearTable();
            $.ajax({
                async: false,
                type: 'GET',
                url: url,
   ");
                WriteLiteral(@"             success: function (data) {
                    for (let i = 0; i < data.length; i++) {
                        let status = ""Inactive"";
                        if (data[i]['status']) {
                            status = ""Active"";
                        }

                        let item = [data[i]['code'], data[i]['name'], data[i]['chartOfAccount'], status];
                        tableD.row.add(item).node().id = data[i]['id'];
                    }
                },
                error: function () {
                    alert(""Server error. Please contact your adminisrator!"");
                }
            });

            tableD.draw();
        }

        function toggleSearch() {
            var search = document.getElementById(""searchData"");
            var searchBtn = document.getElementById(""searchButton"");
            if (search.style.display === ""none"") {
                search.style.display = ""block"";
                searchBtn.style.display = ""none"";
    ");
                WriteLiteral("        } else {\r\n                search.style.display = \"none\";\r\n                searchBtn.style.display = \"block\";\r\n            }\r\n        }\r\n    </script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FixedModules.Models.SupplierAdd>> Html { get; private set; }
    }
}
#pragma warning restore 1591
