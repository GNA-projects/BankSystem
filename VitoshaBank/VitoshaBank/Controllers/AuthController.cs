using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitoshaBank.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
       [HttpGet]
       [Authorize(Roles = "Admin")]
        public ActionResult AuthMe()
        {
            return Ok();
        }
    }
}
