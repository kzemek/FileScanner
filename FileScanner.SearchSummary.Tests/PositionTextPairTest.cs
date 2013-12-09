using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class PositionTextPairTest
    {
        [TestMethod]
        public void Overlaps()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "ala"),
                new PositionTextPair(1, "lama"),
                new PositionTextPair(3, "ma"),
                new PositionTextPair(4, "akota")
            };

            Assert.IsTrue(pairs[0].OverlapsOrIsAdjacentTo(pairs[1]));
            Assert.IsTrue(pairs[1].OverlapsOrIsAdjacentTo(pairs[0]));
            Assert.IsTrue(pairs[0].OverlapsOrIsAdjacentTo(pairs[2]));
            Assert.IsTrue(pairs[2].OverlapsOrIsAdjacentTo(pairs[0]));
            Assert.IsFalse(pairs[0].OverlapsOrIsAdjacentTo(pairs[3]));
            Assert.IsFalse(pairs[3].OverlapsOrIsAdjacentTo(pairs[0]));
        }

        [TestMethod]
        public void Merge_Adjacent()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "ala"),
                new PositionTextPair(3, "ma"),
                new PositionTextPair(0, "ala"),
                new PositionTextPair(3, "ma")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("alama", pairs[0].text);
            Assert.AreEqual(3, pairs[1].position);
            Assert.AreEqual("ma", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("alama", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("ala", pairs[2].text);
        }

        [TestMethod]
        public void Merge_Overlapping()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "ma"),
                new PositionTextPair(1, "ala"),
                new PositionTextPair(0, "ma"),
                new PositionTextPair(1, "ala"),
                new PositionTextPair(20, "alama"),
                new PositionTextPair(24, "ala"),
                new PositionTextPair(20, "alama"),
                new PositionTextPair(24, "ala")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("mala", pairs[0].text);
            Assert.AreEqual(1, pairs[1].position);
            Assert.AreEqual("ala", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("mala", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("ma", pairs[2].text);

            pairs[4].Merge(pairs[5]);
            pairs[7].Merge(pairs[6]);

            Assert.AreEqual(20, pairs[4].position);
            Assert.AreEqual("alamala", pairs[4].text);
            Assert.AreEqual(24, pairs[5].position);
            Assert.AreEqual("ala", pairs[5].text);

            Assert.AreEqual(20, pairs[7].position);
            Assert.AreEqual("alamala", pairs[7].text);
            Assert.AreEqual(20, pairs[6].position);
            Assert.AreEqual("alama", pairs[6].text);
        }

        [TestMethod]
        public void Merge_Substring_CommonStart()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "alama"),
                new PositionTextPair(0, "ala"),
                new PositionTextPair(0, "alama"),
                new PositionTextPair(0, "ala")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("alama", pairs[0].text);
            Assert.AreEqual(0, pairs[1].position);
            Assert.AreEqual("ala", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("alama", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("alama", pairs[2].text);
        }

        [TestMethod]
        public void Merge_Substring_CommonEnd()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "alama"),
                new PositionTextPair(2, "ama"),
                new PositionTextPair(0, "alama"),
                new PositionTextPair(2, "ama")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("alama", pairs[0].text);
            Assert.AreEqual(2, pairs[1].position);
            Assert.AreEqual("ama", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("alama", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("alama", pairs[2].text);
        }

        [TestMethod]
        public void Merge_Substring_Inside()
        {
            PositionTextPair[] pairs = new PositionTextPair[] {
                new PositionTextPair(0, "alama"),
                new PositionTextPair(1, "lam"),
                new PositionTextPair(0, "alama"),
                new PositionTextPair(1, "lam")
            };

            pairs[0].Merge(pairs[1]);
            pairs[3].Merge(pairs[2]);

            Assert.AreEqual(0, pairs[0].position);
            Assert.AreEqual("alama", pairs[0].text);
            Assert.AreEqual(1, pairs[1].position);
            Assert.AreEqual("lam", pairs[1].text);

            Assert.AreEqual(0, pairs[3].position);
            Assert.AreEqual("alama", pairs[3].text);
            Assert.AreEqual(0, pairs[2].position);
            Assert.AreEqual("alama", pairs[2].text);
        }
    }
}
