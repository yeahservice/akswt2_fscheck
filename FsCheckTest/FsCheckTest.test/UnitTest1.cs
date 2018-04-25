using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FsCheck;

namespace FsCheckTest.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testCalculator()
        {
            Calculator calc = new Calculator();
            Assert.IsTrue(calc.add(3, 3) == 6);
        }

        [TestMethod]
        public void testCalculatorFsCheck()
        {
            Prop.ForAll<int, int>((a, b) =>
            {
                Calculator calc = new Calculator();
                return (calc.add(a, b) == (a + b))
                .When(a < 10);
            }).QuickCheckThrowOnFailure();
        }
    }
}
