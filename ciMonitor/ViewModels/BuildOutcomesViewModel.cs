using System.Collections.Generic;

namespace ciMonitor.ViewModels
{
    public class BuildOutcomesViewModel
    {
        public BuildOutcomesViewModel(IEnumerable<BuildOutcome> buildOutcomes, Status overallStatus, IEnumerable<string> transitions)
        {
            BuildOutcomes = buildOutcomes;
            OverallStatus = overallStatus;
            Transitions = transitions;
        }

        public IEnumerable<BuildOutcome> BuildOutcomes { get; private set; }
        public IEnumerable<string> Transitions { get; private set; }
        public Status OverallStatus { get; private set; }
    }
}