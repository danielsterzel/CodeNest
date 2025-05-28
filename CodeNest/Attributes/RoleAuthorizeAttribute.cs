using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeNest.Attributes;

// Attribute class - used to define metadata(data about data). Inheriting from System.Attribute creates an Attribute.
// allows for syntax like this: [RoleAuthorize(...)]


// IAuthorizationFilter - interface defining a single method we implement. Runs before the controller actions is executed

//AuthorizationFilterContext - context object that gives access to HttpContext, route ,metadata, action data, filter metadata
// and a Result property that can be used to short-circuit the pipeline(stop the request and return a response early).

public class RoleAuthorizeAttribute : Attribute,IAuthorizationFilter
{
    private readonly string _role;
    public RoleAuthorizeAttribute(string role) => _role = role;
    
    // Every time a http request is run  it checks
    // the user role from the session, and checks whether the role is the required role. 
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var role = context.HttpContext.Session.GetString("UserRole");
        if (string.IsNullOrEmpty(role) || role != _role)
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }
    }
}