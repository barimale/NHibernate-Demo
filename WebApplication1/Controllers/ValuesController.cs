using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "admin2")]
        [Route("get-admin")]

        public IActionResult test()
        {
            return Ok("You are admin.");
        }

        [HttpGet]
        [Authorize(Roles = "general2")]
        [Route("get-general")]
        public IActionResult GetGeneral()
        {
            return Ok("You are general.");
        }
    }
}
