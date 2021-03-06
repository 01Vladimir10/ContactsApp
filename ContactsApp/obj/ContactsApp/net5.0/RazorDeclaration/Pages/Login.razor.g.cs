// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace ContactsApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using ContactsApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\_Imports.razor"
using ContactsApp.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\Pages\Login.razor"
using Domain.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\Pages\Login.razor"
using Domain.Identity;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/login")]
    public partial class Login : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 21 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\Pages\Login.razor"
       
    public string ErrorMessage { get; set; }
    private string Username { get; set; }
    private string Password { get; set; }

    private async Task SignIn()
    {
        try
        {
            var user = await AuthenticationService.SignInAsync(Username, Password);
            if (user != null)
                NavigationManager.NavigateTo("/");
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
        
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAuthenticationService<AppUser> AuthenticationService { get; set; }
    }
}
#pragma warning restore 1591
