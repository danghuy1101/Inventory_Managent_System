using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace WebUI.Components.Layout.Identity
{
    public interface ICustomAuthorizationService
    {
        bool CustomClaimChecker(ClaimsPrincipal user, string specificClaim);
    }
}
