#pragma checksum "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b4551365a39f54003cf98f0f3b828ee70b3b5411"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shop__ProductListPartial), @"mvc.1.0.view", @"/Views/Shop/_ProductListPartial.cshtml")]
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
#line 3 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\_ViewImports.cshtml"
using EcoPart.Web.UI.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\_ViewImports.cshtml"
using EcoPart.Web.UI.Models.Entities.Membership;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\_ViewImports.cshtml"
using EcoPart.Web.UI.Models.FormModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\_ViewImports.cshtml"
using EcoPart.Web.UI.AppCode.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\_ViewImports.cshtml"
using EcoPart.Web.UI.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b4551365a39f54003cf98f0f3b828ee70b3b5411", @"/Views/Shop/_ProductListPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21e853f66c00e3e6e814a11bbcd771fbe5bec58a", @"/Views/_ViewImports.cshtml")]
    public class Views_Shop__ProductListPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Product>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-flex align-items-center "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<style>\r\n    a {\r\n        text-decoration: none;\r\n        color: darkblue;\r\n    }\r\n\r\n    .image {\r\n        width: 65px;\r\n        height: 65px;\r\n        background-size: cover;\r\n    }\r\n</style>\r\n\r\n");
            WriteLiteral("<div class=\"container\">\r\n    <div class=\"row\">\r\n        <div class=\"col-lg-6\">\r\n            <ul id=\"search-ul\" style=\"position: absolute; left: 25%; background-color: black; width: 40%; z-index: 99; top:20%;\" class=\" mt-3 list-group\">\r\n");
#nullable restore
#line 19 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
                 foreach (Product product in Model)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
                     if (product.DeletedById == null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <li class=\"list-group-item\" style=\"background:linear-gradient(90deg, #020032 0%, darkblue 0%, #75ccde 0%, #829498 100%, #5b5e6c 100%);\r\n\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b4551365a39f54003cf98f0f3b828ee70b3b54116482", async() => {
                WriteLiteral("\r\n                                <div style=\"width:30%;\">\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b4551365a39f54003cf98f0f3b828ee70b3b54116835", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 1002, "~/uploads/images/", 1002, 17, true);
#nullable restore
#line 27 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
AddHtmlAttributeValue("", 1019, product.ImagePath, 1019, 18, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                </div>\r\n                                <div style=\"padding-left:5%;\">\r\n                                    <h6 style=\"font-weight: bold; margin: 0px; font-family: Arial, Helvetica, sans-serif;\">Mehsulun adi : ");
#nullable restore
#line 30 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
                                                                                                                                     Write(product.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h6>\r\n\r\n                                    <p style=\" color: gray; font-weight:bold; margin: 0px;\"> Qiymeti : ");
#nullable restore
#line 32 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
                                                                                                  Write(product.Price);

#line default
#line hidden
#nullable disable
                WriteLiteral(" M</p>\r\n                                    <p style=\" color: red; font-weight: bold; margin: 0px;\"> Mehsulun Kodu : ");
#nullable restore
#line 33 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
                                                                                                        Write(product.PartCodeName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </p>\r\n                                </div>\r\n                            ");
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
#line 25 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
                                                      WriteLiteral(product.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </li>\r\n");
#nullable restore
#line 37 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "C:\Users\omen\Desktop\FinalProject\EcoPart\EcoPart.Web.UI\EcoPartProject\EcoPart.Web.UI\Views\Shop\_ProductListPartial.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </ul>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591
