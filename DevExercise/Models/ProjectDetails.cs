using System;

namespace DevExercise.Models
{
    public enum ReleaseType
    {
        Major,
        Minor,
        Patch
    }

    public class ProjectDetails
    {
        public string Version { get; set; }

        public Patch Patch { get; set; }
    }

    public class Patch
    {
        public string Name { get; set; }

        public string Directory { get; set; }

        public string Ordinal { get; set; }

        public IEnumerable<string> Scripts { get; set; }
    }
}
