using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FileScanner.SearchSummary.Tests
{
    [TestClass]
    public class DocumentBuilderFactoryTest
    {
        [TestMethod]
        public void Create_ValidFormat()
        {
            IEnumerable<string> supportedFormats = DocumentBuilderFactory.GetSupportedFormats();

            foreach (string format in supportedFormats)
                Assert.IsNotNull(DocumentBuilderFactory.Create(format));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_InvalidFormat()
        {
            DocumentBuilderFactory.Create("some invalid format");
        }
    }
}
