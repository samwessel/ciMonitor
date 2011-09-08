using System;
using ciMonitor.ViewModels;

namespace ciMonitor
{
    public class BuildOutcomeFactory
    {
        private readonly IStatusFactory _statusFactory;

        public BuildOutcomeFactory(IStatusFactory statusFactory)
        {
            _statusFactory = statusFactory;                       
        }

        public BuildOutcomeFactory()
            : this(new StatusFactory())
        {
        }

        public BuildOutcome CreateFrom(string currentBuildStatus)
        {
            var splitByOpeningParenthesis = currentBuildStatus.Split('(');
            var splitByHash = splitByOpeningParenthesis[0].Split('#');

            if (splitByOpeningParenthesis.Length != 2 || splitByHash.Length != 2)
                throw new FormatException("Build outcome could not be parsed from the following: " + currentBuildStatus);

            var buildName = splitByHash[0].Trim();
            var buildNumber = int.Parse(splitByHash[1].Trim());
            var status = _statusFactory.From(splitByOpeningParenthesis[1].TrimEnd(')'));

            return new BuildOutcome(buildName, buildNumber, status);
        }
    }
}