using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using VBL.Data;
using Microsoft.AspNetCore.Authorization;
using VBL.Core;
using Microsoft.EntityFrameworkCore;
using AAU;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MohawkManController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly EmailManager _emailManager;
        private readonly VBLDbContext _db;

        public MohawkManController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, VBLDbContext db, EmailManager emailManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _emailManager = emailManager;
        }
        [AllowAnonymous]
        [HttpGet("Init")]
        public async Task<IActionResult> Init()
        {
            await _db.EnsureSeedData(_userManager, _roleManager);
            return Ok("Created");
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<IActionResult> test()
        {
            try
            {
                var aau = new MembershipLookupServiceClient();
                var response = await aau.Membership_Verify_LastName_SecuredAsync("2894774d-6d07-e811-966d-a4badb131cf0", "56985473", "54T57FA8", "Oliverson");
            }
            catch(Exception e)
            {

            }
            return Ok();
        }
    }
}