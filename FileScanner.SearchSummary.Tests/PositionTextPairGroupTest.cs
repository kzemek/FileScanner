using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class PositionTextPairGroupTest
    {
        [TestMethod]
        public void IsWithinRange()
        {
            int contextSizeChars = 5;
            string testStr = "alama";
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, testStr),
                new PositionTextPair(contextSizeChars, testStr),
                new PositionTextPair(contextSizeChars * 2 + testStr.Length - 1, testStr),
                new PositionTextPair(contextSizeChars * 2 + testStr.Length, testStr),
                new PositionTextPair(contextSizeChars * 2 + testStr.Length + 1, testStr)
            };
            PositionTextPairGroup group = new PositionTextPairGroup(pairs[0], contextSizeChars);

            Assert.IsTrue(group.IsWithinRange(pairs[0], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[1], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[2], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[3], contextSizeChars));
            Assert.IsFalse(group.IsWithinRange(pairs[4], contextSizeChars));

            group = new PositionTextPairGroup(pairs[4], contextSizeChars);

            Assert.IsFalse(group.IsWithinRange(pairs[0], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[1], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[2], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[3], contextSizeChars));
            Assert.IsTrue(group.IsWithinRange(pairs[4], contextSizeChars));
        }

    }
}
