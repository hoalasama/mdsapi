using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace mdswebapi.Attributes
{
    public class MyAuthorizeAttribute : TypeFilterAttribute
    {
        public string RoleName { get; set; }
        public string ActionValue { get; set; }
        public MyAuthorizeAttribute(string roleName, string actionValue) : base(typeof(AuthorizeFilter))
        {
            RoleName = roleName;
            ActionValue = actionValue;
            Arguments = new object[] { RoleName, ActionValue };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        public string RoleName { get; set; }
        public string ActionValue { get; set; }
        public AuthorizeFilter(string roleName, string actionValue)
        {
            RoleName = roleName;
            ActionValue = actionValue;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!CanAccessToAction(context.HttpContext))
            {
                context.Result = new ForbidResult();
            }
        }
        private bool CanAccessToAction(HttpContext httpContext)
        {
            var roles = httpContext.User.FindFirstValue(ClaimTypes.Role);
            if (roles.Equals(RoleName))
                return true;

            return false;
        }
    }
}

