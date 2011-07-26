using System.Collections.Generic;
using System.Linq;

namespace ciMonitor.ViewModels
{
    public class BuildOutcomesViewModel
    {
        public BuildOutcomesViewModel(IEnumerable<BuildOutcome> buildOutcomes)
        {
            BuildOutcomes = buildOutcomes;
            OverallStatus = DetermineOverallStatusFrom(buildOutcomes);
        }

        private static Status DetermineOverallStatusFrom(IEnumerable<BuildOutcome> buildOutcomes)
        {
            if (buildOutcomes.Any(outcome => outcome.Status.Equals(Status.Fail())))
                return Status.Fail();
            if (buildOutcomes.Count() > 0 && buildOutcomes.All(outcome => outcome.Status.Equals(Status.Success())))
                return Status.Success();
            return Status.Unknown();
        }

        public IEnumerable<BuildOutcome> BuildOutcomes { get; private set; }
        public Status OverallStatus { get; private set; }
    }
}