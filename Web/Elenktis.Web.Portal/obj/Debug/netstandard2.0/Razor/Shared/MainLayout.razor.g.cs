#pragma checksum "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3bab66a6a27c03e88dcc7f759f02f17be2fd6083"
// <auto-generated/>
#pragma warning disable 1591
namespace Elenktis.Web.Portal.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#line 2 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#line 3 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\_Imports.razor"
using Microsoft.AspNetCore.Components.Layouts;

#line default
#line hidden
#line 4 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#line 5 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#line 6 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\_Imports.razor"
using Elenktis.Web.Portal;

#line default
#line hidden
#line 7 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\_Imports.razor"
using Elenktis.Web.Portal.Shared;

#line default
#line hidden
#line 8 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\_Imports.razor"
using RestSharp;

#line default
#line hidden
    public class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "sidebar");
            builder.AddMarkupContent(2, "\r\n    ");
            builder.OpenComponent<Elenktis.Web.Portal.Shared.NavMenu>(3);
            builder.CloseComponent();
            builder.AddMarkupContent(4, "\r\n");
            builder.CloseElement();
            builder.AddMarkupContent(5, "\r\n\r\n");
            builder.OpenElement(6, "div");
            builder.AddAttribute(7, "class", "main");
            builder.AddMarkupContent(8, "\r\n    ");
            builder.AddMarkupContent(9, "<div class=\"top-row px-4\">\r\n        <a href=\"http://blazor.net\" target=\"_blank\" class=\"ml-md-auto\">\r\n        <span class=\"fas fa-user\"></span>About</a>\r\n    </div>\r\n\r\n    ");
            builder.OpenElement(10, "div");
            builder.AddAttribute(11, "class", "content px-4");
            builder.AddMarkupContent(12, "\r\n        ");
            builder.AddContent(13, 
#line 14 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\Shared\MainLayout.razor"
         Body

#line default
#line hidden
            );
            builder.AddMarkupContent(14, "\r\n    ");
            builder.CloseElement();
            builder.AddMarkupContent(15, "\r\n");
            builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
