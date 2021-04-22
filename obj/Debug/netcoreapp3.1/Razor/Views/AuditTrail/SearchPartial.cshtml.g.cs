#pragma checksum "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0c0653b3c372e3af8846df990c00d596cb368fe2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AuditTrail_SearchPartial), @"mvc.1.0.view", @"/Views/AuditTrail/SearchPartial.cshtml")]
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
#line 1 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/_ViewImports.cshtml"
using FixedModules;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/_ViewImports.cshtml"
using FixedModules.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c0653b3c372e3af8846df990c00d596cb368fe2", @"/Views/AuditTrail/SearchPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6c5dc3e7f40a24cfa3674cd826f0b83719fd9bd", @"/Views/_ViewImports.cshtml")]
    public class Views_AuditTrail_SearchPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FixedModules.Models.AuditTrail>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
    <div id=""table"" class=""table-responsive"">

        <table id=""zero_config"" class=""table table-bordered table-lg table-v2 table-striped"" style=""width:100%"">
            <thead>
                <tr>
                    <th>
                        Date Time
                    </th>
                    <th>
                        User
                    </th>
                    <th>
                        Module
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
            </thead>
            <tbody id=""tableItems"">
");
#nullable restore
#line 23 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                 if (Model != null)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr");
            BeginWriteAttribute("id", " id=\"", 839, "\"", 852, 1);
#nullable restore
#line 27 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
WriteAttributeValue("", 844, item.Id, 844, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <td id=\"codeData\">");
#nullable restore
#line 28 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                         Write(item.CreatedDatetime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td id=\"nameData\">");
#nullable restore
#line 29 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                         Write(item.UserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td id=\"moduleData\">");
#nullable restore
#line 30 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                           Write(item.ActionModuleId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td id=\"actionData\">");
#nullable restore
#line 31 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                           Write(item.ActionTypeId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n                        <tr>\r\n                            <td colspan=\"4\">\r\n                                ");
#nullable restore
#line 35 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                           Write(item.Identifier);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                                <div class=""row delta-header"">
                                    <div class=""col-sm-4"">
                                        <b>Field</b>
                                    </div>
                                    <div class=""col-sm-4"">
                                        <b>Old Value</b>
                                    </div>
                                    <div class=""col-sm-4"">
                                        <b>New Value</b>
                                    </div>
                                </div>
");
#nullable restore
#line 48 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                 foreach (var delta in item.AuditDeltas)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"row\">\r\n                                        <div class=\"col-sm-4\">\r\n                                            ");
#nullable restore
#line 52 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                       Write(delta.FieldName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </div>\r\n                                        <div class=\"col-sm-4\">\r\n                                            ");
#nullable restore
#line 55 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                       Write(delta.OldValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </div>\r\n                                        <div class=\"col-sm-4\">\r\n                                            ");
#nullable restore
#line 58 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                       Write(delta.NewValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </div>\r\n                                    </div>\r\n");
#nullable restore
#line 61 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                <div class=\"p-t-10\"></div>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 66 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "/Volumes/MyStore/Collection Code/Visual Studio Solution/MVC_Core/Views/AuditTrail/SearchPartial.cshtml"
                     

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n\r\n        </table>\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FixedModules.Models.AuditTrail>> Html { get; private set; }
    }
}
#pragma warning restore 1591