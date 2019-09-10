using ImagineSoftware.Models;
using System;
using System.Collections.Generic;

namespace ImagineSoftware.Utilities
{
    public interface ISoftwareUtility
    {
        Version GetInputVersion(string Version);

        IEnumerable<Software> GetSoftwareGreaterThan(Version inputVersion, IEnumerable<Software> softwares);
    }

    public class SoftwareUtility : ISoftwareUtility
    {
        public Version GetInputVersion(string input)
        {
            if(int.TryParse(input, out int majorVersion))
            {
                return new Version(majorVersion, 0, 0);
            }

            var pieces = input.Split('.');
            if(pieces.Length == 2)
            {
                return new Version($"{pieces[0]}.{pieces[1]}.0");
            }

            return Version.Parse(input);
        }

        public IEnumerable<Software> GetSoftwareGreaterThan(Version inputVersion, IEnumerable<Software> softwares)
        {
            var greaterThanSoftwares = new List<Software>();

            foreach(var software in softwares)
            {
                if (!Version.TryParse(software.Version, out Version storedVersion))
                {
                    software.Version += " (Invalid)";
                    greaterThanSoftwares.Add(software);
                }

                if(storedVersion > inputVersion)
                {
                    greaterThanSoftwares.Add(software);
                }
            }

            return greaterThanSoftwares;
        }
    }
}
