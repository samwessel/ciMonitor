using System;
using System.Web.Mvc;
using ciMonitor.ViewModels;

namespace ciMonitor.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IRssParser _rssParser;

        public HomeController()
            : this(new JenkinsRssParser(new[] { "http://buildsrvr01:8080", "http://build02:8080", "http://awsbuild01.esendex.com:8080/" }))
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
