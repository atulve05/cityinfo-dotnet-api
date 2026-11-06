using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    public class PollyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
