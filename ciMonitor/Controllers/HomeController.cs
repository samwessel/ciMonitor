using System.Web.Mvc;

namespace ciMonitor.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IRssParser _rssParser;

        public HomeController()
            : this(new JenkinsRssParser(new[] { "http://buildsrvr01:8080", "http://build02:8080" }))
        {
        }

        public HomeController(IRssParser rssParser)
        {
            _rssParser = rssParser;
        }

        public ViewResult Index()
        {
            return View(_rssParser.LoadBuilds());
        }
    }
}
