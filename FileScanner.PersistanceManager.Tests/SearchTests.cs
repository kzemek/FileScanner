using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileScanner.PersistanceManager.Tests
{
    [TestFixture]
    public class SearchTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException),
            ExpectedMessage = Search.EndTimeEarlierThanStartTimeExceptionMessage)]
        public void Constructor_GivenEndTimeEarlierThanStartTime_ThrowsArgumentException()
        {
            var search = new Search(DateTime.MaxValue, DateTime.MinValue, 123, new LinkedList<string>(), new LinkedList<MatchingFile>());
        }
    }
}
