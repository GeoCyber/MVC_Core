#pragma checksum "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80c2da63f7be6a647f5b468e20653a4dc938ae99"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FixedAssetMDepreciation_Preview), @"mvc.1.0.view", @"/Views/FixedAssetMDepreciation/Preview.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"80c2da63f7be6a647f5b468e20653a4dc938ae99", @"/Views/FixedAssetMDepreciation/Preview.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6c5dc3e7f40a24cfa3674cd826f0b83719fd9bd", @"/Views/_ViewImports.cshtml")]
    public class Views_FixedAssetMDepreciation_Preview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FixedModules.Models.Fixed_Asset_Register>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
   ViewData["Title"] = "Purchases Invoice";
    Layout = "~/Views/Shared/_Layout.cshtml"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
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

<div class=""content-i"">
    <div class=""content");
            WriteLiteral("-box\">\n        <div class=\"element-wrapper\">\n            <input type=\"button\" id=\"btnExport\" value=\"Export\" />\n            <div class=\"element-box\">\n\n                <br>\n\n                <div class=\"table-responsive\">\n");
#nullable restore
#line 54 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                     if (ViewBag.error == "Code exists")
                    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
   Write(Html.Raw("<div class=\"alert alert-danger\" role=\"alert\">Code already exists!</div>"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
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
#line 62 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                               Write(Html.DisplayNameFor(model => model.AssetCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                </th>\n                                <th>\n                                    ");
#nullable restore
#line 65 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                               Write(Html.DisplayNameFor(model => model.AssetName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                </th>\n                                <th>\n                                    ");
#nullable restore
#line 68 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                               Write(Html.DisplayNameFor(model => model.PurchaseDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                </th>\n                                <th>\n                                    ");
#nullable restore
#line 71 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                               Write(Html.DisplayNameFor(model => model.AssetCategory));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                </th>\n                                <th>\n                                    ");
#nullable restore
#line 74 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                               Write(Html.DisplayNameFor(model => model.StatusValue));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                </th>\n                                <th>\n                                    ");
#nullable restore
#line 77 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                               Write(Html.DisplayNameFor(model => model.AssetType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                </th>\n                                <th>\n                                    ");
#nullable restore
#line 80 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                               Write(Html.DisplayNameFor(model => model.CreatedBy));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                </th>\n\n                            </tr>\n                        </thead>\n                        <tbody id=\"tableItems\">\n");
#nullable restore
#line 86 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr");
            BeginWriteAttribute("id", " id=\"", 3106, "\"", 3119, 1);
#nullable restore
#line 88 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
WriteAttributeValue("", 3111, item.Id, 3111, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("editable", " editable=\"", 3120, "\"", 3151, 1);
#nullable restore
#line 88 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
WriteAttributeValue("", 3131, item.CreationStatus, 3131, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                    <th>\n                        ");
#nullable restore
#line 90 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                   Write(item.AssetCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </th>\n                    <th>\n                        ");
#nullable restore
#line 93 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                   Write(item.AssetName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </th>\n                    <th>\n                        ");
#nullable restore
#line 96 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                   Write(item.PurchaseDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </th>\n                    <th>\n                        ");
#nullable restore
#line 99 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                   Write(item.AssetCategory);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </th>\n                    <th>\n                        ");
#nullable restore
#line 102 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                   Write(item.StatusValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </th>\n                    <th>\n                        ");
#nullable restore
#line 105 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                   Write(item.AssetType);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </th>\n                    <th>\n                        ");
#nullable restore
#line 108 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
                   Write(item.CreatedBy);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </th>\n\n\n\n\n                </tr>\n");
#nullable restore
#line 115 "/Volumes/MyStore/Collection Code/MVC_Core/Views/FixedAssetMDepreciation/Preview.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </tbody>
                    </table>
                </div>

                <br /><br />
            </div>





        </div>
    </div>
</div>

<input type=""button"" id=""btnExport"" value=""Export"" />
<script type=""text/javascript"" src=""https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js""></script>
<script type=""text/javascript"" src=""https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js""></script>
<script type=""text/javascript"" src=""https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js""></script>
<script type=""text/javascript"">
    $(""body"").on(""click"", ""#btnExport"", function () {
        html2canvas($('#zero_config')[0], {
            onrendered: function (canvas) {
                var data = canvas.toDataURL();
                var docDefinition = {
                    content: [{
                        image: data,
                        width: 500
                    }]
                };
                //pdfMake.createPdf(docDe");
            WriteLiteral("finition).print(\"preview.pdf\");\n                pdfMake.createPdf(docDefinition).open({}, window);\r\n\n            }\n        });\n    });\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FixedModules.Models.Fixed_Asset_Register>> Html { get; private set; }
    }
}
#pragma warning restore 1591
