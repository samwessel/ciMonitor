using System.Collections.Generic;
using ciMonitor.ViewModels;
using NUnit.Framework;

namespace ciMonitor.Tests
{
    [TestFixture]
    public class BuildOutcomesViewModelOverallStatusTests
    {
        [Test]
        public void GivenNoOutcomesThenOverallStatusIsUnknown()
        {
            var status = new BuildOutcomesViewModel(new List<BuildOutcome>()).OverallStatus;
            Assert.That(status, Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void GivenOnlySuccessfulOutcomesThenOverallStatusIsSuccess()
        {
            var buildOutcomes = new List<BuildOutcome>() { 
                new BuildOutcome("", 0, Status.Success()), 
                new BuildOutcome("", 0, Status.Success()), 
                new BuildOutcome("", 0, Status.Success())
            };
            var status = new BuildOutcomesViewModel(buildOutcomes).OverallStatus;
            Assert.That(status, Is.EqualTo(Status.Success()));
        }

        [Test]
        public void GivenAnyBuildWithStatusFailThenOverallStatusIsFail()
        {
            var buildOutcomes = new List<BuildOutcome>() { 
                new BuildOutcome("", 0, Status.Success()), 
                new BuildOutcome("", 0, Status.Fail()),
                new BuildOutcome("", 0, Status.Success())
            }; 
            var status = new BuildOutcomesViewModel(buildOutcomes).OverallStatus;
            Assert.That(status, Is.EqualTo(Status.Fail()));
        }
    }
}