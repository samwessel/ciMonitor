using System.Web.Mvc;

namespace ciMonitor.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
