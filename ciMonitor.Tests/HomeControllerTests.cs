using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ciMonitor.Controllers;
using ciMonitor.ViewModels;
using Moq;
using NUnit.Framework;

namespace ciMonitor.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IRssParser> _mockJenkinsRssParser;
        private ViewResult _result;
        private List<BuildOutcome> _buildOutcomes;

        [SetUp]
        public void WhenCallingIndex()
        {
            _buildOutcomes = new List<BuildOutcome>();
            _mockJenkinsRssParser = new Mock<IRssParser>();
            _mockJenkinsRssParser.Setup(parser => parser.LoadBuilds()).Returns(_buildOutcomes);

            _result = new HomeController(_mockJenkinsRssParser.Object).Index();
        }

        [Test]
        public void ItCallsToTheJenkinsParser()
        {
            _mockJenkinsRssParser.Verify(parser => parser.LoadBuilds());
        }

        [Test]
        public void TheViewModelIsTheCorrectType()
        {
            Assert.That(_result.ViewData.Model, Is.TypeOf(typeof(BuildOutcomesViewModel)));
        }

        [Test]
        public void ItPassesTheBuildOutcomesToTheView()
        {
            Assert.That(((BuildOutcomesViewModel)_result.ViewData.Model).BuildOutcomes, Is.SameAs(_buildOutcomes));
        }
    }
}
