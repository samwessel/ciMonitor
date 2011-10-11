using System.Collections.Generic;

namespace ciMonitor.ViewModels
{
    public class BuildOutcomesViewModel
    {
        public BuildOutcomesViewModel(IEnumerable<BuildOutcome> buildOutcomes, Status overallStatus)
        {
            BuildOutcomes = buildOutcomes;
            OverallStatus = overallStatus;
        }

        public IEnumerable<BuildOutcome> BuildOutcomes { get; private set; }
        public Status OverallStatus { get; private set; }
    }
}