using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VitoshaBank.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
       [HttpGet]
       [Authorize]
        public ActionResult AuthMe()
        {
            return Ok();
        }
    }
}
