using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExercise.Services
{
    public static class VersionExtension
    {
        public static Version IncrementMajor(this Version version)
        {
            return new Version(version.Major + 1, 0, 0);
        }

        public static Version IncrementMinor(this Version version)
        {
            return new Version(version.Major, version.Minor + 1, 0);
        }

        public static Version IncrementPatch(this Version version)
        {
            return new Version(version.Major, version.Minor, version.Build + 1);
        }
    }
}
