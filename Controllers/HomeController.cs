using Microsoft.AspNetCore.Mvc;

namespace FidelityTesting.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("🚀 .NET application running");
        }
    }
}