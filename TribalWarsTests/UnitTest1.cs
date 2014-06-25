using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TribalWarsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsInKingdom()
        {
            IsInKingdom(1, new Point(1, 1));
            IsInKingdom(1, new Point(560, 1));

            IsInKingdom(50, new Point(1, 560));
        }

        private static void IsInKingdom(int expectedKingdom, Point gameLocation)
        {
            int kingdom = (int)(Math.Floor((double)gameLocation.X / 100) + 10 * Math.Floor((double)gameLocation.Y / 100));
            Assert.AreEqual(expectedKingdom, kingdom);
        }
    }
}
