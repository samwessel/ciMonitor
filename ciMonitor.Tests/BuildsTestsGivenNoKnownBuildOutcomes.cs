using System.Collections.Generic;
using System.Linq;
using ciMonitor.ViewModels;
using Moq;
using NUnit.Framework;

namespace ciMonitor.Tests
{
    [TestFixture]
    public class BuildsTestsGivenNoBuildsListed
    {
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcomesViewModel _result;
        private BuildOutcome _successfulBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _successfulBuildOutcome = new BuildOutcome("buildName", 11, Status.Success());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, new Dictionary<string, BuildProperties>(), Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() {_successfulBuildOutcome});
        }

        [Test]
        public void ThenNewBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.NewBuild());
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Success()));
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _successfulBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), true) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _successfulBuildOutcome = new BuildOutcome(buildname, 11, Status.Success());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Success());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _successfulBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenSuccessfulBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.SuccessfulBuild());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), true) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Success());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenNoSuccessfulBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.SuccessfulBuild(), Times.Never());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failedBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailedBuild()
        {
            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), true) } };
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Success());
            _failedBuildOutcome = new BuildOutcome(buildname, 11, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failedBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenFailedBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.FailedBuild());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failingBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailingBuild()
        {
            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _failingBuildOutcome = new BuildOutcome("buildName", 11, Status.Fail());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, new Dictionary<string, BuildProperties>(), Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failingBuildOutcome });
        }

        [Test]
        public void ThenNewBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.NewBuild());
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Fail()));
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _successfulBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), true) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _successfulBuildOutcome = new BuildOutcome(buildname, 11, Status.Success());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _successfulBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenFixedBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.FixedBuild());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), true) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenNoFailedBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.FailedBuild(), Times.Never());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failedBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailedBuild()
        {
            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), true) } };
            _failedBuildOutcome = new BuildOutcome(buildname, 11, Status.Fail());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failedBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenStillFailingBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.StillFailing());
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_failedBuildOutcome));
        }
    }

    public class BuildsTestsGivenNoBuildsListed_Unknown
    {
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcomesViewModel _result;
        private BuildOutcome _unknownBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailingBuild()
        {
            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _unknownBuildOutcome = new BuildOutcome("buildName", 11, Status.Unknown());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, new Dictionary<string, BuildProperties>(), Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _unknownBuildOutcome });
        }

        [Test]
        public void ThenNewBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.NewBuild());
        }

        [Test]
        public void ThenTheOverallStatusIsUnknown()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Unknown()));
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _successfulBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Unknown(), true) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _successfulBuildOutcome = new BuildOutcome(buildname, 11, Status.Success());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _successfulBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenFixedBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.FixedBuild());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Unknown(), true) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ThenNoFailedBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.FailedBuild(), Times.Never());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failedBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailedBuild()
        {
            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Unknown(), true) } };
            _failedBuildOutcome = new BuildOutcome(buildname, 11, Status.Fail());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failedBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenStillFailingBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.StillFailing());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _successfulBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithSuccessfulBuild()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), false) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _successfulBuildOutcome = new BuildOutcome(buildname, 11, Status.Success());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Success());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _successfulBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenNoSuccessfulBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.SuccessfulBuild(), Times.Never());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Success(), false) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Success());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsSuccess()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ThenBuildStartedEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.BuildStarted());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), false) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenBuildStartedEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.BuildStarted());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcomesViewModel _result;
        private BuildOutcome _failedBuildOutcome;

        [SetUp]
        public void WhenUpdatingWithFailedBuild()
        {
            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Fail(), false) } };
            _failedBuildOutcome = new BuildOutcome(buildname, 11, Status.Fail());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Fail());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _failedBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ThenNoStillFailingBuildEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.StillFailing(), Times.Never());
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
        private Mock<IAnnouncer> _mockSoundAnnouncer;
        private BuildOutcome _inProgressBuildOutcome;
        private BuildOutcomesViewModel _result;

        [SetUp]
        public void WhenUpdatingWithBuildStillInProgress()
        {
            const string buildname = "buildName";
            var builds = new Dictionary<string, BuildProperties> { { buildname, new BuildProperties(11, Status.Unknown(), false) } };

            _mockSoundAnnouncer = new Mock<IAnnouncer>();
            _inProgressBuildOutcome = new BuildOutcome(buildname, 11, Status.Building());
            Builds.Instance = new Builds(_mockSoundAnnouncer.Object, builds, Status.Unknown());
            _result = Builds.Instance.Update(new BuildOutcomeCollection() { _inProgressBuildOutcome });
        }

        [Test]
        public void ThenTheOverallStatusIsFail()
        {
            Assert.That(Builds.OverallStatus(), Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ThenBuildStartedEventIsGenerated()
        {
            _mockSoundAnnouncer.Verify(s => s.BuildStarted());
        }

        [Test]
        public void ThenTheCorrectBuildOutcomeIsReturned()
        {
            Assert.That(_result.BuildOutcomes.First(), Is.EqualTo(_inProgressBuildOutcome));
        }
    }
}