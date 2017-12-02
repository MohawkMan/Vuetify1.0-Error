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
        public static string UserId(this ClaimsPrincipal principal, string issuer)
        {
            return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && c.Issuer == issuer)?.Value;
        }
    }
}
