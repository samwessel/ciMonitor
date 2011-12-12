using System.Collections.Generic;
using ciMonitor.ViewModels;

namespace ciMonitor
{
    public class Builds
    {
        private static Dictionary<string, BuildProperties> _builds;
        private static Status _overallStatus;
        private static Builds _instance;
        private IList<string> _transitions;

        public static Builds Instance
        {
            get { return _instance ?? (_instance = new Builds(new Dictionary<string, BuildProperties>(), Status.Unknown())); }
            set { _instance = value; }
        }

        public Builds(Dictionary<string, BuildProperties> builds, Status initialStatus)
        {
            _builds = builds;
            _overallStatus = initialStatus;
        }

        public BuildOutcomesViewModel Update(IBuildOutcomeCollection buildOutcomes)
        {
            _transitions = new List<string>();
            foreach (var buildOutcome in buildOutcomes)
            {
                if (!_builds.ContainsKey(buildOutcome.Name))
                {
                    _builds.Add(buildOutcome.Name, new BuildProperties(buildOutcome.BuildNumber, Status.Unknown(), false));
                }

                var isBuilding = buildOutcome.Status.Equals(Status.Building());
                var previousBuildProperties = _builds[buildOutcome.Name];

                if (isBuilding != previousBuildProperties.IsBuilding)
                {
                    if (isBuilding)
                    {
                        _transitions.Add(Transitions.BuildStarted);
                    }
                    else
                    {
                        if (buildOutcome.Status.Equals(Status.Fail()))
                        {
                            if (previousBuildProperties.Status.Equals(Status.Success()))
                                _transitions.Add(Transitions.FailedBuild);
                            else
                                _transitions.Add(Transitions.RepeatedlyFailingBuild);
                        }

                        if (buildOutcome.Status.Equals(Status.Success()))
                        {
                            if (previousBuildProperties.Status.Equals(Status.Success()))
                                _transitions.Add(Transitions.SuccessfulBuild);
                            else
                                _transitions.Add(Transitions.FixedBuild);
                        }
                    }
                }

                var newBuildStatus = isBuilding ? previousBuildProperties.Status : buildOutcome.Status;
                _builds[buildOutcome.Name] = new BuildProperties(buildOutcome.BuildNumber, newBuildStatus, isBuilding);
            }

            if (!buildOutcomes.OverallStatus().Equals(Status.Building()))
                _overallStatus = buildOutcomes.OverallStatus();
            return new BuildOutcomesViewModel(buildOutcomes, _overallStatus, _transitions);
        }
    }
}