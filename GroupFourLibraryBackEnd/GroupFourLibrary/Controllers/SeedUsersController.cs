using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using GroupFourLibrary.ViewModels;
namespace GroupFourLibrary.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class SeedUsersController : ControllerBase
    {
        private UserManager<IdentityUser> usrMgr;
        private RoleManager<IdentityRole> roleMgr;

        public SeedUsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.usrMgr = userManager;
            this.roleMgr = roleManager;
        }

        [HttpGet]
        [Route("seedroles")]
        public async Task<IActionResult> SeedRoles()
        {
            IdentityRole role = new IdentityRole
            {
                Name = "User",
                NormalizedName="stdUser"
            };

            var result = await roleMgr.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
            
        }

        [HttpPost]
        [Route("seeduser")]
        public async Task<IActionResult> SeedUsers(UserModel usrMdl)
        {
            IdentityUser usr = new IdentityUser
            {
                UserName = usrMdl.UserName,
                Email = usrMdl.Email,
                PhoneNumber = usrMdl.PhoneNumber

            };

            var result = await usrMgr.CreateAsync(usr, usrMdl.Password);

            if (result.Succeeded)
            {
                result = await usrMgr.AddToRoleAsync(usr, "User");
                if (result.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

    }
}
