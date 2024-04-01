using DevExercise.Models;
using System;

namespace DevExercise.Services
{
    interface IProjectDetailsService
    {
        void UpdateProjectDetailsVersion(ReleaseType releaseType, string filePath);
    }
}
