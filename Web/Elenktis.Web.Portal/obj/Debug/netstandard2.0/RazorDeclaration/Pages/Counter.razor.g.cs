#pragma checksum "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\Pages\Counter.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "417d21b1af06e73404e3ad77904dcbdbf7d9065a"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Elenktis.Web.Portal.Pages
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
    [Microsoft.AspNetCore.Components.Layouts.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/counter")]
    public class Counter : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.RenderTree.RenderTreeBuilder builder)
        {
        }
        #pragma warning restore 1998
#line 9 "c:\Weixian\Projects\Src\POCs\Project Elenktis\Web\Elenktis.Web.Portal\Pages\Counter.razor"
            
    int currentCount = 0;

    void IncrementCount()
    {
        currentCount += 2;
    }

#line default
#line hidden
    }
}
#pragma warning restore 1591
