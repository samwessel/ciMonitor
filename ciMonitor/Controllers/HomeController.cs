using System;
using System.Web.Mvc;

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
            return View(ciMonitor.Builds.Instance.Update(_rssParser.LoadBuilds()));
        }

        public ViewResult Builds()
        {
            try
            {
                return View(ciMonitor.Builds.Instance.Update(_rssParser.LoadBuilds()));
            }
            catch (Exception exception)
            {
                return View("Error", exception);
            }
        }
    }
}
