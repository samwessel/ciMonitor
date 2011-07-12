using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ciMonitor.ViewModels;

namespace ciMonitor
{
    public interface IRssParser
    {
        IEnumerable<BuildOutcome> LoadBuilds();
    }

    public class JenkinsRssParser : IRssParser
    {
        private readonly string[] _serverUris;
        private readonly BuildOutcomeFactory _buildOutcomeFactory;

        public JenkinsRssParser(string[] serverUris)
        {
            _serverUris = serverUris;
            _buildOutcomeFactory = new BuildOutcomeFactory();
        }

        public IEnumerable<BuildOutcome> LoadBuilds()
        {
            XNamespace rssNamespace = "http://www.w3.org/2005/Atom";
            var result = new List<BuildOutcome>();
            foreach (var serverUri in _serverUris)
            {
                result.AddRange(XDocument.Load(serverUri + "/rssLatest").Descendants(rssNamespace + "entry").Select(
                    entry => entry.Element(rssNamespace + "title").Value).Select(
                        jenkinsBuildTitle => _buildOutcomeFactory.CreateFrom(jenkinsBuildTitle)));
            }
            return result;
        }
    }
}