#pragma checksum "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\Statistics\AverageCityTripCostPerMonth.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "155f1ac3a11357049e353df4b515b1f3dda88e34"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Statistics_AverageCityTripCostPerMonth), @"mvc.1.0.view", @"/Views/Statistics/AverageCityTripCostPerMonth.cshtml")]
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
#line 1 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC.ViewModels.AccountViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC.ViewModels.HomeViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC.ViewModels.TripViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC.ViewModels.TicketViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC.ViewModels.CityViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.DAL.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC.ViewModels.PaymentCard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\_ViewImports.cshtml"
using Lab06.MVC.ViewModels.StatisticsViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"155f1ac3a11357049e353df4b515b1f3dda88e34", @"/Views/Statistics/AverageCityTripCostPerMonth.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ac52d771944ec1ceb691e6d76b3fa1147e2f9a9", @"/Views/_ViewImports.cshtml")]
    public class Views_Statistics_AverageCityTripCostPerMonth : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AverageTripCostPerMonthViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Statistics", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Statistics", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\Statistics\AverageCityTripCostPerMonth.cshtml"
  
    Layout = null;
    var mod = Json.Serialize(Model);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"" />
<link type=""text/css"" rel=""StyleSheet"" href=""https://bootstraptema.ru/plugins/2016/shieldui/style.css"" />
<script src=""https://bootstraptema.ru/plugins/jquery/jquery-1.11.3.min.js""></script>
<script src=""https://bootstraptema.ru/plugins/2016/shieldui/script.js""></script>

<div class=""col-md-8 col-md-offset-2"">
    <div id=""chart"">
    </div>
    <div>
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "155f1ac3a11357049e353df4b515b1f3dda88e346657", async() => {
                WriteLiteral("back to statistics page");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n<script>\r\n    $(function () {\r\n        var modelFromJson = JSON.parse(\'");
#nullable restore
#line 23 "D:\Repositories\Lab01\lab-06-mvc\Lab06.MVC\Views\Statistics\AverageCityTripCostPerMonth.cshtml"
                                   Write(mod);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');
        var statisticsModelArray = [];

        for (var i = 0; i < modelFromJson.cities.length; i++) {
            statisticsModelArray[i] = {
                seriesType: 'line',
                collectionAlias:  modelFromJson.cities[i],
                data: modelFromJson.averageCost[i]
            };
        }

        $(""#chart"").shieldChart({
            theme: ""light"",
            seriesSettings: {
                line: {
                    dataPointText: {
                        enabled: true
                    }
                }
            },
            chartLegend: {
                align: 'center',
                borderRadius: 2,
                borderWidth: 2,
                verticalAlign: 'top'
            },
            exportOptions: {
                image: true,
                print: true
            },
            axisX: {
                categoricalValues: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
       ");
            WriteLiteral(@"     },
            axisY: {
                title: {
                    text: ""Cost, $""
                }
            },
            primaryHeader: {
                text: ""Average cost of a city trip""
            },
            dataSeries: statisticsModelArray
        });
    });
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AverageTripCostPerMonthViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
