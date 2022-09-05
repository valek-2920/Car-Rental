using LastTest.Application.Entensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Application.Handlers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {

        public PermissionHandler(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        readonly UserManager<IdentityUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        async Task<List<Claim>> ListClaimsAsync(AuthorizationHandlerContext context)
        {
            var identity = (ClaimsIdentity)context.User.Identity; //give me user claim identity
            var user = await _userManager.FindByEmailAsync
                (identity.Claims.FirstOrDefault
                    (s => s.Type.EndsWith("emailaddress", StringComparison.OrdinalIgnoreCase)).Value);

            List<Claim> claims = new List<Claim>();

            foreach (var role in _roleManager.Roles)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name)) //bring user role
                {
                    foreach (var claim in await _roleManager.GetClaimsAsync(role)) //bring user role claims
                    {
                        if (!claims.Contains(claim))
                        {
                            claims.Add(claim);
                        }
                    }
                }
            }
            return claims;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if(context == null)
            {
                //user not authenticated
                context.Fail();
            }
            else
            {
                //list role claims
                var claims = ListClaimsAsync(context).GetAwaiter().GetResult(); //await in a sync method
                var description = requirement._permissionType.GetDescription().Split(':'); //split every time it finds a :
                var claim = new Claim(description.FirstOrDefault(), description.LastOrDefault());

                if (claims.Exists(e => e.Value.Contains(claim.Value)))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
