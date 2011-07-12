using System.Collections.Generic;
using System.Web.Mvc;
using ciMonitor.ViewModels;

namespace ciMonitor.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly JenkinsRssParser _jenkinsRssParser;
        readonly string[] _serverUris = new[] { "http://buildsrvr01:8080", "http://build02:8080" };

        public HomeController()
        {
            _jenkinsRssParser = new JenkinsRssParser();
        }

        public ActionResult Index()
        {
            var results = LoadBuilds(_serverUris);

            return View(results);
        }

        private List<BuildOutcome> LoadBuilds(IEnumerable<string> feedUris)
        {
            var results = new List<BuildOutcome>();
            foreach (var uri in feedUris)
            {
                results.AddRange(_jenkinsRssParser.ParseJenkinsBuilds(uri));
            }
            return results;
        }
    }
}
