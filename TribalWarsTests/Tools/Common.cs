using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TribalWarsTests.Tools
{
    [TestClass]
    public class Common
    {
        [TestMethod]
        public void GetPrettyNumber_Works()
        {
            IsPrettyNumberFormattedCorrectly(100, "100");
            IsPrettyNumberFormattedCorrectly(2500, "2 500"); // The spaces are ascii 160!!
            IsPrettyNumberFormattedCorrectly(12345, "12 345");
            IsPrettyNumberFormattedCorrectly(123456, "123k");
            IsPrettyNumberFormattedCorrectly(1234567, "1,2M");
            IsPrettyNumberFormattedCorrectly(12345678, "12,3M");
            IsPrettyNumberFormattedCorrectly(12000000, "12M");
            IsPrettyNumberFormattedCorrectly(123456789, "123M");
            IsPrettyNumberFormattedCorrectly(1234567890, "1 234M");
        }

        private static void IsPrettyNumberFormattedCorrectly(int input, string expected)
        {
            string actual = TribalWars.Tools.Common.GetPrettyNumber(input);
            // If its looks like they are the same but the assertion fails:
            // ToString(#,0) apparently places a non-breaking space (ascii 160) and not space (ascii 32)
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsInKingdom_SomeRandomTests()
        {
            IsInKingdom(00, new Point(1, 1));
            IsInKingdom(01, new Point(123, 99));
            IsInKingdom(05, new Point(560, 1));

            IsInKingdom(50, new Point(1, 560));
            IsInKingdom(59, new Point(950, 560));

            IsInKingdom(99, new Point(950, 951));
        }

        private static void IsInKingdom(int expectedKingdom, Point gameLocation)
        {
            int kingdom = TribalWars.Tools.Common.Kingdom(gameLocation);
            Assert.AreEqual(expectedKingdom, kingdom);
        }
    }
}
