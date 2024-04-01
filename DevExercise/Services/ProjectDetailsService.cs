using DevExercise.Models;
using DevExercise.Util;
using Microsoft.Extensions.Logging;

namespace DevExercise.Services
{
    public class ProjectDetailsService : IProjectDetailsService
    {
        private ProjectDetails _projectDetails { get; set; }
        private readonly ILogger _logger;
        private readonly IJsonHandler _jsonHandler;

        public ProjectDetailsService(ILogger logger, IJsonHandler jsonHandler)
        {
            _logger = logger;
            _jsonHandler = jsonHandler;
        }

        private void GetProjectDetails(string filePath)
        {
            _projectDetails = _jsonHandler.DeserializeObject<ProjectDetails>(filePath);
        }

        private void UpdateProjectDetails(string filePath)
        {
            _jsonHandler.SerializeObject(filePath, _projectDetails);
        }

        public void UpdateProjectDetailsVersion(ReleaseType releaseType, string filePath)
        {
            GetProjectDetails(filePath);

            try
            {
                var version = new Version(_projectDetails.Version);

                if (releaseType == ReleaseType.Minor)
                {
                    version = version.IncrementMinor();
                    _projectDetails.Version = version.ToString();                    
                }
                else if (releaseType == ReleaseType.Patch)
                {
                    version = version.IncrementPatch();
                    _projectDetails.Version = version.ToString();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating version number.");
                throw;
            }

            UpdateProjectDetails(filePath);
        }
    }
}
