using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileScanner.Preprocessing.Tests
{
    /// <summary>
    /// Tests for class PreprocessorFactory
    /// </summary>
    [TestClass]
    public class PreprocessorFactoryTest
    {
        private IPreprocessorFactory _factory;

        [TestInitialize]
        public void TestInitialize()
        {
            _factory = new PreprocessorFactory();
        }

        [TestMethod]
        public void TestGetIPreprocessor()
        {
            IPreprocessor preprocessor = _factory.GetIPreprocessor();
            Assert.IsNotNull(preprocessor);
            Assert.IsInstanceOfType(preprocessor, typeof(Preprocessor));
        }
    }
}
