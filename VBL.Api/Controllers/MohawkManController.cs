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

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MohawkManController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly VBLDbContext _db;

        public MohawkManController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, VBLDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        [AllowAnonymous]
        [HttpGet("Init")]
        public async Task<IActionResult> Init()
        {
            await _db.EnsureSeedData(_userManager, _roleManager);
            return Ok("Created");
        }

        [HttpGet("test")]
        public async Task<IActionResult> test()
        {
            if (await _userManager.FindByNameAsync("7143971038") == null)
            {
                var me = new ApplicationUser()
                {
                    FirstName = "Ed",
                    LastName = "Ratledge",
                    PhoneNumber = "7143971038",
                    Email = "",
                    UserName = "7143971038"
                };

                var x = await _userManager.CreateAsync(me, "Iamrambo1");
            }
            return Ok();
        }
    }
}