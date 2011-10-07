using System.Collections.Generic;

namespace ciMonitor.ViewModels
{
    public class BuildOutcomesViewModel
    {
        public BuildOutcomesViewModel(BuildOutcomeCollection buildOutcomes)
        {
            BuildOutcomes = buildOutcomes;
            OverallStatus = buildOutcomes.OverallStatus();
        }

        public IEnumerable<BuildOutcome> BuildOutcomes { get; private set; }
        public Status OverallStatus { get; private set; }
    }
}