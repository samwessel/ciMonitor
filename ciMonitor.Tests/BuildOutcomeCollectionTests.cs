using ciMonitor.ViewModels;
using NUnit.Framework;

namespace ciMonitor.Tests
{
    [TestFixture]
    public class BuildOutcomeCollectionTests
    {
        [Test]
        public void GivenNoBuildsThenOverallStatusIsUnknown()
        {
            Assert.That(new BuildOutcomeCollection().OverallStatus(), Is.EqualTo(Status.Unknown()));    
        }

        [Test]
        public void GivenAtLeastOneFailingBuildThenOverallStatusIsFailed()
        {
            var buildOutcomeCollection = new BuildOutcomeCollection
                                             {
                                                 new BuildOutcome("successful build", 5, Status.Success()),
                                                 new BuildOutcome("failed build", 5, Status.Fail()),
                                                 new BuildOutcome("another successful build", 5, Status.Success()),
                                                 new BuildOutcome("unknown build", 5, Status.Unknown()),
                                                 new BuildOutcome("build in progress", 5, Status.Building())
                                             };

            Assert.That(buildOutcomeCollection.OverallStatus(), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void GivenAtLeastOneUnknownBuildAndNoFailingBuildsThenOverallStatusIsUnknown()
        {
            var buildOutcomeCollection = new BuildOutcomeCollection
                                             {
                                                 new BuildOutcome("successful build", 5, Status.Success()),
                                                 new BuildOutcome("unknown build", 5, Status.Unknown()),
                                                 new BuildOutcome("build in progress", 5, Status.Building()),
                                                 new BuildOutcome("another successful build", 5, Status.Success())
                                             };

            Assert.That(buildOutcomeCollection.OverallStatus(), Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void GivenAtLeastOneBuildInProgressAndNoFailingOrUnknownBuildsThenOverallStatusIsBuilding()
        {
            var buildOutcomeCollection = new BuildOutcomeCollection
                                             {
                                                 new BuildOutcome("successful build", 5, Status.Success()),
                                                 new BuildOutcome("build in progress", 5, Status.Building()),
                                                 new BuildOutcome("another successful build", 5, Status.Success())
                                             };

            Assert.That(buildOutcomeCollection.OverallStatus(), Is.EqualTo(Status.Building()));
        }

        [Test]
        public void GivenAllSuccessfulBuildsThenOverallStatusIsSuccess()
        {
            var buildOutcomeCollection = new BuildOutcomeCollection
                                             {
                                                 new BuildOutcome("successful build", 5, Status.Success()),
                                                 new BuildOutcome("another successful build", 5, Status.Success()),
                                                 new BuildOutcome("yet another successful build", 5, Status.Success())
                                             };

            Assert.That(buildOutcomeCollection.OverallStatus(), Is.EqualTo(Status.Success()));
        }
    }
}