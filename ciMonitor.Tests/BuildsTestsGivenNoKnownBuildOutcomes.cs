using System.Collections.Generic;
using System.Linq;
using ciMonitor.ViewModels;
using NUnit.Framework;

namespace ciMonitor.Tests
{
    [TestFixture]
    public class BuildsTestsGivenNoBuildsListed
    {
        private BuildOutcomesViewModel _result;
        private BuildOutcome _successfulBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            _successfulBuildOutcome = new BuildOutcome("buildName", 11, Status.Success());
            Builds.Instance = new Builds(new Dictionary<string, BuildProperties>(), Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() {_successfulBuildOutcome});
        }

        [Test]
        public void ThenTheResultContainsNewBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.NewBuild }));
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_successfulBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenSuccessfulBuildInProgressSucceeds
    {
        private BuildOutcome _successfulBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), true) } };

            _successfulBuildOutcome = new BuildOutcome(buildname, 11, Status.Success());
            Builds.Instance = new Builds(builds, Status.Success());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _successfulBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenTheResultContainsSuccessfulBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.SuccessfulBuild }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_successfulBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenSuccessfulBuildInProgressIsStillBuilding
    {
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), true) } };

            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(builds, Status.Success());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenTheResultContainsNoTransitions()
        {
            Assert.That(_result.Transitions.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_inProgressBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenSuccessfulBuildInProgressFails
    {
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failedBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailedBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), true) } };
            Builds.Instance = new Builds(builds, Status.Success());
            _failedBuildOutcome = new BuildOutcome(buildname, 11, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failedBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenTheResultContainsFailedBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.FailedBuild }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_failedBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenNoBuildsListed_Fail
    {
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failingBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailingBuild()
        {
            _failingBuildOutcome = new BuildOutcome("buildName", 11, Status.Fail());
            Builds.Instance = new Builds(new Dictionary<string, BuildProperties>(), Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failingBuildOutcome });
        }

        [Test]
        public void ThenTheResultContainsNewBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.NewBuild }));
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_failingBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenFailedBuildInProgressSucceeds
    {
        private BuildOutcome _successfulBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), true) } };

            _successfulBuildOutcome = new BuildOutcome(buildname, 11, Status.Success());
            Builds.Instance = new Builds(builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _successfulBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenTheResultContainsFixedBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.FixedBuild }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_successfulBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenFailedBuildInProgressIsStillBuilding
    {
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), true) } };

            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenTheResultContainsNoTransitions()
        {
            Assert.That(_result.Transitions.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            var buildOutcome = _result.BuildOutcomes.First();
            Assert.That(buildOutcome, Is.EqualTo(_inProgressBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenFailedBuildInProgressFails
    {
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failedBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailedBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), true) } };
            _failedBuildOutcome = new BuildOutcome(buildname, 11, Status.Fail());
            Builds.Instance = new Builds(builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failedBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenTheResultContainsRepeatedlyFailingBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.RepeatedlyFailingBuild }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_failedBuildOutcome));
        }
    }

    public class BuildsTestsGivenNoBuildsListed_Unknown
    {
        private BuildOutcomesViewModel _result;
        private BuildOutcome _unknownBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailingBuild()
        {
            _unknownBuildOutcome = new BuildOutcome("buildName", 11, Status.Unknown());
            Builds.Instance = new Builds(new Dictionary<string, BuildProperties>(), Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _unknownBuildOutcome });
        }

        [Test]
        public void ThenTheResultContainsNewBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.NewBuild }));
        }

        [Test]
        public void ThenTheOverallStatusIsUnknown()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_unknownBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenUnknownBuildInProgressSucceeds
    {
        private BuildOutcome _successfulBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Unknown(), true) } };

            _successfulBuildOutcome = new BuildOutcome(buildname, 11, Status.Success());
            Builds.Instance = new Builds(builds, Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _successfulBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenTheResultContainsFixedBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.FixedBuild }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_successfulBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenUnknownBuildInProgressIsStillBuilding
    {
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Unknown(), true) } };

            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(builds, Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ThenTheResultContainsNoTransitions()
        {
            Assert.That(_result.Transitions.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_inProgressBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenUnknownBuildInProgressFails
    {
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failedBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailedBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Unknown(), true) } };
            _failedBuildOutcome = new BuildOutcome(buildname, 11, Status.Fail());
            Builds.Instance = new Builds(builds, Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failedBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenTheResultContainsRepeatedlyFailingBuildTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.RepeatedlyFailingBuild }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_failedBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenSuccessfulBuildStillSuccessful
    {
        private BuildOutcome _successfulBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), false) } };

            _successfulBuildOutcome = new BuildOutcome(buildname, 11, Status.Success());
            Builds.Instance = new Builds(builds, Status.Success());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _successfulBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenTheResultContainsNoTransitions()
        {
            Assert.That(_result.Transitions.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_successfulBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenSuccessfulBuildStartsBuilding
    {
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), false) } };

            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(builds, Status.Success());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenTheResultContainsBuildStartedTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.BuildStarted }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_inProgressBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenFailedBuildStartsBuilding
    {
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), false) } };

            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenTheResultContainsBuildStartedTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.BuildStarted }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_inProgressBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenFailedBuildIsStillFailing
    {
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failedBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailedBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), false) } };
            _failedBuildOutcome = new BuildOutcome(buildname, 11, Status.Fail());
            Builds.Instance = new Builds(builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failedBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenTheResultContainsNoTransitions()
        {
            Assert.That(_result.Transitions.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_failedBuildOutcome));
        }
    }

    [TestFixture]
    public class BuildsTestsGivenUnknownBuildStartsBuilding
    {
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Unknown(), false) } };

            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(builds, Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(_result.OverallStatus, Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ThenTheResultContainsBuildStartedTransition()
        {
            Assert.That(_result.Transitions, Is.EqualTo(new List<string>() { Transitions.BuildStarted }));
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_inProgressBuildOutcome));
        }
    }
}