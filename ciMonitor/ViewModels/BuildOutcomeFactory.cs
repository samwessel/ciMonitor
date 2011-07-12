using System;

namespace ciMonitor.ViewModels
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
            var strings = currentBuildStatus.TrimEnd(')').Split('(');
            if (strings.Length != 2)
                throw new FormatException("Build outcome could not be parsed from the following: " + currentBuildStatus);
            return new BuildOutcome(strings[0], _statusFactory.From(strings[1]));
        }
    }
}