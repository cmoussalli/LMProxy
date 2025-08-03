using Microsoft.AspNetCore.Mvc;

namespace MLProxy.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        [Route("api/test")]
        public IActionResult Test()
        {
            return Ok("Hello from MLProxy");
        }
    }
}
