using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using ciMonitor.ViewModels;

namespace ciMonitor
{
    public interface IRssParser
    {
        BuildOutcomeCollection LoadBuilds();
    }

    public class JenkinsRssParser : IRssParser
    {
        private readonly string[] _serverUris;
        private readonly BuildOutcomeFactory _buildOutcomeFactory;

        public JenkinsRssParser()
            : this(ConfigurationManager.AppSettings["ServerUris"].Split(','))
        {
        }

        public JenkinsRssParser(string[] serverUris)
        {
            _serverUris = serverUris;
            _buildOutcomeFactory = new BuildOutcomeFactory();
        }

        public BuildOutcomeCollection LoadBuilds()
        {
            var buildOutcomes = new List<BuildOutcome>();
            foreach (var serverUri in _serverUris)
            {
                buildOutcomes.AddRange(GetServerBuildOutcomes(serverUri));
            }

            var excludedBuilds = ConfigurationManager.AppSettings["ExcludedBuilds"].Split(',');
            foreach (var excludedBuild in excludedBuilds)
            {
                buildOutcomes.RemoveAll(build => build.Name.StartsWith(excludedBuild));
            }
            
            return new BuildOutcomeCollection(buildOutcomes);
        }

        private IEnumerable<BuildOutcome> GetServerBuildOutcomes(string serverUri)
        {
            XNamespace rssNamespace = "http://www.w3.org/2005/Atom";
            var xDocument = XDocument.Load(serverUri + "/rssLatest");
            var entryElements = xDocument.Descendants(rssNamespace + "entry");
            var titleElements = entryElements.Select(entry => entry.Element(rssNamespace + "title").Value);
            return titleElements.Select(jenkinsBuildTitle => _buildOutcomeFactory.CreateFrom(jenkinsBuildTitle));
        }
    }
}