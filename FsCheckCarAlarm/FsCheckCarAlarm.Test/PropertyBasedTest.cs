using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FsCheck;

namespace FsCheckCarAlarm.Test
{
    [TestClass]
    public class PropertyBasedTest
    {
        [TestMethod]
        public void TestProperties()
        {
            new CarAlarmSpec()
                .ToProperty()
                .QuickCheck();
        }
    }
}
