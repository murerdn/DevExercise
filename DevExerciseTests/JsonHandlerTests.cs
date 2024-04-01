using DevExercise.Models;
using DevExercise.Services;
using DevExercise.Util;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace JsonHandlerTests
{
    public class JsonHandlerTests
    {
        private JsonHandler _jsonHandler;

        [SetUp]
        public void Setup()
        {
            var _logger = new Mock<ILogger<JsonHandler>>();

            _jsonHandler = new JsonHandler(_logger.Object);
        }

        [Test]
        public void DeserializeObject_WithNonExistingJsonFile_ThrowsFileNotFoundException()
        {
            _jsonHandler.Invoking(x => x.DeserializeObject<ProjectDetails>(
                Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData/abc.json"))).Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void DeserializeObject_WithFaultyJsonFile_ThrowsJsonReaderException()
        {
            _jsonHandler.Invoking(x => x.DeserializeObject<ProjectDetails>(
                Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData/ProjectDetailsFaulty.json")))
                .Should().Throw<JsonReaderException>();
        }

        [Test]
        public void DeserializeObject_WithValidJsonFile_ReturnsExpectedProjectDetails()
        {
            var expected = new ProjectDetails()
            {
                Version = "1.6.23",
                Patch = new Patch()
                {
                    Name = "Patch022024",
                    Directory = "Patch022024",
                    Ordinal = "1",
                    Scripts = new[] { "script1.sql", "script2.sql" }
                }
            };

            var actual = _jsonHandler.DeserializeObject<ProjectDetails>(
                Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData/ProjectDetails.json"));

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
