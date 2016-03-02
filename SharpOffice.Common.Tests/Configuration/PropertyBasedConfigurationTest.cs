using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpOffice.Core.Configuration;

namespace SharpOffice.Common.Tests.Configuration
{
    [TestClass]
    public class PropertyBasedConfigurationTest
    {
        private IConfiguration config;
        readonly bool[] arr = { true, false, true };

        [TestMethod]
        public void CreationTest()
        {
            config = new TestPropertyBasedConfiguration();
        }

        [TestMethod]
        public void SetTest()
        {
            CreationTest();
            config.SetProperty("Integer", 8);
            config.SetProperty("Text", "Hello!");
            config.SetProperty("Bits", arr);
        }

        [TestMethod]
        public void GetTest()
        {
            SetTest();
            Assert.AreEqual(8, config.GetProperty<int>("Integer"));
            Assert.AreEqual("Hello!", config.GetProperty<string>("Text"));
            int i = 0;
            foreach (var el in config.GetProperty<IEnumerable<bool>>("Bits"))
            {
                Assert.AreEqual(arr[i++], el);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void InvalidGetNullCastTest()
        {
            CreationTest();
            config.GetProperty<int>("Text");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void InvalidGetCastTest()
        {
            CreationTest();
            SetTest();
            config.GetProperty<int>("Text");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void InvalidSetCastTest()
        {
            CreationTest();
            config.SetProperty("Text", 5);
        }

        [TestMethod]
        public void GetAllPropertiesTest()
        {
            CreationTest();
            SetTest();
            var dict = config.GetAllProperties().ToList();
            Assert.AreEqual("Hello!", dict.First(kvp => kvp.Key == "Text").Value);
            Assert.AreEqual(8, dict.First(kvp => kvp.Key == "Integer").Value);
            int i = 0;
            foreach (var el in (IEnumerable<bool>)dict.First(kvp => kvp.Key == "Bits").Value)
            {
                Assert.AreEqual(arr[i++], el);
            }
        }
    }
}
