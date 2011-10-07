using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ciMonitor.ViewModels;

namespace ciMonitor
{
    public interface IBuildOutcomeCollection : IEnumerable<BuildOutcome>
    {
        Status OverallStatus();
    }

    public class BuildOutcomeCollection : IBuildOutcomeCollection
    {
        private readonly ICollection<BuildOutcome> _buildOutcomes;

        public BuildOutcomeCollection()
            : this(new List<BuildOutcome>())
        {
        }

        public BuildOutcomeCollection(ICollection<BuildOutcome> buildOutcomes)
        {
            _buildOutcomes = buildOutcomes;
        }

        public Status OverallStatus()
        {
            if (_buildOutcomes.Count > 0 && _buildOutcomes.All(build => build.Status.Equals(Status.Success())))
                return Status.Success();
            if (_buildOutcomes.Any(build => build.Status.Equals(Status.Fail())))
                return Status.Fail();
            if (_buildOutcomes.Any(build => build.Status.Equals(Status.Unknown())))
                return Status.Unknown();
            if (_buildOutcomes.Any(build => build.Status.Equals(Status.Building())))
                return Status.Building();
            return Status.Unknown();
        }

        public void Add(BuildOutcome buildOutcome)
        {
            _buildOutcomes.Add(buildOutcome);
        }

        public IEnumerator<BuildOutcome> GetEnumerator()
        {
            return _buildOutcomes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}