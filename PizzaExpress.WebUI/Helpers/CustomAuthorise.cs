using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PizzaExpress.WebUI.Helpers
{
    public class CustomAuthorise : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //authentication
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //TO DO: fetch user permission from db

                if (!context.HttpContext.User.IsInRole(Roles))
                {
                    context.Result = new RedirectToActionResult("Unauthorize", "Account", new { area = "" });
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
        }
    }
}
