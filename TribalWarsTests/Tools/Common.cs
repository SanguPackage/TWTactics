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
