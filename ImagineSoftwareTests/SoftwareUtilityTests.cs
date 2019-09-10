using ImagineSoftware.Models;
using ImagineSoftware.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImagineSoftwareTests
{
    [TestClass]
    public class SoftwareUtilityTests
    {
        private ISoftwareUtility softwareUtility;

        [TestInitialize]
        public void Setup()
        {
            softwareUtility = new SoftwareUtility();
        }

        [TestMethod]
        public void WhenOnlyMajorProvided_ReturnsWithMinorAndPatch()
        {
            var version = softwareUtility.GetInputVersion("2");

            Assert.AreEqual(new Version(2, 0, 0), version);
        }

        [TestMethod]
        public void WhenOnlyMajorMinorProvided_ReturnsWithPatch()
        {
            var version = softwareUtility.GetInputVersion("2.0");

            Assert.AreEqual(new Version(2, 0, 0), version);
        }

        [TestMethod]
        public void WhenInvalidInListOfAll_IncludedInListOfGreaterThanWithMessage()
        {
            var allVersions = new List<Software>
            {
                new Software { Name = "software1", Version = "abc" }
            };

            var softwares = softwareUtility.GetSoftwareGreaterThan(new Version("2.0.0"), allVersions);

            Assert.IsTrue(softwares.Any());
            Assert.AreEqual("software1", softwares.FirstOrDefault().Name);
            Assert.IsTrue(softwares.FirstOrDefault().Version.Contains("Invalid"));
        }

        [TestMethod]
        public void WhenEqual_NotIncludedInList()
        {
            var allVersions = new List<Software>
            {
                new Software { Name = "software2", Version = "2.0.0" }
            };

            var softwares = softwareUtility.GetSoftwareGreaterThan(new Version("2.0.0"), allVersions);

            Assert.IsFalse(softwares.Any());
        }

        [TestMethod]
        public void WhenValidInput_ReturnsExpectedSoftwared()
        {
            var allVersions = new List<Software>
            {
                new Software { Name = "software1", Version = "1.2.3" },
                new Software { Name = "software2", Version = "2.0.0" },
                new Software { Name = "software3", Version = "2.0.1" },
                new Software { Name = "software4", Version = "3.2.1" }
            };

            var softwares = softwareUtility.GetSoftwareGreaterThan(new Version("2.0.0"), allVersions).ToList();

            Assert.AreEqual(2, softwares.Count);
            Assert.IsTrue(softwares.Any(x => x.Name == "software3"));
            Assert.IsTrue(softwares.Any(x => x.Name == "software4"));
        }
    }
}
