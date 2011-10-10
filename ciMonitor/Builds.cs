using System.Collections.Generic;
using ciMonitor.ViewModels;

namespace ciMonitor
{
    public class Builds
    {
        private static IAnnouncer _announcer;
        private static Dictionary<string, BuildProperties> _builds;
        private static Status _overallStatus;
        private static bool _initialised;

        private static Builds _instance;
        public static Builds Instance
        {
            get { return _instance ?? (_instance = new Builds(new Announcer(), new Dictionary<string, BuildProperties>(), Status.Unknown())); }
            set { _instance = value; _initialised = true; }
        }

        public Builds(IAnnouncer announcer, Dictionary<string, BuildProperties> builds, Status initialStatus)
        {
            _announcer = announcer;
            _builds = builds;
            _overallStatus = initialStatus;
        }

        public static Status OverallStatus()
        {
            return _overallStatus;
        }

        public BuildOutcomesViewModel Initialise(BuildOutcomeCollection buildOutcomes)
        {
            _builds = new Dictionary<string, BuildProperties>();
            foreach (var buildOutcome in buildOutcomes)
            {
                var isBuilding = buildOutcome.Status.Equals(Status.Building());
                var newBuildStatus = isBuilding ? Status.Unknown() : buildOutcome.Status;
                _builds.Add(buildOutcome.Name, new BuildProperties(buildOutcome.BuildNumber, newBuildStatus, isBuilding));
            }
            _initialised = true;

            if (!buildOutcomes.OverallStatus().Equals(Status.Building()))
                _overallStatus = buildOutcomes.OverallStatus();
            return new BuildOutcomesViewModel(buildOutcomes, _overallStatus);
        }

        public BuildOutcomesViewModel Update(BuildOutcomeCollection buildOutcomes)
        {
            if (!_initialised)
                return Initialise(buildOutcomes);

            foreach (var buildOutcome in buildOutcomes)
            {
                Update(buildOutcome);
            }

            if (!buildOutcomes.OverallStatus().Equals(Status.Building()))
                _overallStatus = buildOutcomes.OverallStatus();
            return new BuildOutcomesViewModel(buildOutcomes, _overallStatus);
        }

        private static void Update(BuildOutcome buildOutcome)
        {
            if (!_builds.ContainsKey(buildOutcome.Name))
            {
                _builds.Add(buildOutcome.Name, new BuildProperties(buildOutcome.BuildNumber, Status.Unknown(), false));
                _announcer.NewBuild();
            }

            var isBuilding = buildOutcome.Status.Equals(Status.Building());
            var previousBuildProperties = _builds[buildOutcome.Name];

            if (isBuilding != previousBuildProperties.IsBuilding)
            {
                if (isBuilding)
                {
                    _announcer.BuildStarted();
                }
                else
                {
                    if (buildOutcome.Status.Equals(Status.Fail()))
                    {
                        if (previousBuildProperties.Status.Equals(Status.Success()))
                            _announcer.FailedBuild();
                        else
                            _announcer.StillFailing();
                    }

                    if (buildOutcome.Status.Equals(Status.Success()))
                    {
                        if (previousBuildProperties.Status.Equals(Status.Success()))
                            _announcer.SuccessfulBuild();
                        else
                            _announcer.FixedBuild();
                    }
                }
            }

            var newBuildStatus = isBuilding ? previousBuildProperties.Status : buildOutcome.Status;
            _builds[buildOutcome.Name] = new BuildProperties(buildOutcome.BuildNumber, newBuildStatus, isBuilding);
        }
    }
}