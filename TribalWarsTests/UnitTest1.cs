using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TribalWarsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Display()
        {
            DateTime standard = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Romance Standard Time");
            DateTime summer = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Romance Summer Time");

            Console.WriteLine("DaylightName" + TimeZone.CurrentTimeZone.DaylightName); // Romance Summer Time
            Console.WriteLine("StandardName" + TimeZone.CurrentTimeZone.StandardName); // Romance Standard Time
            //Console.WriteLine("StandardName" + TimeZone.CurrentTimeZone.); // 
    
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-BE");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-BE");
        }
    }
}
