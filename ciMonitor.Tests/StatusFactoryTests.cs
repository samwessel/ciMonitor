using NUnit.Framework;

namespace ciMonitor.Tests
{
    [TestFixture]
    public class StatusFactoryTests
    {
        private JenkinsStatusFactory _jenkinsStatusFactory;

        [SetUp]
        public void SetUp()
        {
            _jenkinsStatusFactory = new JenkinsStatusFactory();            
        }

        [Test]
        public void ReturnsUnknownByDefault()
        {
            Assert.That(_jenkinsStatusFactory.From("sfglfglk"), Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ReturnsUnknown()
        {
            Assert.That(_jenkinsStatusFactory.From("unknown"), Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ReturnsSuccess()
        {
            Assert.That(_jenkinsStatusFactory.From("stable"), Is.EqualTo(Status.Success()));
            Assert.That(_jenkinsStatusFactory.From("back to normal"), Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ReturnsFail()
        {
            Assert.That(_jenkinsStatusFactory.From("broken since this build"), Is.EqualTo(Status.Fail()));
            Assert.That(_jenkinsStatusFactory.From("broken since build #9"), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void AbortedBuildsInterpretedAsFailures()
        {
            Assert.That(_jenkinsStatusFactory.From("aborted"), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ReturnsBuilding()
        {
            Assert.That(_jenkinsStatusFactory.From("?"), Is.EqualTo(Status.Building()));
        }
    }
}