using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace App.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
               context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

        public static bool ValidarRolesUsuario(HttpContext context, IdentityRole role)
        {
            return context.User.Identity.IsAuthenticated &&
               context.User.IsInRole(role.Name);
        }
    }
}
