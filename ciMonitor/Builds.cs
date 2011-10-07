using System.Collections.Generic;
using ciMonitor.ViewModels;

namespace ciMonitor
{
    public class Builds
    {
        private static ISoundAnnouncer _soundAnnouncer;
        private static Dictionary<string, BuildProperties> _builds;

        private static Status _overallStatus;

        private static Builds _instance;
        public static Builds Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Builds(new SoundAnnouncer(), new Dictionary<string, BuildProperties>(), Status.Unknown());
                return _instance;
            }
            set { _instance = value; }
        }

        public Builds(ISoundAnnouncer soundAnnouncer, Dictionary<string, BuildProperties> builds, Status initialStatus)
        {
            _soundAnnouncer = soundAnnouncer;
            _builds = builds;
            _overallStatus = initialStatus;
        }

        public static Status OverallStatus()
        {
            return _overallStatus;
        }

        public BuildOutcomesViewModel Update(BuildOutcomeCollection buildOutcomes)
        {
            foreach (var buildOutcome in buildOutcomes)
            {
                var isBuilding = buildOutcome.Status.Equals(Status.Building());
                var lastKnownStatus = (_builds.ContainsKey(buildOutcome.Name)) ? _builds[buildOutcome.Name].Status : Status.Unknown();
                var newBuildStatus = isBuilding ? lastKnownStatus : buildOutcome.Status;
                var buildProperties = new BuildProperties(buildOutcome.BuildNumber, newBuildStatus, isBuilding);

                if (!_builds.ContainsKey(buildOutcome.Name))
                {
                    _soundAnnouncer.NewBuild();
                }
                else
                {
                    if (isBuilding != _builds[buildOutcome.Name].IsBuilding)
                    {
                        if (isBuilding)
                        {
                            _soundAnnouncer.BuildStarted();
                        }
                        else
                        {
                            if (newBuildStatus.Equals(Status.Fail()))
                            {
                                if (lastKnownStatus.Equals(Status.Success()))
                                    _soundAnnouncer.FailedBuild();
                                else
                                    _soundAnnouncer.StillFailing();
                            }

                            if (newBuildStatus.Equals(Status.Success()))
                            {
                                if (lastKnownStatus.Equals(Status.Success()))
                                    _soundAnnouncer.SuccessfulBuild();
                                else
                                    _soundAnnouncer.FixedBuild();
                            }
                        }
                    }
                }

                if (!_builds.ContainsKey(buildOutcome.Name))
                    _builds.Add(buildOutcome.Name, buildProperties);
                else
                    _builds[buildOutcome.Name] = buildProperties;
            }

            if (!buildOutcomes.OverallStatus().Equals(Status.Building()))
                _overallStatus = buildOutcomes.OverallStatus();
            return new BuildOutcomesViewModel(buildOutcomes, _overallStatus);
        }
    }
}