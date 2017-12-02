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
            var admin = new ApplicationUser()
            {
                FirstName = "Test",
                LastName = "Tester",
                PhoneNumber = "2141112222",
                Email = "Jon@VolleyballLife.com",
                UserName = "2141112222"
            };
            var x = await _userManager.CreateAsync(admin, "volley13");
            return Ok("Created");
        }

        [HttpGet("test")]
        public async Task<IActionResult> test()
        {
            _db.Emails.Add(new Email
            {
                Address = "test"
            });
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}