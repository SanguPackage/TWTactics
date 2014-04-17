using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TribalWarsTests
{
    [TestClass]
    public class UnitTest1
    {
        private const string AvailableWorlds = "http://www.tribalwars.nl/backend/get_servers.php";

        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
