using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BldScramblerLib;

namespace UnitTestProject1
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void FactorialTest()
        {
            Assert.AreEqual(120, MyMath.Factorial(5));
        }

        [TestMethod]
        public void ChooseTest()
        {
            Assert.AreEqual(15, MyMath.Choose(6, 2));
        }

        [TestMethod]
        public void ProbTest()
        {
            var prob = MyMath.Prob(6, 2, new Fraction(1, 3));
            Assert.AreEqual(80, prob.Numerator);
            Assert.AreEqual(243, prob.Denominator);
        }

        [TestMethod]
        public void ProbTest2()
        {
            var prob = MyMath.Prob(1, 1, new Fraction(1, 3));
            Assert.AreEqual(1, prob.Numerator);
            Assert.AreEqual(3, prob.Denominator);
        }

        [TestMethod]
        public void ProbTest3()
        {
            var prob = MyMath.Prob(0, 0, new Fraction(1, 3));
            Assert.AreEqual(1, prob.Numerator);
            Assert.AreEqual(1, prob.Denominator);
        }

    }
}
