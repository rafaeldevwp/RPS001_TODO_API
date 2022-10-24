using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Extensions
{
    public class RoleAuthorizeAttribute : TypeFilterAttribute
    {
        public RoleAuthorizeAttribute(string roleName) : base(typeof(RequisitoRoleFilter))
        {
            Arguments = new object[] { new IdentityRole(roleName) }; 
        }
    }
}
