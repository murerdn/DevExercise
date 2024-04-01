using Microsoft.Extensions.Logging;
using Moq;
using DevExercise.Services;
using DevExercise.Models;
using FluentAssertions;
using DevExercise.Util;

namespace DevExerciseTests
{
    public class ProjectDetailsServiceTests
    {
        private ProjectDetailsService _projectDetailsService;
        private Mock<IJsonHandler> _jsonHandler;
        private ProjectDetails _projectDetailsTestData;

        [SetUp]
        public void Setup()
        {
            var _logger = new Mock<ILogger<ProjectDetailsService>>();
            _jsonHandler = new Mock<IJsonHandler>();
            _projectDetailsTestData = new ProjectDetails()
            {
                Version = "1.25.5",
                Patch = new Patch()
                {
                    Name = "Patch022024",
                    Directory = "Patch022024",
                    Ordinal = "1",
                    Scripts = new[] { "script1.sql", "script2.sql" }
                }
            };

            _projectDetailsService = new ProjectDetailsService(_logger.Object, _jsonHandler.Object);            
        }

        [Test]
        public void UpdateProjectDetailsVersion_WithReleaseTypeMinor_MinorVersionIncreases()
        {
            _jsonHandler.Setup(x => x.DeserializeObject<ProjectDetails>(It.IsAny<string>()))
                .Returns(_projectDetailsTestData);

            _projectDetailsService.UpdateProjectDetailsVersion(ReleaseType.Minor, It.IsAny<string>());

            _projectDetailsTestData.Version.Should().Be("1.26.0");
        }

        [Test]
        public void UpdateProjectDetailsVersion_WithReleaseTypePatch_PatchVersionIncreases()
        {
            _jsonHandler.Setup(x => x.DeserializeObject<ProjectDetails>(It.IsAny<string>()))
                .Returns(_projectDetailsTestData);

            _projectDetailsService.UpdateProjectDetailsVersion(ReleaseType.Patch, It.IsAny<string>());

            _projectDetailsTestData.Version.Should().Be("1.25.6");
        }

        [Test]
        public void UpdateProjectDetailsVersion_WithInvalidVersion_ThrowsException()
        {
            _projectDetailsTestData.Version = "abc";

            _jsonHandler.Setup(x => x.DeserializeObject<ProjectDetails>(It.IsAny<string>()))
                .Returns(_projectDetailsTestData);

            _projectDetailsService.Invoking(x => x.UpdateProjectDetailsVersion(ReleaseType.Patch, 
                It.IsAny<string>())).Should().Throw<Exception>();
        }
    }
}