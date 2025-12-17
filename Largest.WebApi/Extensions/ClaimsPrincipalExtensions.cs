using System.Security.Claims;

namespace Largest.WebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var v = user.FindFirst("id")?.Value;
            return int.TryParse(v, out var id) ? id : 0;
        }
    }
}
