using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VBL.Api
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            return principal.IsInRole("Admin") || principal.IsInRole("MohawkMan");
        }
        public static bool IsMohawkMan(this ClaimsPrincipal principal)
        {
            return principal.IsInRole("MohawkMan");
        }
        public static string UserId(this ClaimsPrincipal principal, string issuer)
        {
            return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && c.Issuer == issuer)?.Value;
        }
    }
}
