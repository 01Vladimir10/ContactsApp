#pragma checksum "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\Pages\Login.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d80eaa15604aa65f8d9371d34677f0458a72009b"
// <auto-generated/>
#pragma warning disable 1591
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
            __builder.OpenElement(0, "div");
            __builder.OpenElement(1, "div");
            __builder.OpenElement(2, "input");
            __builder.AddAttribute(3, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 8 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\Pages\Login.razor"
                            Username

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Username = __value, Username));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(5, "\r\n    ");
            __builder.OpenElement(6, "div");
            __builder.OpenElement(7, "input");
            __builder.AddAttribute(8, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 11 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\Pages\Login.razor"
                            Password

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(9, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => Password = __value, Password));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(10, "\r\n    ");
            __builder.OpenElement(11, "div");
            __builder.OpenElement(12, "button");
            __builder.AddAttribute(13, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 14 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\Pages\Login.razor"
                          SignIn

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(14, "Login");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n    ");
            __builder.OpenElement(16, "div");
            __builder.OpenElement(17, "p");
            __builder.AddAttribute(18, "style", "color: red;");
            __builder.AddContent(19, 
#nullable restore
#line 17 "D:\Users\Vladimir\RiderProjects\ContactsApp\ContactsApp\Pages\Login.razor"
                                ErrorMessage

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
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
