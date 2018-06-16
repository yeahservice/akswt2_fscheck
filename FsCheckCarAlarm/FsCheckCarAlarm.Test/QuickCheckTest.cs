using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FsCheck;

namespace FsCheckCarAlarm.Test
{
    [TestClass]
    public class QuickCheckTest
    {
        [TestMethod]
        public void QuickCheck()
        {
            Configuration config = Configuration.VerboseThrowOnFailure;
            config.MaxNbOfTest = 1;

            for (int i = 1; i <= 5000; ++i)
            {
                config.StartSize = i;
                config.EndSize = i;

                new CarAlarmSpec()
                    .ToProperty()
                    .Check(config);

                Console.WriteLine();
            }
        }
    }
}
