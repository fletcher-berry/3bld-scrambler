using System;
using System.Collections.Generic;
using BldScramblerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class LeafTests
    {
        [TestMethod]
        public void NumAlgs()
        {
            var leaf = new FinalLeaf(new List<int> { 5, 1, 2 }, Fraction.One, 1);
            Assert.AreEqual(5, leaf.GetNumAlgs());
        }
    }
}
