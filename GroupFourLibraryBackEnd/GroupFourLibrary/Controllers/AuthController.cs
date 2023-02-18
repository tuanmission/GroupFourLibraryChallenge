using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GroupFourLibrary.Services;
using GroupFourLibrary.ViewModels;
using Microsoft.AspNetCore.Cors;

namespace GroupFourLibrary.Controllers
{
    
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IUserService usrService;
        public AuthController(IUserService userService)
        {
            this.usrService = userService;
        }

        [HttpPost]
        [Route("login")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> login(LoginUserViewModel loginViewMdl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Login Model is not Valid or missing parameters");
            }

            var result = await this.usrService.LoginUser(loginViewMdl);
            if (result.IsSuccess == false)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);



        }

        
    }
}
