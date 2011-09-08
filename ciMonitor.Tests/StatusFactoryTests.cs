using NUnit.Framework;

namespace ciMonitor.Tests
{
    [TestFixture]
    public class StatusFactoryTests
    {
        private StatusFactory _statusFactory;

        [SetUp]
        public void SetUp()
        {
            _statusFactory = new StatusFactory();            
        }

        [Test]
        public void ReturnsUnknownByDefault()
        {
            Assert.That(_statusFactory.From("sfglfglk"), Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ReturnsUnknown()
        {
            Assert.That(_statusFactory.From("unknown"), Is.EqualTo(Status.Unknown()));
        }

        [Test]
        public void ReturnsSuccess()
        {
            Assert.That(_statusFactory.From("stable"), Is.EqualTo(Status.Success()));
            Assert.That(_statusFactory.From("back to normal"), Is.EqualTo(Status.Success()));
        }

        [Test]
        public void ReturnsFail()
        {
            Assert.That(_statusFactory.From("broken since this build"), Is.EqualTo(Status.Fail()));
            Assert.That(_statusFactory.From("broken since build #9"), Is.EqualTo(Status.Fail()));
        }

        [Test]
        public void ReturnsBuilding()
        {
            Assert.That(_statusFactory.From("?"), Is.EqualTo(Status.Building()));
        }
    }
}