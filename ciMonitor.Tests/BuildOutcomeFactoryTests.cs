using System;
using Moq;
using ciMonitor.ViewModels;
using NUnit.Framework;

namespace ciMonitor.Tests
{
    [TestFixture]
    public class BuildOutcomeFactoryTestsGivenValidJenkinsBuildTitle
    {
        private BuildOutcome _result;
        private string _name;
        private string _status;
        private Mock<IStatusFactory> _mockStatusFactory;
        private Status _parsedStatus;

        [SetUp]
        public void WhenParsingValidJenkinsBuildTitle()
        {
            _name = "asdfjasldfkasd";
            _status = "bndcvndcvm,ndsal";
            var currentBuildStatus = _name + "(" + _status + ")";

            _mockStatusFactory = new Mock<IStatusFactory>();
            _parsedStatus = Status.Success();
            _mockStatusFactory.Setup(f => f.From(It.IsAny<string>())).Returns(_parsedStatus);

            var buildOutcomeFactory = new BuildOutcomeFactory(_mockStatusFactory.Object);
            _result = buildOutcomeFactory.CreateFrom(currentBuildStatus);
        }

        [Test]
        public void ThenItSetsName()
        {
            Assert.That(_result.Name, Is.EqualTo(_name));
        }

        [Test]
        public void ThenItCallsToStatusFactory()
        {
            _mockStatusFactory.Verify(factory => factory.From(_status));
        }

        [Test]
        public void ThenItSetsStatus()
        {
            Assert.That(_result.Status, Is.SameAs(_parsedStatus));
        }
    }

    [TestFixture]
    public class BuildOutcomesFactoryTestsGivenInvalidJenkinsBuildTitle
    {
        [Test]
        [ExpectedException(typeof(FormatException))]
        public void WhenParsingInvalidJenkinsBuildTitleThenItThrowsFormatException()
        {
            new BuildOutcomeFactory().CreateFrom("<something>sadgfj;fr;jdfklsdfkl jklsdfsdjklaf:");
        }
    }
}
