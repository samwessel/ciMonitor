using System.Web.Mvc;
using ciMonitor.ViewModels;

namespace ciMonitor.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IRssParser _rssParser;

        public HomeController()
            : this(new JenkinsRssParser())
        {
        }

        public HomeController(IRssParser rssParser)
        {
            _rssParser = rssParser;
        }

        public ViewResult Index()
        {
            return View(new BuildOutcomesViewModel(_rssParser.LoadBuilds()));
        }

        public ViewResult Builds()
        {
            return View(new BuildOutcomesViewModel(_rssParser.LoadBuilds()));
        }
    }
}
