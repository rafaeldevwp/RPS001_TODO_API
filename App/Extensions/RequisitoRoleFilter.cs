using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Extensions
{
    public class RequisitoRoleFilter : IAuthorizationFilter
    {
        private readonly IdentityRole _identityRole;
        public RequisitoRoleFilter(IdentityRole identityRole)
        {
            _identityRole = identityRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
            if (!CustomAuthorization.ValidarRolesUsuario(context.HttpContext, _identityRole))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
