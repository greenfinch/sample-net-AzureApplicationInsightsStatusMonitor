using System.Net;
using System.Web.Mvc;

namespace WebRole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            (new WebClient()).DownloadString("https://google.com");
            (new WebClient()).DownloadString("https://bing.com");
            return View();
        }
    }
}