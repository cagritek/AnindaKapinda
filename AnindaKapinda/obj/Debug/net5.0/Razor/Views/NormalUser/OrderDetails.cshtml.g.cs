#pragma checksum "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "41cfd02d0920e748fe8943c310bcc156e6bfa20b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_NormalUser_OrderDetails), @"mvc.1.0.view", @"/Views/NormalUser/OrderDetails.cshtml")]
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
#line 1 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\_ViewImports.cshtml"
using AnindaKapinda;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\_ViewImports.cshtml"
using AnindaKapinda.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\_ViewImports.cshtml"
using AnindaKapinda.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"41cfd02d0920e748fe8943c310bcc156e6bfa20b", @"/Views/NormalUser/OrderDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1eab02c250f7d20c0cf1d24316d43a873532bf1d", @"/Views/_ViewImports.cshtml")]
    public class Views_NormalUser_OrderDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MarketOrderInfoViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
  
    ViewData["Title"] = "OrderDetail";

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
 if (ViewBag.Message != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h6 style=\"color:red\">");
#nullable restore
#line 8 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
                     Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n");
#nullable restore
#line 9 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n<hr />\r\n<div class=\"row\">\r\n    <div class=\"col-md-4\">\r\n        <div>\r\n            <dl class=\"row\">\r\n                <dt class=\"col-sm-6\">\r\n                    ");
#nullable restore
#line 17 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayName("Ad"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 20 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-6\">\r\n                    ");
#nullable restore
#line 23 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayName("Soyad"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 26 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayFor(model => model.Surname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-6\">\r\n                    ");
#nullable restore
#line 29 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayName("Adres Tarifi"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 32 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayFor(model => model.AddressField));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-6\">\r\n                    ");
#nullable restore
#line 35 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayName("İlçe"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 38 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayFor(model => model.District));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-6\">\r\n                    ");
#nullable restore
#line 41 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayName("İl"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 44 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayFor(model => model.City));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-6\">\r\n                    ");
#nullable restore
#line 47 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayName("Sipariş Durumu"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 50 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
               Write(Html.DisplayFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </dd>

            </dl>
        </div>
     

    </div>
    <div class=""col-md-8"">
        <table class=""table"">
            <thead>
                <tr>
                    <th>
                        Ürün Adı
                    </th>
                    <th>
                        Sipariş Adeti
                    </th>
                    <th ></th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 72 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
                 foreach (var item in Model.OrderInfoViewModels)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 76 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.ProductName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 79 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 82 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 86 "C:\Users\mcagr\Desktop\AnındaKapında\Proje\AnindaKapinda\AnindaKapinda\Views\NormalUser\OrderDetails.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MarketOrderInfoViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591