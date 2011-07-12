using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ciMonitor.ViewModels;

namespace ciMonitor
{
    public class JenkinsRssParser
    {
        private readonly BuildOutcomeFactory _buildOutcomeFactory;

        public JenkinsRssParser()
        {
            _buildOutcomeFactory = new BuildOutcomeFactory();
        }

        public IEnumerable<BuildOutcome> ParseJenkinsBuilds(string serverUri)
        {
            XNamespace rssNamespace = "http://www.w3.org/2005/Atom";
            var feedXml = XDocument.Load(serverUri + "/rssLatest");

            var jenkinsBuildTitles = feedXml.Descendants(rssNamespace + "entry").Select(
                entry => entry.Element(rssNamespace + "title").Value);

            return jenkinsBuildTitles.Select(jenkinsBuildTitle => _buildOutcomeFactory.CreateFrom(jenkinsBuildTitle)).ToList();
        }
    }
}