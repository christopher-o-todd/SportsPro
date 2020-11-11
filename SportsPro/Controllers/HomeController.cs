using Microsoft.AspNetCore.Mvc;
using SportsPro.DataLayer;


namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        // serving main index webpage
        public IActionResult Index()
        {
            return View();
        }

        // serving the about page
        [Route("About")]
        public IActionResult About()
        {
            return View();
        }
    }
}